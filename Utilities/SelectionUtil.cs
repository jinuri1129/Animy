using SkiaSharp;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Animy.Utilities
{
    public static class SelectionUtil
    {
        public static SKRect GetBoundingBox(List<Stroke> strokes , int offset= 0)
        {
            // 초기값을 큰 값으로 설정하여 최소값을 찾을 수 있도록 한다.
            int minX = int.MaxValue, minY = int.MaxValue;
            int maxX = int.MinValue, maxY = int.MinValue;

            // 모든 Stroke를 순회하여 점들을 가져온다.
            foreach (var stroke in strokes)
            {
                // 최소값, 최대값을 갱신한다.
                minX = Math.Min(minX, (int)stroke.position.X) - offset;
                minY = Math.Min(minY, (int)stroke.position.Y) - offset;
                maxX = Math.Max(maxX, (int)stroke.position.X) + offset;
                maxY = Math.Max(maxY, (int)stroke.position.Y) + offset;
            }

            // 최소값과 최대값을 사용해 가장 작은 사각형을 구한다.
            return new SKRect(minX, minY, maxX, maxY);

            //return new SKRect(left, top,right, bottom);
        }
    }
}
