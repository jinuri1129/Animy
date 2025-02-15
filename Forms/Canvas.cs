using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Newtonsoft.Json;
using SkiaSharp;
using SkiaSharp.Views.Desktop;
using WeifenLuo.WinFormsUI.Docking;
using System.Runtime.InteropServices;
using System.Windows.Input;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;
using Animy.Utilities;
using System.Xml;
using static System.Formats.Asn1.AsnWriter;
using Animy.Forms;

namespace Animy
{
    public partial class Canvas : DockContent
    {

        Form1 mainform;
        string title;

        private Color colPic;

        private string projectPath;
        private TimelineData timelineData;
        private ProjectData projectData;

        private SKGLControl skControl;



        private SKMatrix currentMatrix = SKMatrix.CreateIdentity();


        private float zoomFactor = 1.0f; // 현재 줌 수준
        private SKPoint mousePosition = new SKPoint(0, 0);
        private SKPoint canvasMousePosition = new SKPoint(0, 0);


        private float paperWidth;  // 초기 너비
        private float paperHeight; // 초기 높이
        private SKRect paperRect;

        // 드래그 관련 변수
        private bool isZoomMode = false;   // 줌 모드 활성화 여부
        private SKPoint dragStartPoint;  // 드래그 시작 지점
        private float lastZoomFactor = 1f; // 이전 줌 비율

        // 팬 관련 변수
        private bool isPanMode = false;   // 팬 모드 활성화 여부
        private SKPoint panStartPoint;   // 팬 시작 지점


        private bool isBrushMode = false;

        //private SKPoint lastMousePosition;
        //private bool isFirstMouseMove = true; // 첫 이동 여부 확인

        private List<Stroke> strokeList = new List<Stroke>();


        public HistoryManager historymanager;


        private string _currentText;

        private SKBitmap orgBitmap;


        //저장관련
        private bool isNeedToSave = false;

        public ICommand testcommand { get; private set; }

        public void SetFilePath(string path)
        {
            this.projectPath = path;
        }

        public string GetFilePath()
        {
            return this.projectPath;
        }

