using System;
using System.IO;
using Newtonsoft.Json;

namespace GeoEditor
{
   static class JsonUtils
   {
      public static void Write()
      {
         File.WriteAllText("D:\\Nikon\\proging\\csharp\\GeoEditor\\settings.json", JsonConvert.SerializeObject(GE_ViewModel.DeskViewModel.Instance.Model));
      }

      public static void Read()
      {

      }
   }
}
