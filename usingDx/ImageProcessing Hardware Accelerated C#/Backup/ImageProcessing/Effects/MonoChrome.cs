using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.DirectX.Direct3D ;
using System.Drawing;
namespace ImageProcessing.Effects
{
    public class MonoChromeEffect :ShaderEffect
    {

        public EffectHandle FilterColor;
        public MonoChromeEffect(Device device)
            : base(device, "MonoChrome")
        {
            FilterColor = _InternalEffect.GetParameter(null, "FilterColor");
        }

        public override void SetData(EffectData data)
        {
            for (int i = 0; i < data.Data.Count(); i++)
            {
                switch (data.Data[i].Key)
                {
                    case "filtercolor":
                        _InternalEffect.SetValue(FilterColor, (float[])data.Data[i].Value);
                        break;
                }
            }

            _InternalEffect.CommitChanges();

        }

        public static EffectData GetData(Color filtercolor)
        {
            List<KeyValuePair<string, object>> dt = new List<KeyValuePair<string, object>>();
            dt.Add(new KeyValuePair<string, object>("filtercolor", new float[] { filtercolor.R / 255f, filtercolor.G / 255f, filtercolor.B / 255f, filtercolor.A / 255f }));

            return new EffectData("MonoChrome", dt.ToArray());
        }



    }
}