        public Canvas(Form1 main, ProjectData project)
        {
            InitializeComponent();
            mainform = main;

            // 폼 설정
            this.Width = 800;
            this.Height = 600;
            title = project.ProjectName;
            this.Text = title;

            paperWidth = project.CanvasWidth;
            paperHeight = project.CanvasHeight;
            paperRect = new SKRect();


            // SKControl 생성 및 설정
            skControl = new SKGLControl();
            skControl.Dock = DockStyle.Fill;

            this.Controls.Add(skControl);

            timelineData = new TimelineData();
            timelineData.AddFrame(project.CanvasWidth, project.CanvasHeight); // 초기 프레임
            mainform.timeline.UpdateTimeLine(timelineData, project);
            projectData = project;

        }
        private void Canvas_Load(object sender, EventArgs e)
        {
            InitializePaper(); // 초기화
            skControl.PaintSurface += SkControl_PaintSurface;
            skControl.MouseDown += SkControl_MouseDown;
            skControl.MouseMove += SkControl_MouseMove;
            skControl.MouseUp += SkControl_MouseUp;
            skControl.KeyDown += SkControl_KeyDown;
            skControl.KeyUp += SkControl_KeyUp;
            skControl.Resize += SkControl_Resize;

            mainform.timeline.SetCanvas(this);

            historymanager = new HistoryManager();
        }
        private void SkControl_PaintSurface(object? sender, SKPaintGLSurfaceEventArgs e)
        {
            var canvas = e.Surface.Canvas;
            canvas.Clear(SKColors.Gray); // 배경색
            canvas.SetMatrix(currentMatrix); // 현재 매트릭스 적용
            DrawPaper(canvas); // 종이 그리기


            // 현재 프레임의 비트맵을 캔버스에 렌더링
            var currentFrame = timelineData.GetCurrentFrame();
            if (currentFrame?.LayerList[0].LayerBitmap != null)
            {
                //canvas.DrawBitmap(currentFrame.Bitmap, paperRect);
                canvas.DrawBitmap(currentFrame.LayerList[0].LayerBitmap,paperRect);
            }
        }
        private void SkControl_KeyUp(object? sender, KeyEventArgs e)
        {
            isZoomMode = false;
            isPanMode = false;
        }
        private void SkControl_Resize(object? sender, EventArgs e)
        {
            InitializePaper();
            skControl.Invalidate();
        }
        private void SkControl_MouseDown(object? sender, MouseEventArgs e)
        {
            strokeList.Clear();
            if (e.Button == MouseButtons.Left && isBrushMode)
            {
                var currentFrame = timelineData.GetCurrentFrame();

                // 현재 비트맵을 깊은 복사
                orgBitmap = new SKBitmap(currentFrame.LayerList[0].LayerBitmap.Width, currentFrame.LayerList[0].LayerBitmap.Height);

                // 캔버스를 사용하여 기존 비트맵의 내용을 새 비트맵에 복사
                using (SKCanvas canvas = new SKCanvas(orgBitmap))
                {
                    // 기존 비트맵을 새 비트맵에 그리기
                    canvas.DrawBitmap(currentFrame.LayerList[0].LayerBitmap, 0, 0);
                }
            }

            if (isPanMode)
            {
                panStartPoint = new SKPoint(e.X, e.Y);
            }
            if (isZoomMode)
            {
                dragStartPoint = new SKPoint(e.X, e.Y);
                lastZoomFactor = 1f; // 초기화
            }
        }
        private void SkControl_MouseMove(object? sender, MouseEventArgs e)
        {
            Console.WriteLine(mainform.PenPressure);
            colPic = mainform.colorPicker.GetColor();

            mousePosition = new SKPoint(e.X, e.Y); // 현재 마우스 위치 저장

            // 1. 마우스 좌표를 캔버스 좌표로 변환
            var inverseMatrix = currentMatrix;
            if (!currentMatrix.TryInvert(out inverseMatrix)) return;
            canvasMousePosition = inverseMatrix.MapPoint(mousePosition);


            this.Text = title + " <" + (int)canvasMousePosition.X + "," + (int)canvasMousePosition.Y + ">";
            if (historymanager.isNeedToSave)
            {
                this.Text = title + "*";

            }



            if (e.Button == MouseButtons.Left && isBrushMode)
            {
                var currentFrame = timelineData.GetCurrentFrame();
                using (var canvas = new SKCanvas(currentFrame.LayerList[0].LayerBitmap))
                using (var paint = new SKPaint { Color = new SKColor(colPic.R, colPic.G, colPic.B), StrokeWidth = 3, IsAntialias = false })
                {

                    // 즉시 반응을 위해 현재 점을 찍음
                    canvas.DrawCircle(canvasMousePosition, (int)Util.PressureToSize(mainform.PenPressure,mainform.PressureMax,1,10), paint);

                    strokeList.Add(new Stroke(canvasMousePosition, mainform.PenPressure, mainform.PressureMax, new SKColor(colPic.R, colPic.G, colPic.B)));
                }

                skControl.Invalidate(); // 화면 갱신
            }
            else if (e.Button == MouseButtons.Left && isPanMode)
            {
                // 이동 벡터 계산 (이전 지점에서 현재 지점으로 이동)
                SKPoint delta = new SKPoint(
                    (mousePosition.X - panStartPoint.X) * 1.3f,
                    (mousePosition.Y - panStartPoint.Y) * 1.3f
                );
                // 이동 매트릭스 적용
                var translationMatrix = SKMatrix.CreateTranslation(delta.X, delta.Y);
                currentMatrix = translationMatrix.PostConcat(currentMatrix);
                // 시작 지점 업데이트
                panStartPoint = mousePosition;

                // 화면 업데이트
                skControl.Invalidate();
            }
            else if (e.Button == MouseButtons.Left && isZoomMode)
            {

                // 드래그 거리 계산 (좌/우 이동에 따라 줌인/줌아웃 결정)
                float dragDistance = (mousePosition.X - dragStartPoint.X) * 2f;

                // 줌 비율 계산 (우측으로 드래그하면 확대, 좌측으로 드래그하면 축소)
                float zoomFactor = 1 + dragDistance * 0.001f; // 드래그 거리 기반 비율 (0.001은 조정 가능)
                zoomFactor = Math.Clamp(zoomFactor, 0.5f, 3f); // 최소/최대 줌 제한

                // 이전 줌 비율과 비교하여 매트릭스 업데이트
                float scaleChange = zoomFactor / lastZoomFactor;
                Zoom(scaleChange, dragStartPoint);

                // 마지막 줌 비율 업데이트
                lastZoomFactor = zoomFactor;
            }
        }
        private void SkControl_MouseUp(object? sender, MouseEventArgs e)
        {
            if (strokeList.Count < 2) return; // 2개 이상 좌표가 있어야 보간 가능

            var currentFrame = timelineData.GetCurrentFrame();

            // 리스트 복사 후 전달 (깊은 복사)

            var strokeCopy = new List<Stroke>(strokeList.Select(s => new Stroke(s.position, s.Pressure, mainform.PressureMax, s.Color)));

            DrawCommand drawcmd = new DrawCommand(strokeCopy, orgBitmap, currentFrame.LayerList[0].LayerBitmap, skControl);
            historymanager.Execute(drawcmd);

            strokeList.Clear(); // strokeList 초기화
            skControl.Invalidate(); // 화면 갱신
        }

