using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;
using Newtonsoft.Json;
using System.Runtime.Serialization;
using Animy.Utilities;

namespace Animy
{


    public partial class ProjectSetup : Form
    {
        string projectName;
        string projectPath;

        int width = 0;
        int height = 0;
        int fps = 24;
        public ProjectSetup()
        {
            InitializeComponent();
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            fpsCombo.SelectedIndex = 0;
            fpsCombo.DropDownStyle = ComboBoxStyle.DropDownList;
            timer1.Start();
        }

        private void ProjectSetup_Load(object sender, EventArgs e)
        {

        }

        private void nameBox_TextChanged(object sender, EventArgs e)
        {
            // 텍스트 변경 시 커서 위치 복원
            int cursorPosition = nameBox.SelectionStart; // 현재 커서 위치 저장
            nameBox.Text = Util.SafeWords(nameBox.Text); // 텍스트 변환
            nameBox.SelectionStart = cursorPosition;    // 저장된 커서 위치 복원
            projectName = nameBox.Text;
        }

        private void pathBox_TextChanged(object sender, EventArgs e)
        {
            projectPath = pathBox.Text;
        }

        private void browseBtn_Click(object sender, EventArgs e)
        {
            FolderBrowserDialog folderBrowserDialog = new FolderBrowserDialog
            {
                RootFolder = Environment.SpecialFolder.MyDocuments
            };

            if (folderBrowserDialog.ShowDialog() == DialogResult.OK)
            {
                pathBox.Text = folderBrowserDialog.SelectedPath;
                projectPath = pathBox.Text;

                // 프로젝트 파일 이름 설정
                string fileName = projectName + ".anp";
                string filePath = Path.Combine(projectPath, fileName);

                // 파일이 존재하는지 확인
                if (File.Exists(filePath))
                {
                    DialogResult result = MessageBox.Show(
                        fileName + " already exists.\n" +
                        "Do you want to overwrite it?",
                        "Project Exists",
                        MessageBoxButtons.YesNo,
                        MessageBoxIcon.Warning
                    );

                    if (result == DialogResult.No)
                    {
                        MessageBox.Show("Operation canceled.", "Notice", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        pathBox.Text = "";
                        projectPath = "";
                        return; // 파일 덮어쓰기를 취소
                    }
                }

            }
        }

        private void widthBox_TextChanged(object sender, EventArgs e)
        {
            int cursorPosition = widthBox.SelectionStart; // 현재 커서 위치 저장
            widthBox.Text = Util.OnlyNumber(widthBox.Text);
            widthBox.SelectionStart = cursorPosition;    // 저장된 커서 위치 복원
            Int32.TryParse(widthBox.Text, out width);
        }

        private void heightBox_TextChanged(object sender, EventArgs e)
        {
            int cursorPosition = heightBox.SelectionStart; // 현재 커서 위치 저장
            heightBox.Text = Util.OnlyNumber(heightBox.Text);
            heightBox.SelectionStart = cursorPosition;    // 저장된 커서 위치 복원
            Int32.TryParse(heightBox.Text, out height);

        }

        private void fpsCombo_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (fpsCombo.SelectedIndex == 0) fps = 24;
            else fps = 30;
        }

        private void CreateBtn_Click(object sender, EventArgs e)
        {
            if (IsReadyToCreate())
            {
                CreateProject(projectPath, projectName, width, height, fps);
                this.Close();

                MessageBox.Show
                    (
                    "Project Name : " + projectName + "\n" +
                    "Project Path : " + projectPath + "\n" +
                    "Width : " + width.ToString() + "\n" +
                    "Height : " + height.ToString() + "\n" +
                    "FPS : " + fps.ToString() + "\n", "Project Created");

            }
            else
            {
                MessageBox.Show(
                    "Fail to create project",
                    "Error",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error,
                    MessageBoxDefaultButton.Button1
                );
            }
        }

        private bool IsReadyToCreate()
        {
            if (projectName != "" && Util.PathExists(projectPath) && width > 0 && height > 0 && (fps == 30 || fps == 24))
            {
                return true;
            }
            return false;
        }

        // 프로젝트 파일 생성
        private void CreateProject(string filePath, string projectName, int width, int height, int fps)
        {
            // 프로젝트 데이터 생성
            var projectData = new ProjectData
            {
                ProjectName = projectName,
                CreationDate = DateTime.Now.ToString("yyyy-MM-dd"),
                CanvasWidth = width, // 기본 캔버스 크기
                CanvasHeight = height,
                TimelineFrames = 120, // 기본 타임라인 프레임 수
                FPS = fps,
            };


            // JSON으로 직렬화하여 파일 저장
            //string json = JsonConvert.SerializeObject(projectData, Formatting.Indented);

            string anpfile = projectName + ".anp";
            string savePath = Path.Combine(filePath, anpfile);
            Serializer.ToFile(projectData, savePath);

            //File.WriteAllText(savePath, json);

            //MessageBox.Show($"Project '{projectName}' has been created at {filePath}", "Project Created");
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            if(nameBox.Text== "")
            {
                pathBox.Enabled = false;
                browseBtn.Enabled = false;
            }
            else
            {
                pathBox.Enabled= true;
                browseBtn.Enabled= true;
            }
        }
    }

    [DataContract]
    public class ProjectData
    {
        [DataMember]
        public string ProjectName { get; set; }
        [DataMember]
        public string CreationDate { get; set; }
        [DataMember]

        public int CanvasWidth { get; set; }
        [DataMember]

        public int CanvasHeight { get; set; }
        [DataMember]

        public int TimelineFrames { get; set; }
        [DataMember]

        public int FPS { get; set; }

        [DataMember]
        public TimelineData TimelineData { get; set; }
    }
}
