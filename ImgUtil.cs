using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SkiaSharp;
namespace Animy
{
    public static class ImgUtil
    {

        public static SKBitmap MergeImages(SKBitmap skbitmap1, SKBitmap skbitmap2)
        {

            // 병합 결과를 저장할 캔버스 생성
            int mergedWidth = Math.Max(skbitmap1.Width, skbitmap2.Width);
            int mergedHeight = skbitmap1.Height + skbitmap2.Height;

            using var mergedBitmap = new SKBitmap(mergedWidth, mergedHeight);
            using var canvas = new SKCanvas(mergedBitmap);

            // 캔버스를 흰색으로 초기화
            canvas.Clear(SKColors.White);

            // 첫 번째 이미지를 (0, 0) 위치에 그리기
            canvas.DrawBitmap(skbitmap1, 0, 0);

            // 두 번째 이미지를 첫 번째 이미지 아래로 그리기
            canvas.DrawBitmap(skbitmap2, 0, skbitmap1.Height);

            // 캔버스 해제
            canvas.Flush();

            return mergedBitmap;
        }

    }
}