        private void SkControl_KeyDown(object? sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Q)
            {
                Zoom(0.9f, mousePosition); // 축소
            }
            else if (e.KeyCode == Keys.W)
            {
                Zoom(1.1f, mousePosition); // 확대
            }
            if (e.KeyCode == Keys.Z)
            {
                isZoomMode = true;
                isBrushMode = false;
                isPanMode = false;

            }
            if (e.KeyCode == Keys.B)
            {
                isBrushMode = true;
                isZoomMode = false;
                isPanMode = false;

            }
            if (e.KeyCode == Keys.Space)
            {
                isPanMode = true;
                isBrushMode = false;
                isZoomMode = false;

            }

            if (e.KeyCode == Keys.S)
            {
                if (timelineData.CurrentFrameIndex < timelineData.Frames.Count - 1)
                {
                    timelineData.CurrentFrameIndex++;
                }
                mainform.timeline.UpdateTimeLine(timelineData, projectData);
                skControl.Invalidate();
            }
            if (e.KeyCode == Keys.A)
            {
                if (0 < timelineData.CurrentFrameIndex)
                {
                    timelineData.CurrentFrameIndex--;
                }
                mainform.timeline.UpdateTimeLine(timelineData, projectData);
                skControl.Invalidate();
            }

            if (e.KeyCode == Keys.X)
            {
                undoToolStripMenuItem.PerformClick();
            }
            if (e.KeyCode == Keys.C)
            {
                redoToolStripMenuItem.PerformClick();
            }
        }


        //++++++++++++
        //Functions
        //++++++++++++
        //데이터 업데이트
        public void UpdateCanvas(TimelineData td)
        {

            skControl.Invalidate();
        }

        // 초기화: 고정된 종이 크기를 화면에 맞게 설정
        private void InitializePaper()
        {
            // 종이 크기와 화면 크기의 비율을 계산
            float scaleX = skControl.Width / paperWidth;
            float scaleY = skControl.Height / paperHeight;

            // 비율 중 작은 값을 선택 (비율을 유지하기 위해)
            float initialScale = Math.Min(scaleX, scaleY);

            // 초기 매트릭스를 화면 크기에 맞게 설정
            currentMatrix = SKMatrix.CreateScale(initialScale, initialScale);

            // 종이를 화면 중앙에 배치
            float translateX = (skControl.Width - paperWidth * initialScale) / 2f;
            float translateY = (skControl.Height - paperHeight * initialScale) / 2f;
            currentMatrix = currentMatrix.PostConcat(SKMatrix.CreateTranslation(translateX, translateY));

            // 종이 영역 설정 (고정 크기)
            paperRect = new SKRect(0, 0, paperWidth, paperHeight);
        }
        // 종이 그리기
        private void DrawPaper(SKCanvas canvas)
        {
            using (var paint = new SKPaint { Color = SKColors.White, Style = SKPaintStyle.Fill })
            {
                canvas.DrawRect(paperRect, paint);
            }
        }

