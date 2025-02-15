using OpenTK;
using SkiaSharp;
using SkiaSharp.Views.Desktop;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Net.NetworkInformation;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using WeifenLuo.WinFormsUI.Docking;

namespace Animy
{
    public partial class Timeline : DockContent
    {

        TimelineData timeLineData;
        ProjectData projectData;
        Canvas canvas;
        private FlowLayoutPanel timelinePanel;
        public event EventHandler FrameCountChanged;

        public Timeline()
        {
            InitializeComponent();
            this.ControlBox = false;

            FrameCountChanged += On_Timeline_FrameCountChanged;
        }
        private void InitializeTimelinePanel()
        {
            // 패널이 없으면 생성, 있으면 초기화
            if (timelinePanel == null)
            {
                timelinePanel = new FlowLayoutPanel
                {
                    Name = "timelinePanel",
                    Dock = DockStyle.Bottom,
                    AutoScroll = true,
                    FlowDirection = FlowDirection.LeftToRight,
                    Height = 100,

                };
                this.Controls.Add(timelinePanel);
            }
            else
            {
                // 기존에 추가된 버튼들을 모두 제거
                timelinePanel.Controls.Clear();
            }
        }
        private void On_Timeline_FrameCountChanged(object? sender, EventArgs e)
        {
            // 타임라인 패널 초기화(생성 또는 클리어)
            InitializeTimelinePanel();

            // timeLineData.Frames.Count 갯수에 따라 버튼 생성
            for (int i = 0; i < timeLineData.Frames.Count; i++)
            {
                var frameButton = new Button
                {
                    
                    Text = $"{i + 1}",
                    Width = 50,
                    Height = 50,
                };
                timelinePanel.Controls.Add(frameButton);
                frameButton.Click += (s, e) =>
                {
                    int number;
                    int.TryParse(frameButton.Text ,out number);
                    timeLineData.SetCurrentFrameIndex(number-1);;
                    UpdateTimeLine(timeLineData, projectData);
                    canvas.UpdateCanvas(timeLineData);
                };


            }

        }



        public void SetData(ProjectData pd)
        {
            //this.timeLineData = new TimelineData(td);
            //this.timeLineData.Frames = new List<Frame>();
            //this.timeLineData.Frames = td.Frames;
            //this.timeLineData.CurrentFrameIndex = td.CurrentFrameIndex;
            //this.timeLineData.ByteMaps = td.ByteMaps;
            //this.timeLineData.ByteToFrames();

            for(int i = 0; i < pd.TimelineData.Frames.Count; i++)
            {
                for(int j = 0; j < pd.TimelineData.Frames[i].LayerList.Count; j++)
                {


                    Console.WriteLine(pd.TimelineData.Frames.Count);
                    Console.WriteLine(pd.TimelineData.Frames[i].LayerList.Count);
                    Console.WriteLine(pd.TimelineData.Frames[i].LayerList == null);
                    Console.WriteLine(pd.TimelineData.Frames[i].LayerList[j].LayerBytemap.Length);
                    Console.WriteLine(pd.TimelineData.Frames[i].LayerList[j].LayerBitmap == null);


                    this.timeLineData.Frames = pd.TimelineData.Frames;
                    this.timeLineData.Frames[i].LayerList = pd.TimelineData.Frames[i].LayerList;
                    Console.WriteLine("________________________________________________");
                    Console.WriteLine(this.timeLineData.Frames[i].LayerList[j] == null);
                    Console.WriteLine(this.timeLineData.Frames[i].LayerList[j].LayerBytemap.Length);


                    SKBitmap layerImage = Serializer.ByteArrayToSkBitmap(this.timeLineData.Frames[i].LayerList[j].LayerBytemap);
                    this.timeLineData.Frames[i].LayerList[j].LayerBitmap = layerImage;



                    //timeLineData.Frames[i].LayerList.Add(pd.TimelineData.Frames[i].LayerList[j]);

                    //timeLineData.Frames[i].LayerList[j].LayerBitmap =
                    //Serializer.ByteArrayToSkBitmap(timeLineData.Frames[i].LayerList[j].LayerBytemap);
                }
            }

            FrameCountChanged?.Invoke(this, EventArgs.Empty);
        }

        public void SetCanvas(Canvas canvas)
        {
            this.canvas = canvas;
        }

        private void Timeline_Load(object sender, EventArgs e)
        {
        }

        public void UpdateTimeLine(TimelineData td, ProjectData pd)
        {

            label1.Text = (td.CurrentFrameIndex + 1).ToString();
            timeLineData = td;
            projectData = pd;

            if (timelinePanel != null)
            {
                foreach (Button item in timelinePanel.Controls)
                {
                    int number;
                    int.TryParse(item.Text, out number);

                    if (timeLineData.CurrentFrameIndex == number - 1)
                    {
                        item.BackColor = Color.LightBlue;
                    }
                    else
                    {
                        item.BackColor = Color.White;

                    }

                }

            }


        }

        private void button1_Click(object sender, EventArgs e)
        {
            if(timeLineData != null)
            {
                timeLineData.AddFrame(projectData.CanvasWidth, projectData.CanvasHeight);
                timeLineData.CurrentFrameIndex = timeLineData.Frames.Count - 1;
                UpdateTimeLine(timeLineData, projectData);
                canvas.UpdateCanvas(timeLineData);

                FrameCountChanged?.Invoke(this, EventArgs.Empty);
            }

        }
    }
}
