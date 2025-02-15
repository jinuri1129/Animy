using SkiaSharp;
using SkiaSharp.Views.Desktop;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;

namespace Animy
{

    
    public abstract class Command
    {
        public abstract void Execute(); // 실행
        public abstract void Undo();    // 취소
        public abstract void Redo();
    }
    public class HistoryManager
    {
        public bool isNeedToSave = false;
        private List<Command> redoList = new List<Command>();
        private List<Command> undoList = new List<Command>();
        public void Execute(Command command)
        {

            command.Execute();
            undoList.Add(command);
            redoList.Clear();  // Clear redo list after a new command is executed
            Console.WriteLine(@$"redo: {redoList.Count} undo: {undoList.Count}");
            isNeedToSave = true;
        }

        public void Undo()
        {

            if (undoList.Count > 0)
            {
                var command = undoList[undoList.Count - 1];
                command.Undo();
                undoList.RemoveAt(undoList.Count - 1);  // Remove the last command after undoing it
                redoList.Add(command);  // Add it to the redo list
                Console.WriteLine(@$"redo: {redoList.Count} undo: {undoList.Count}");
                isNeedToSave = true;
            }
        }

        public void Redo()
        {

            if (redoList.Count > 0)
            {
                var command = redoList[redoList.Count - 1];
                command.Execute();
                redoList.RemoveAt(redoList.Count - 1);  // Remove the last command after redoing it
                undoList.Add(command);  // Add it to the undo list
                Console.WriteLine(@$"redo: {redoList.Count} undo: {undoList.Count}");
                isNeedToSave = true;
            }

        }
    }



    public class DrawCommand : Command
    {
        public List<Stroke> strokes;
        public SKBitmap orgbitmap;
        public SKBitmap bitmap;
        public SKGLControl glControl;
        private SKBitmap _orgSnapshot;
        private SKBitmap _newSnapshot;
        private SKRect _rect;

        public DrawCommand(List<Stroke> strokes, SKBitmap orgbitmap , SKBitmap bitmap, SKGLControl sKGLControl)
        {
            this.strokes = strokes;
            this.bitmap = bitmap;
            this.orgbitmap = orgbitmap; 
            this.glControl = sKGLControl;
        }

        public override void Execute()
        {
            _rect = Utilities.SelectionUtil.GetBoundingBox(strokes,2);
            _orgSnapshot = SavePixelsForUndoRedo(_rect, orgbitmap);

            Console.WriteLine(strokes.Count);
            using (var canvas = new SKCanvas(bitmap))
            using (var paint = new SKPaint { Color = SKColors.Black, StrokeWidth = 3, IsAntialias = false })
            {
                // 모든 연속된 점을 선으로 연결 (보간)
                for (int i = 1; i < strokes.Count; i++)
                {
                    paint.Color = strokes[i].Color;
                    paint.StrokeWidth = strokes[i].BrushSize;

                    SKPoint p1 = strokes[i - 1].position;
                    SKPoint p2 = strokes[i].position;

                    // 두 점 사이를 채우는 선형 보간 (점 간격이 클 경우 빈 공간 방지)
                    float dx = p2.X - p1.X;
                    float dy = p2.Y - p1.Y;
                    float distance = (float)Math.Sqrt(dx * dx + dy * dy);
                    int steps = (int)(distance / 2); // 2픽셀 간격으로 보간 (간격 조절 가능)

                    for (int j = 0; j <= steps; j++)
                    {
                        float t = (float)j / steps;
                        float x = p1.X + t * dx;
                        float y = p1.Y + t * dy;
                        canvas.DrawCircle(x, y, paint.StrokeWidth, paint); // 점을 찍어 보간
                    }
                }
            }

           // _rect = Utilities.SelectionUtil.GetBoundingBox(strokes);
            _newSnapshot = SavePixelsForUndoRedo(_rect, bitmap);
        }