        // 줌 함수
        private void Zoom(float scaleFactor, SKPoint centerPoint)
        {
            // 1. 마우스 좌표를 캔버스 좌표로 변환
            var inverseMatrix = currentMatrix;
            if (!currentMatrix.TryInvert(out inverseMatrix)) return;
            var canvasCenterPoint = inverseMatrix.MapPoint(centerPoint);

            // 2. 줌 수행 (중심 기준)
            var translateToCenter = SKMatrix.CreateTranslation(-canvasCenterPoint.X, -canvasCenterPoint.Y);
            var scale = SKMatrix.CreateScale(scaleFactor, scaleFactor);
            var translateBack = SKMatrix.CreateTranslation(canvasCenterPoint.X, canvasCenterPoint.Y);

            currentMatrix = translateToCenter
                .PostConcat(scale)
                .PostConcat(translateBack)
                .PostConcat(currentMatrix);

            skControl.Invalidate();
        }        // Export: 고정된 종이 크기대로 이미지 저장
        private void ExportCanvas(string filePath, SKEncodedImageFormat format)
        {

            // 저장
            using (var image = SKImage.FromBitmap(timelineData.GetCurrentFrame().LayerList[0].LayerBitmap))
            {
                using (var data = image.Encode(format, 100)) // 선택된 포맷으로 인코딩
                {
                    using (var stream = File.OpenWrite(filePath))
                    {
                        data.SaveTo(stream);
                    }
                }
            }

        }
        private void currentFrameToolStripMenuItem_Click(object sender, EventArgs e)
        {
            // SaveFileDialog 설정
            SaveFileDialog sfd = new SaveFileDialog
            {
                Filter = "PNG Image (*.png)|*.png|JPEG Image (*.jpg)|*.jpg",
                DefaultExt = "png", // 기본 확장자
                Title = "Export Canvas"
            };

            if (sfd.ShowDialog() == DialogResult.OK)
            {
                // 선택된 파일 확장자를 기반으로 형식 결정
                SKEncodedImageFormat format = SKEncodedImageFormat.Png; // 기본값
                string extension = Path.GetExtension(sfd.FileName).ToLower();

                switch (extension)
                {
                    case ".jpg":
                    case ".jpeg":
                        format = SKEncodedImageFormat.Jpeg;
                        break;
                    case ".png":
                    default:
                        format = SKEncodedImageFormat.Png;
                        break;
                }

                // 캔버스를 선택된 형식으로 내보내기
                ExportCanvas(sfd.FileName, format);
            }
        }

        private void Canvas_Enter(object sender, EventArgs e)
        {
            mainform.timeline.Text = "Timeline  " + title;
            mainform.timeline.SetCanvas(this);
            mainform.timeline.UpdateTimeLine(timelineData, projectData);
        }

        public void Save()
        {


            for (int i = 0; i < timelineData.Frames.Count; i++)
            {
                for (int j = 0; j < timelineData.Frames[i].LayerList.Count; j++)
                {

                    var layerImagebyte = Serializer.SKBitmapToByteArray(timelineData.Frames[i].LayerList[j].LayerBitmap);
                    this.timelineData.Frames[i].LayerList[j].LayerBytemap = layerImagebyte;

                }
            }



            // 프로젝트 데이터 생성
            var saveprojectData = new ProjectData
            {
                ProjectName = projectData.ProjectName,
                CreationDate = DateTime.Now.ToString("yyyy-MM-dd"),
                CanvasWidth = projectData.CanvasWidth, // 기본 캔버스 크기
                CanvasHeight = projectData.CanvasHeight,
                TimelineFrames = projectData.TimelineFrames, // 기본 타임라인 프레임 수
                FPS = projectData.FPS,
                TimelineData = timelineData,
            };

            // JSON으로 직렬화하여 파일 저장
            //string json = JsonConvert.SerializeObject(saveprojectData, Formatting.Indented);

            string anpfile = saveprojectData.ProjectName + ".anp";
            string savePath = projectPath;

            //File.WriteAllText(savePath, json);
            Serializer.ToFile(saveprojectData, savePath);

            historymanager.isNeedToSave = false;
        }


        public bool SaveAs()
        {
            SaveFileDialog sfd = new SaveFileDialog()
            {
                Title = "Save Animy Project File",
                Filter = "Animy Project Files (*.anp)|*.anp", // 필터 설정
                FilterIndex = 1, // 기본 선택 필터를 .anp로 설정
                InitialDirectory = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments) // 초기 디렉터리 설정
            };

