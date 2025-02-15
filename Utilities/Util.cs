using Newtonsoft.Json;
using SkiaSharp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace Animy.Utilities
{
    public class Util
    {
        public static string OnlyNumber(string input)
        {
            // 정규식으로 숫자가 아닌 모든 문자를 제거
            return Regex.Replace(input, @"[^\d]", "");
        }

        public static string SafeWords(string input)
        {
            if (string.IsNullOrWhiteSpace(input))
                return string.Empty;

            // Windows 파일 경로에서 금지된 문자 정의
            string invalidChars = @"[\\/:*?""<>|]";

            // 정규식으로 금지된 문자 제거
            string sanitized = Regex.Replace(input, invalidChars, "");

            // 스페이스바를 _로 변환
            //sanitized = sanitized.Replace(" ", "_");

            return sanitized;
        }

        public static bool PathExists(string path)
        {
            // 입력 값이 null, 공백 또는 빈 문자열인지 확인
            if (string.IsNullOrWhiteSpace(path))
                return false;

            // 파일 또는 디렉터리가 존재하는지 확인
            return File.Exists(path) || Directory.Exists(path);
        }

        public static ProjectData ReadANPFile(string filePath)
        {
            // 파일 읽기
            //string jsonText = File.ReadAllText(filePath);

            var data = Serializer.FromFile<ProjectData>(filePath);

            // JSON 문자열을 C# 객체로 역직렬화
            //ProjectData data = JsonConvert.DeserializeObject<ProjectData>(jsonText);



            return data;

        }


        public static SKColor GetPixelColor(SKBitmap bitmap, int x, int y)
        {
            IntPtr addr = bitmap.GetPixels();
            int index = (y * bitmap.Width + x) * 4; // RGBA (4바이트) 포맷

            unsafe
            {
                byte* ptr = (byte*)addr;
                byte r = ptr[index];
                byte g = ptr[index + 1];
                byte b = ptr[index + 2];
                byte a = ptr[index + 3];


                //if (r == 0 && g == 0 && b == 0 && a == 0)
                  //  return new SKColor(255,255,255,255);
                
                return new SKColor(r, g, b, a);
            }
        }

        public static void SetPixelColor(SKBitmap bitmap, int x, int y, byte r, byte g, byte b, byte a)
        {
            IntPtr addr = bitmap.GetPixels();
            int index = (y * bitmap.Width + x) * 4; // RGBA (4바이트)

            unsafe
            {
                byte* ptr = (byte*)addr;
                ptr[index] = r;       // Red
                ptr[index + 1] = g;   // Green
                ptr[index + 2] = b;   // Blue
                ptr[index + 3] = a;   // Alpha
            }
        }

        public static float PressureToSize(float penPressure, float penPressureMax, float minSize, float maxSize)
        {
            // 압력을 0~1 범위로 정규화
            float normalizedPressure = penPressure / penPressureMax;

            // 최소와 최대 브러시 크기 사이에서 크기 계산
            float brushSize = minSize + (maxSize - minSize) * normalizedPressure;

            return brushSize;
        }
    }
}
