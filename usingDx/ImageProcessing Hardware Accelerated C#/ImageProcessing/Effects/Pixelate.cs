using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.DirectX.Direct3D;
using System.Drawing;

namespace ImageProcessing.Effects
{
    public class PixelateEffect : ShaderEffect
    {
        public EffectHandle PixelCounts;
        public EffectHandle BrickOffset;

        public PixelateEffect(Device device)
            : base(device, "Pixelate")
        {
            PixelCounts = _InternalEffect.GetParameter(null, "PixelCounts");
            BrickOffset = _InternalEffect.GetParameter(null, "BrickOffset");

        }


        public override void SetData(EffectData data)
        {
            for (int i = 0; i < data.Data.Count(); i++)
            {
                switch (data.Data[i].Key)
                {
                    case "pixelcounts":
                        _InternalEffect.SetValue(PixelCounts, (float[])data.Data[i].Value);
                        break;
                    case "brickoffset":
                        _InternalEffect.SetValue(BrickOffset, (float)data.Data[i].Value);
                        break;
                }
            }

            _InternalEffect.CommitChanges();

        }


        public override void Begin()
        {
            base.Begin();

        }

        public static EffectData GetData(PointF pixelcounts, float brickoffset)
        {
            List<KeyValuePair<string, object>> dt = new List<KeyValuePair<string, object>>();
            dt.Add(new KeyValuePair<string, object>("pixelcounts", new float[] { pixelcounts.X, pixelcounts .Y}));
            dt.Add(new KeyValuePair<string, object>("brickoffset", brickoffset));

            return new EffectData("Pixelate", dt.ToArray());
        }

    }
}

