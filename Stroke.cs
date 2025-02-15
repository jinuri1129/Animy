using SkiaSharp;
using System;
using System.Drawing;

public class Stroke
{
    public SKPoint position;
    public float Pressure { get; }
    public int BrushSize { get; private set; }
    public SKColor Color { get; }

    public Stroke(SKPoint pos , float pressure, float penPressureMax ,SKColor color)
    {
        position = pos;
        Pressure = pressure;
        Color = color;
        BrushSize = (int)PressureToSize(pressure,penPressureMax,1,10);
    }


    float PressureToSize(float penPressure, float penPressureMax, float minSize, float maxSize)
    {
        // 압력을 0~1 범위로 정규화
        float normalizedPressure = penPressure / penPressureMax;

        // 최소와 최대 브러시 크기 사이에서 크기 계산
        float brushSize = minSize + (maxSize - minSize) * normalizedPressure;

        return brushSize;
    }

}
