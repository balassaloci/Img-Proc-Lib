using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Collections;
using System.Data;
using System.Diagnostics;
namespace ImageProcessing
{
   
    public struct EffectData
    {

        public string EffectName;

        public KeyValuePair<string, object>[] Data;
        public EffectData(string effectName, KeyValuePair<string, object>[] data)
        {
            this.EffectName = effectName;
            this.Data = data;
        }

        //public static KeyValuePair<string, object>[] GetPairs(object[] arr)
        //{
        //    List<KeyValuePair<string, object>> res = new List<KeyValuePair<string, object>>();

        //    for (int i = 0; i < arr.Length;i+=2 )
        //    {
        //        res.Add(new KeyValuePair<string, object> ((string)arr[i],(object)arr[i+1]));
        //    }
        //    return res.ToArray();
        //}
    }
}
