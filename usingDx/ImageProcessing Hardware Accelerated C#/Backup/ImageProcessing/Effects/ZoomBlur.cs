using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Collections;
using System.Data;
using System.Diagnostics;
using Microsoft.DirectX.Direct3D;
using System.Drawing;

namespace ImageProcessing.Effects
{
    
    public class ZoomBlurEffect : ShaderEffect
    {

        public EffectHandle Center;

        public EffectHandle Amount;
       
        public ZoomBlurEffect(Device device)
            : base(device, "ZoomBlur")
        {

            Center = _InternalEffect.GetParameter(null, "center");
            Amount = _InternalEffect.GetParameter(null, "progress");

            this._InternalEffect.SetValue(Center, new float[] {0.5f,0.5f});

        }

        public override void SetData(EffectData data)
        {
            for (int i = 0; i < data.Data.Count() ; i++)
            {
                switch (data.Data[i].Key)
                {
                    case "amount":
                        _InternalEffect.SetValue(Amount, (float)data.Data[i].Value);
                        break;
                    case "center":
                        _InternalEffect.SetValue(Center,(float[]) data.Data[i].Value);
                        break;
                }
            }

            _InternalEffect.CommitChanges();

        }


        public override void Begin()
        {
            base.Begin();
           
        }

        public static EffectData GetData(float amount, PointF center)
        {
            List<KeyValuePair<string, object>> dt = new List<KeyValuePair<string, object>>();
            dt.Add(new KeyValuePair<string, object>("amount", amount));
            dt.Add ( new KeyValuePair<string,object>("center",new float[]{center.X,center.Y}));

            return new EffectData("ZoomBlur", dt.ToArray());
       }

    }
}
