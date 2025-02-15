using SkiaSharp;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace Animy
{
    public static class Serializer
    {
        public static void ToFile<T>(T instance, string path)
        {
            try
            {
                using var fs = new FileStream(path, FileMode.Create);
                var serializer = new DataContractSerializer(typeof(T));
                serializer.WriteObject(fs, instance);
            }
            catch (Exception e)
            {
                Debug.WriteLine(e.Message);
                throw;
            }
        }

        public static T FromFile<T>(string path)
        {
            try
            {
                using var fs = new FileStream(path, FileMode.Open);
                var serializer = new DataContractSerializer(typeof(T));
                T instance = (T)serializer.ReadObject(fs);
                return instance;
            }
            catch (Exception e)
            {
                Debug.WriteLine(e.Message);
                throw;
            }
        }
        public static void UpdateNodeValue(string filePath, string nodeName, string newValue)
        {
            // 파일이 존재하는지 확인
            if (!File.Exists(filePath))
            {
                Console.WriteLine("Serializer.UpdateNodeValue() : File does not exist.");
                return;
            }

            try
            {
                // XML 파일 로드
                var xmlDoc = XDocument.Load(filePath);

                // 지정된 노드 이름 검색 후 첫 번째 노드 선택
                var targetElement = xmlDoc.Descendants()
                                           .FirstOrDefault(el => el.Name.LocalName == nodeName);

                if (targetElement != null)
                {
                    // 값 업데이트
                    targetElement.Value = newValue;
                    xmlDoc.Save(filePath); // 변경된 XML 저장
                    Console.WriteLine($"Serializer.UpdateNodeValue() : Node '{nodeName}' updated successfully.");
                }
                else
                {
                    Console.WriteLine($"Serializer.UpdateNodeValue() : No node named '{nodeName}' found.");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Serializer.UpdateNodeValue() : Error updating node: {ex.Message}");
            }
        }





        public static byte[] SKBitmapToByteArray(SKBitmap skBitmap)
        {
            using (var image = SKImage.FromBitmap(skBitmap))
            using (var data = image.Encode(SKEncodedImageFormat.Png, 100)) // PNG 형식으로 인코딩 (100% 품질)
            {
                return data.ToArray();
            }
        }


        public static SKBitmap ByteArrayToSkBitmap(byte[] byteArray)
        {
            if (byteArray == null || byteArray.Length == 0)
            {
                throw new ArgumentException("Byte array is null or empty.");
            }

            using (var stream = new SKMemoryStream(byteArray))
            {
                var bitmap = SKBitmap.Decode(stream);
                if (bitmap == null)
                {
                    throw new InvalidOperationException("Failed to decode byte array to SKBitmap.");
                }
                return bitmap;
            }
            
        }

    }

}