        public override void Undo()
        {


            Console.WriteLine(strokes.Count);
            // Iterate through the rect's dimensions and set pixel colors in the bitmap
            for (int x = 0; x < _rect.Width; x++)
            {
                for (int y = 0; y < _rect.Height; y++)
                {
                    // Map the (x, y) position relative to the rect's top-left corner in the bitmap
                    int bitmapX = (int)(_rect.Left + x);
                    int bitmapY = (int)(_rect.Top + y);

                    // Set the pixel color at the mapped position in the bitmap
                    SKColor color = Utilities.Util.GetPixelColor(_newSnapshot, x, y);
                    if (color.Alpha != 0) // 색이 있으면
                    {
                        SKColor orgColor = Utilities.Util.GetPixelColor(_orgSnapshot, x, y);
                        // bitmap에 색상 설정
                        Utilities.Util.SetPixelColor(bitmap, bitmapX, bitmapY, orgColor.Red, orgColor.Green, orgColor.Blue, orgColor.Alpha);
                    }

                    //Utilities.Util.SetPixelColor(bitmap, bitmapX, bitmapY, color.Red, color.Green, color.Blue, 255); // Red color
                   // Utilities.Util.SetPixelColor(bitmap, bitmapX, bitmapY, 255, 255, 0, 255); // Red color
                }
            }


            /*
            for(int x = 0; x < bitmap.Width; x++)
                for (int y = 0; y < bitmap.Height; y++)
                {
                    //white to transparent
                    if(Utilities.Util.GetPixelColor(bitmap,x,y) == white)
                    {
                        Utilities.Util.SetPixelColor(bitmap, x, y, 0, 0, 0, 0);
                    }
                }
            */
            glControl.Invalidate();
            Console.WriteLine("undo action");
            Console.WriteLine(_newSnapshot.IsNull);
        }

        public override void Redo()
        {
            LoadBitmapPixels(_rect, _newSnapshot, bitmap);
            glControl.Invalidate();
            Console.WriteLine("redo action");
            Console.WriteLine(_newSnapshot.IsNull);
        }


        // Undo/Redo 기능을 위한 픽셀 저장
        private SKBitmap SavePixelsForUndoRedo(SKRect rect, SKBitmap bitmap)
        {
            // 캔버스에서 지정된 영역을 가져옵니다.

            SKBitmap snapshot = new SKBitmap((int)rect.Width, (int)rect.Height);
            using (var canvas = new SKCanvas(snapshot))
            {
                // 영역을 잘라서 그리기 (좌표계에 맞게 오프셋을 적용)
                canvas.DrawBitmap(bitmap, -rect.Left, -rect.Top);
            }
            return snapshot;
        }

        private void LoadBitmapPixels(SKRect rect, SKBitmap snapshot, SKBitmap bitmap)
        {
            
            // Iterate through the rect's dimensions and set pixel colors in the bitmap
            for (int x = 0; x < rect.Width; x++)
            {
                for (int y = 0; y < rect.Height; y++)
                {
                    // Map the (x, y) position relative to the rect's top-left corner in the bitmap
                    int bitmapX = (int)(rect.Left + x);
                    int bitmapY = (int)(rect.Top + y);

                    // Set the pixel color at the mapped position in the bitmap
                    Utilities.Util.SetPixelColor(bitmap, bitmapX, bitmapY, 255, 0, 0, 255); // Red color
                }
            }
            using (var canvas = new SKCanvas(bitmap))
            {
                // Draw the snapshot into the bitmap at the position defined by the rect
                canvas.DrawBitmap(snapshot, rect.Left, rect.Top);
            }


        }


    }

    public class TextChangeCommand : Command
    {
        private string _originalText;
        private string _newText;
        private Action<string> _applyText;

        public TextChangeCommand(string originalText, string newText, Action<string> applyText)
        {
            _originalText = originalText;
            _newText = newText;
            _applyText = applyText;
        }

        // Execute the change
        public override void Execute()
        {
            _applyText(_newText); // Apply the new text
        }

        // Undo the change by reverting to the original text
        public override void Undo()
        {
            _applyText(_originalText); // Restore the original text
        }

        // Redo the change by applying the new text again
        public override void Redo()
        {
            _applyText(_newText); // Reapply the new text
        }
    }



}
