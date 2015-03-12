using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.DirectX.Direct3D;
using ImageProcessing;

namespace Test
{
    class ScaleExtended : ImageProcessing.ShaderEffect
    {

        #region effect running on GPU
  

        static string effectstring = @"


sampler2D input : register(s0);

float scalex : register(C0);
float scaley : register(c1);

float centerx : register(C2);
float centery : register(c3);
float4 main(float2 uv : TEXCOORD) : COLOR 
{ 
	

	float4 color; 
	
	uv.x -= centerx;
	uv.y -= centery;
	
	uv.x /= scalex;
	uv.y /= scaley;
	
	uv.x += centerx;
	uv.y += centery;
	
	if (uv.x < 0 || uv.y < 0 || uv.x > 1 || uv.y > 1 )
	color = 0 ;
	else
	color= tex2D( input , uv.xy); 


	return color; 
}

technique Tech
{
	pass Pss
	{
	pixelshader = compile ps_2_0 main();
	}
}


";
        #endregion

        public EffectHandle scalex, scaley, centerx, centery;

        internal void loadEffectIfNotLoaded()
        {
            if (InternalEffect != null) return ;
            string errs = "";
            this.SetInternalEffect((Effect)Microsoft.DirectX.Direct3D.Effect.FromString(this.Device, effectstring, (Include)null, ShaderFlags.None, (EffectPool)null, out errs));
                if (errs.Length > 0)
                {
                    System.Windows.Forms.MessageBox.Show(errs);
                    return ;
                }
                scalex = InternalEffect.GetParameter(null,"scalex");
                scaley = InternalEffect.GetParameter(null, "scaley");
                centerx = InternalEffect.GetParameter(null,"centerx");
                centery = InternalEffect.GetParameter(null, "centery");
             
        }

        public ScaleExtended(Device device)
            : base(device)
        {
            
        }

        public override void Begin()
        {
            loadEffectIfNotLoaded();
            base.Begin();

        }

        public override void SetData(ImageProcessing.EffectData data)
        {
            loadEffectIfNotLoaded();

            for (int i = 0; i < data.Data.Length; i++)
            {
                switch (data.Data[i].Key)
                {
                    case "scalex":
                        InternalEffect.SetValue(scalex , (float) data.Data[i].Value);
                        break;
                    case "scaley":
                        InternalEffect.SetValue(scaley, (float)data.Data[i].Value);
                        break;
                    case "centerx":
                        InternalEffect.SetValue(centerx, (float)data.Data[i].Value);
                        break;
                    case "centery":
                        InternalEffect.SetValue(centery, (float)data.Data[i].Value);
                        break;

                }
            }
        }

        public static EffectData GetData(float scalex, float scaley, float centerx, float centery)
        {
            List<KeyValuePair<string, object>> dt = new List<KeyValuePair<string, object>>();
            dt.Add(new KeyValuePair<string, object>("scalex", scalex));
            dt.Add(new KeyValuePair<string, object>("scaley", scaley));
            dt.Add(new KeyValuePair<string, object>("centerx", centerx));
            dt.Add(new KeyValuePair<string, object>("centery", centery));

            return new EffectData("ScaleExtended", dt.ToArray());

        }
    }
}
