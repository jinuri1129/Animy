using SkiaSharp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;
using static Animy.Canvas;

namespace Animy
{
    [DataContract]
    public class TimelineData
    {
        public TimelineData()
        {

        }

        [DataMember]
        public List<Frame> Frames { get; set; }
        [DataMember]
        public int CurrentFrameIndex { get; set; }

       // [DataMember]
        //public List<byte[]> ByteMaps { get; set; }


        public void SetCurrentFrameIndex(int index)
        {
            CurrentFrameIndex = index;
        }

        /*
        public void FramesToByte()
        {
            ByteMaps = new List<byte[]>();
            foreach (var frame in Frames)
            {
                ByteMaps.Add(frame.SKBitmapToByteArray());

                if(frame.LayerList == null)
                {
                    frame.LayerList = new List<Layer>();
                }
                frame.LayerList.Add(new Layer("test2", frame.Bitmap, -1, false));
            }

        }
        */


        /*
        public void ByteToFrames()
        {
            Frames = new List<Frame>();
            foreach (var bytes in ByteMaps)
            {
                Frames.Add(new Frame(bytes));
            }
        }
        */

        public void AddFrame(int width, int height)
        {
            if(Frames == null)
            {
                Frames = new List<Frame>();
            }
            Frame newFrame = new Frame();
            if(newFrame.LayerList == null)
            {
                newFrame.LayerList = new List<Layer>();
            }

            newFrame.LayerList.Add(new Layer("new Layer", new SKBitmap(width, height), Frames.Count));
            Frames.Add(newFrame);
        }

        public void DeleteCurrentFrame()
        {
            if (Frames.Count > 1)
            {
                Frames.RemoveAt(CurrentFrameIndex);
                CurrentFrameIndex = Math.Max(0, CurrentFrameIndex - 1);
            }
        }

        public Frame GetCurrentFrame()
        {
            if (CurrentFrameIndex > Frames.Count)
                CurrentFrameIndex = Frames.Count - 1;
            if (CurrentFrameIndex < 0)
                CurrentFrameIndex = 0;
           
            return Frames[CurrentFrameIndex];
        }
    }

    [DataContract]
    public class Frame
    {
        [DataMember]
        public List<Layer> LayerList { get; set; }
        
        public Frame() {  }
     
    }
}

