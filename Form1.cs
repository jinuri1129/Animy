using Animy.Forms;
using Newtonsoft.Json;
using WeifenLuo.WinFormsUI.Docking;
using VBTablet;
using System.Runtime.InteropServices;

namespace Animy
{




    public partial class Form1 : Form
    {
        public Tablet Digitizer;

        int tmpl;
        string tmps;
        int Xold, Yold, RectWidth;
        public int PenPressure;
        public int PressureMax;


        [DllImport("user32.dll", SetLastError = true)]
        private static extern IntPtr FindWindow(string lpClassName, string lpWindowName);

        [DllImport("user32.dll")]
        private static extern bool SetWindowPos(IntPtr hWnd, IntPtr hWndInsertAfter, int X, int Y, int cx, int cy, uint uFlags);
        private static readonly IntPtr HWND_BOTTOM = new IntPtr(1);
        private static readonly IntPtr HWND_TOPMOST = new IntPtr(-1);
        private const uint SWP_NOSIZE = 0x0001;
        private const uint SWP_NOMOVE = 0x0002;
        private const uint SWP_SHOWWINDOW = 0x0040;














        public Timeline timeline;
        public ColorPicker colorPicker;

        private List<Canvas> canvasList = new List<Canvas>();
        public FormClosingEventArgs closeEventArg;



        public Form1(string args)
        {
            InitializeComponent();

            this.StartPosition = FormStartPosition.CenterScreen;
            dockPanel1.Dock = DockStyle.Fill;
            dockPanel1.Theme = new WeifenLuo.WinFormsUI.Docking.VS2015LightTheme(); // 라이트 테마
            timeline = new Timeline();
            timeline.Show(dockPanel1, DockState.DockBottom);
            colorPicker = new ColorPicker();
            colorPicker.Show(dockPanel1, DockState.DockLeft);
            OpenProject(args);

            InitTablet();
            ConnectTablet();
            EnableTablet();

        }
        public Form1()
        {
            InitializeComponent();
            this.StartPosition = FormStartPosition.CenterScreen;

            dockPanel1.Dock = DockStyle.Fill;
            dockPanel1.Theme = new WeifenLuo.WinFormsUI.Docking.VS2015LightTheme(); // 라이트 테마
            timeline = new Timeline();
            timeline.Show(dockPanel1, DockState.DockBottom);
            colorPicker = new ColorPicker();
            colorPicker.Show(dockPanel1, DockState.DockLeft);

            InitTablet();
            ConnectTablet();
            EnableTablet();

        }

        private void newProjectToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (!Application.OpenForms.OfType<ProjectSetup>().Any())
            {
                ProjectSetup projectSetup = new ProjectSetup();
                projectSetup.ShowDialog();
            }

        }

        private void Form1_Load(object sender, EventArgs e)
        {
            //after form1 load
            StartUp startup = new StartUp();
            startup.ShowDialog(); // 모달 창으로 실행

        }
        private void Form1_Shown(object sender, EventArgs e)
        {
        }

