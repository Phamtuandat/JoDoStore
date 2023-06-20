using System.Text;

namespace App.Models
{
      public class Elfinder
      {
            public string ElfinderId { get; set; }
            public string InputId { get; set; }
            public string? ImageListId { get; set; }
            public Elfinder(string elfinderId, string inputId, bool loadLib = true, string isMutiple = "true", int hieght = 450, string lang = "en", int width = 840)
            {
                  ElfinderId = elfinderId;
                  Lang = lang;
                  LoadLib = loadLib;
                  Hieght = hieght;
                  InputId = inputId;
                  Width = width;
                  IsMutiple = isMutiple;
            }
            public string Lang { get; set; }
            public int Hieght { get; set; }
            public bool LoadLib { get; set; }
            public int Width { get; set; }
            public string IsMutiple { get; set; }



      }
}