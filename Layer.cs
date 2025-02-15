using SkiaSharp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace Animy
{

    [DataContract]
    public  class Layer
    {
        [DataMember]
        public string Name { get; set; }
        [DataMember]

        public int frameIndex;
        [DataMember]
        public bool Visibility { get; set; }
        public SKBitmap LayerBitmap { get; set; }

        [DataMember]
        public byte[] LayerBytemap { get; set; }

        //Todo: blending mode 

        public Layer(string layerName, SKBitmap layerBitmap, int frameIndex, bool visibility = true) 
        {
            Name = layerName;
            LayerBitmap = layerBitmap;
            this.frameIndex = frameIndex;   
            Visibility = visibility;

             LayerBytemap = Serializer.SKBitmapToByteArray(layerBitmap);
        }



       
    }
}