        private void openProjectToolStripMenuItem_Click(object sender, EventArgs e)
        {
            OpenFileDialog ofd = new OpenFileDialog
            {
                Title = "Open Animy Project File",
                Filter = "Animy Project Files (*.anp)|*.anp|All Files (*.*)|*.*", // 필터 설정
                FilterIndex = 1, // 기본 선택 필터를 .anp로 설정
                InitialDirectory = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments) // 초기 디렉터리 설정
            };
            if (ofd.ShowDialog() == DialogResult.OK)
            {
                string selectedFile = ofd.FileName;
                OpenProject(selectedFile);
            }
        }


        private void OpenProject(string filePath)
        {
            if (Path.GetExtension(filePath) == ".anp")
            {
                // .anp 파일 로드 로직 추가
                CreateCanvas(filePath);


            }
            else
            {
                MessageBox.Show("Invalid file format. Please select a .anp file.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void CreateCanvas(string projectPath)
        {
            ProjectData project = Serializer.FromFile<ProjectData>(projectPath);

            Canvas canvas = new Canvas(this, project);
            canvas.SetFilePath(projectPath);
            canvas.Show(dockPanel1, DockState.Document);
            Console.WriteLine(canvas.GetFilePath());

            if (project.TimelineData != null)
            {
                timeline.SetData(project);
            }
            else
            {
                MessageBox.Show("time line data null");
            }
            canvasList.Add(canvas);
        }

        private void dockPanel1_ActiveContentChanged(object sender, EventArgs e)
        {

        }

        private void saveProjectToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }

        private void saveProjectAsToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }

        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {
            closeEventArg = e;

            List<Canvas> CanvasAttemptClose = new List<Canvas>();

            foreach (var item in canvasList)
            {
                if (item.historymanager.isNeedToSave)
                {
                    CanvasAttemptClose.Add(item);
                }
            }

            foreach (var closeCanvas in CanvasAttemptClose)
            {
                closeCanvas.Activate();
                closeCanvas.Close();
            }
        }

        public List<Canvas> GetAllCanvas()
        {
            return canvasList;
        }

        private void addBlankToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Blank blank = new Blank();
            blank.Show(this.dockPanel1,DockState.Float);
        }




        //digitiger

        private void InitTablet()
        {
            try
            {

                // On Error GoTo errorload
                Digitizer = new Tablet(); //Actually create the tablet object
                                          //sldGranularity.Value = 2; //Set packet granularity value
                                          //that uses a tablet attribute. Remember not everyone has _your_ tablet.
                Digitizer.UnavailableIsError = false;

                //Tablet.hWnd = frmMain.DefInstance.Handle
                //prgX.Maximum = Digitizer.Context.OutputExtentX - Digitizer.Context.OutputOriginX;
                //prgY.Maximum = Digitizer.Context.OutputExtentY - Digitizer.Context.OutputOriginY;
                //prgZ.Maximum = 255;
                PressureMax = (int)Digitizer.Device.NormalPressure.get_Max(true);
                //prgTangentPressure.Maximum = (int)Digitizer.Device.TangentPressure.get_Max(true);


                //Digitizer.ContextClosed += new VBTablet.Tablet.ContextClosedEventHandler(ContextClosed);
                //Digitizer.ContextOpened += new VBTablet.Tablet.ContextOpenedEventHandler(ContextOpened);
                //Digitizer.ContextRepositioned += new Tablet.ContextRepositionedEventHandler(ContextRepositioned);
                //Digitizer.ContextUpdated += new Tablet.ContextUpdatedEventHandler(ContextUpdated);
                //Digitizer.CursorChange += new Tablet.CursorChangeEventHandler(CursorChange);
                //Digitizer.InfoChange += new Tablet.InfoChangeEventHandler(InfoChange);
                //Digitizer.ProximityChange += new Tablet.ProximityChangeEventHandler(ProximityChange);
                Digitizer.PacketArrival += new Tablet.PacketArrivalEventHandler(PacketArrival);


            }

            catch (Exception ex)
            {

                MessageBox.Show(ex.Message + ". Please Connect the Digitizer Device First !!");

            }
        }


        private void ConnectTablet()
        {
            IntPtr Hwnd;
            bool IsDigitizingContext = false;
            string ContextID = "FirstContext";
            Hwnd = this.Handle;
            Digitizer.hWnd = Hwnd;
            Digitizer.AddContext(ContextID, ref IsDigitizingContext);
            Digitizer.SelectContext(ref ContextID);
            Digitizer.Connected = true;
            Digitizer.Context.QueueSize = 32;//Set queue size to a reasonable value
            Deviceinfo();
        }
        private void Deviceinfo()
        {
            /*
            lblDeviceMaxPktRate.Text = "Maximal Update Packet Rate (Packets/sec): " + Digitizer.Device.MaxPktRate.ToString();
            lblDeviceMargins.Text = "Device Context Margins (x, y, z): " + Digitizer.Device.Margins.X.ToString() + ", " + Digitizer.Device.Margins.Y.ToString() + ", " + Digitizer.Device.Margins.Z.ToString(); ;
            lblDeviceNormalPressure.Text = "Normal Pressure (Min, Max, Resolution, Units): " + Digitizer.Device.NormalPressure.get_Min(true).ToString() + ", " + Digitizer.Device.NormalPressure.get_Max(true).ToString() + " , " + Digitizer.Device.NormalPressure.Resolution.ToString() + " , " + Digitizer.Device.NormalPressure.Units;
            lblDevicePnPID.Text = "Plug-and-Play device ID: " + Digitizer.Device.PnPID.ToString();
            lblDeviceX.Text = "X capabilities (Min, Max, Resolution, Units): " + Digitizer.Device.X.get_Min(true).ToString() + ", " + Digitizer.Device.X.get_Max(true).ToString() + " ," + Digitizer.Device.X.Resolution.ToString() + " ," + Digitizer.Device.X.Units;
            lblDeviceY.Text = "Y capabilities (Min, Max, Resolution, Units): " + Digitizer.Device.Y.get_Min(true).ToString() + ", " + Digitizer.Device.Y.get_Max(true).ToString() + " ," + Digitizer.Device.Y.Resolution.ToString() + " ," + Digitizer.Device.Y.Units;
            lblDeviceZ.Text = "Z capabilities (Min, Max, Resolution, Units): " + Digitizer.Device.Z.get_Min(true).ToString() + ", " + Digitizer.Device.Z.get_Max(true).ToString() + " ," + Digitizer.Device.Z.Resolution.ToString() + " ," + Digitizer.Device.Z.Units;
            lblDeviceAzimuth.Text = "Azimuth capabilities (Min, Max, Resolution, Units): " + Digitizer.Device.Azimuth.get_Min(true).ToString() + ", " + Digitizer.Device.Azimuth.get_Max(true).ToString() + " ," + Digitizer.Device.Azimuth.Resolution.ToString() + " ," + Digitizer.Device.Azimuth.Units;
            lblDevName.Text = " Device Name : " + Digitizer.Device.GetType().ToString();

            lblStatusContexts.Text = "Number of Contexts open : " + Digitizer.Status.OpenContexts.ToString();
            lblStatusSysContexts.Text = "Number of System Contexts open : " + Digitizer.Status.OpenSysContexts.ToString();
            lblStatusMaxRate.Text = "Maximum Packet Rate in use (packets/sec): " + Digitizer.Status.MaxCurrentPktRate.ToString();
            lblStatusMgrHandles.Text = "Number of Manager Handles open : " + Digitizer.Status.OpenMgrHandles.ToString();
            lblExtensionTag.Text = "Extension ID: " + Digitizer.Extension.ID.ToString();
            lblExtensionAbsSize.Text = "Size of Extension in a Packet (Absolute Mode): " + Digitizer.Extension.AbsoluteSize.ToString();
            lblExtensionRelSize.Text = "Size of Extension in a Packet (Relative Mode): " + Digitizer.Extension.RelativeSize.ToString();
            lblExtensionMask.Text = "Extension Or-Mask: " + Digitizer.Extension.OrMask.MaskValue.ToString();
            */

        }
        private void EnableTablet()
        {
            InContext();//Call Incontext
                        //Check Following Code
            Digitizer.Context.TrackingMode = true;
            Digitizer.Context.Enabled = true;
            return;
        }


        private void DisableTablet()
        {
            OutContext();
            Digitizer.Context.Enabled = false;
        }
        private void DisConnectTablet()
        {
            string RemoveCOntextID = "FirstContext";
            Digitizer.Connected = false;
            Digitizer.RemoveContext(ref RemoveCOntextID);
            // Disable the Digitizer
        }



        private void OutContext() //Cursor has moved out of the context
        {
        }
        private void InContext() //Cursor has moved into context
        {
            //If digitising Then timClear.Enabled = False
        }

        public void PacketArrival(ref IntPtr ContextHandle, ref int Cursor_Renamed, ref int X, ref int Y, ref int Z, ref int Buttons, ref int Pressure, ref int TangentPressure, ref int Azimuth, ref int Altitude, ref int Twist, ref int Pitch, ref int Roll, ref int Yaw, ref int PacketSerial, ref int PacketTim)
        {
            //Show current stats. Note that it's a good idea not to update
            //if not necessary - 100+ updates a second can really hurt performance

            tmpl = System.Math.Abs(Pressure);
            PenPressure = tmpl;


            /*
             * tmpl = System.Math.Abs(X);
            if (tmpl != prgX.Value)
            {
                if (tmpl <= prgX.Maximum)
                    prgX.Value = tmpl;
            }
            tmpl = System.Math.Abs(Y);
            if (tmpl != prgY.Value)
            {
                if (tmpl <= prgY.Maximum)
                    prgY.Value = tmpl;
            }
            tmpl = System.Math.Abs(Z);
            if (tmpl != prgZ.Value)
            {
                if (tmpl <= prgZ.Maximum)
                    prgZ.Value = tmpl;
            }
            tmpl = System.Math.Abs(Pressure);
            if (tmpl != progressBar1.Value)
                progressBar1.Value = tmpl;
            tmpl = System.Math.Abs(TangentPressure);
            if (tmpl != prgTangentPressure.Value)
                prgTangentPressure.Value = tmpl;

            if (Convert.ToInt32(lblX.Text) != X)
                lblX.Text = X.ToString();
            if (Convert.ToInt32(lblY.Text) != Y)
                lblY.Text = Y.ToString();
            if (Convert.ToInt32(lblZ.Text) != Z)
                lblZ.Text = Z.ToString();
            if (Convert.ToInt32(lblCursor.Text) != Cursor_Renamed)
                lblCursor.Text = Cursor_Renamed.ToString();
            if (Convert.ToInt32(lblPressure.Text) != Pressure)
                lblPressure.Text = Pressure.ToString();
            if (Convert.ToInt32(lblTangentPressure.Text) != TangentPressure)
                lblTangentPressure.Text = TangentPressure.ToString();
            if (Convert.ToInt32(lblAzimuth.Text) != Azimuth)
                lblAzimuth.Text = Azimuth.ToString();
            if (Convert.ToInt32(lblAltitude.Text) != Altitude)
                lblAltitude.Text = Altitude.ToString();
            if (Convert.ToInt32(lblTwist.Text) != Twist)
                lblTwist.Text = Twist.ToString();
            if (Convert.ToInt32(lblPitch.Text) != Pitch)
                lblPitch.Text = Twist.ToString();
            if (Convert.ToInt32(lblRoll.Text) != Roll)
                lblRoll.Text = Roll.ToString();
            if (Convert.ToInt32(lblYaw.Text) != Yaw)
                lblYaw.Text = Yaw.ToString();

            Pen ppen = new Pen(Color.Red, 1);
            Graphics Gr;

            if (Pressure > 0) // 'catch normalpressure and button 1
            {
                if (Digitizer.Context.CursorIsInverted)
                {
                    ppen.Color = Color.Red;
                    //picDraw.Refresh();
                }
                else
                {
                    ppen.Color = Color.Blue;
                }
                tmpl = (int)Digitizer.Device.NormalPressure.get_Max(true);
                RectWidth = (int)((Pressure / (float)tmpl) * 100);
                if ((RectWidth >= 0) & (RectWidth <= 20))
                    ppen.Color = Color.LawnGreen;
                else if ((RectWidth >= 21) & (RectWidth <= 40))
                    ppen.Color = Color.Blue;
                else if ((RectWidth >= 41) & (RectWidth <= 100))
                    ppen.Color = Color.Red;

                try
                {
                   // Gr = picDraw.CreateGraphics();
                   // Gr.DrawLine(ppen, X, picDraw.Height - Y, Xold, picDraw.Height - Yold + 1);
                    // Gr.DrawEllipse(ppen, X, picDraw.Height - Y, 1, 1);

                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message.ToString(), "Error", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                }
            }
            //   Application.DoEvents(); // Poor Data Capture & Rendering
            Xold = X;
            Yold = Y;*/
        }


    }
}