            if (sfd.ShowDialog() == DialogResult.OK)
            {
                for (int i = 0; i < timelineData.Frames.Count; i++)
                {
                    for (int j = 0; j < timelineData.Frames[i].LayerList.Count; j++)
                    {

                        var layerImagebyte = Serializer.SKBitmapToByteArray(timelineData.Frames[i].LayerList[j].LayerBitmap);
                        this.timelineData.Frames[i].LayerList[j].LayerBytemap = layerImagebyte;

                    }
                }

                // 프로젝트 데이터 생성
                var saveprojectData = new ProjectData
                {
                    ProjectName = projectData.ProjectName,
                    CreationDate = DateTime.Now.ToString("yyyy-MM-dd"),
                    CanvasWidth = projectData.CanvasWidth, // 기본 캔버스 크기
                    CanvasHeight = projectData.CanvasHeight,
                    TimelineFrames = projectData.TimelineFrames, // 기본 타임라인 프레임 수
                    FPS = projectData.FPS,
                    TimelineData = timelineData,
                };

                string anpfile = sfd.FileName;
                string fileName = Path.GetFileName(sfd.FileName);

                Console.WriteLine(anpfile);
                Serializer.ToFile(projectData, anpfile);
                Serializer.UpdateNodeValue(anpfile, "ProjectName", fileName);
                isNeedToSave = false;
                return true;
            }
            return false;
        }
        private void saveProjectToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Save();
        }

        private void saveProjectAsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            SaveAs();
        }
        private void projectInfoToolStripMenuItem_Click(object sender, EventArgs e)
        {
            MessageBox.Show("Creation Data: " + projectData.CreationDate);

        }

        private void undoToolStripMenuItem_Click(object sender, EventArgs e)
        {
            historymanager.Undo();
            skControl.Invalidate();

        }

        private void findFlawToolStripMenuItem_Click(object sender, EventArgs e)
        {
            var currentFrame = timelineData.GetCurrentFrame();
            SKBitmap skiaBitmap = currentFrame.LayerList[0].LayerBitmap;

            // 픽셀 데이터 가져오기
            SKColor[] pixels = new SKColor[skiaBitmap.Width * skiaBitmap.Height];
            skiaBitmap.Pixels.CopyTo(pixels, 0);

            // 각 픽셀을 확인하고 흰색 픽셀을 빨간색으로 변경
            for (int i = 0; i < pixels.Length; i++)
            {
                SKColor pixelColor = pixels[i];

                // 흰색 픽셀을 빨간색으로 변경
                if (pixelColor.Red == 0 && pixelColor.Green == 0 && pixelColor.Blue == 0 && pixelColor.Alpha == 0)
                {
                    pixels[i] = new SKColor(255, 0, 0); // 빨간색으로 변경
                }
            }

            // 수정된 픽셀 데이터를 Bitmap에 다시 적용
            skiaBitmap.Pixels = pixels;

            // Bitmap을 업데이트하여 변경 사항을 반영
            currentFrame.LayerList[0].LayerBitmap = skiaBitmap;

            skControl.Invalidate();


            // Assuming 'mainform.timeline.Text' is the current text that needs to be changed
            Action<string> applyText = (text) =>
            {
                mainform.timeline.Text = text;  // This applies the text to the timeline or any relevant UI element
            };

            // Creating the TextChangeCommand with original text, new text, and applyText delegate
            TextChangeCommand tcmd = new TextChangeCommand(mainform.timeline.Text, "newText", applyText);
            historymanager.Execute(tcmd);
        }



        private void Canvas_Activated(object sender, EventArgs e)
        {
            mainform.timeline.SetCanvas(this);

        }

        private void redoToolStripMenuItem_Click(object sender, EventArgs e)
        {
            historymanager.Redo();
            skControl.Invalidate();
        }

        private void Canvas_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (historymanager.isNeedToSave)
            {
                SaveForm save = new SaveForm(this);
                save.StartPosition = FormStartPosition.CenterScreen;
                var result = save.ShowDialog(); // 한 번만 호출
                save.Invalidate();

                if (result == DialogResult.Cancel)
                {
                    if(mainform.closeEventArg != null)
                    {
                        mainform.closeEventArg.Cancel = true;
                    }

                    e.Cancel = true;
                }
                else if (result == DialogResult.OK)
                {
                    e.Cancel = false;
                    if (mainform.closeEventArg != null)
                    {
                        mainform.closeEventArg.Cancel = false;
                    }
                }
            }
        }

    }
}