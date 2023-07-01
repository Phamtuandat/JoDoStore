namespace App.Models
{
      public class Summernote
      {
            public Summernote(string idEditor, bool loadLib = true, int hieght = 120)
            {
                  IDEditor = idEditor;
                  LoadLib = loadLib;
                  Hieght = hieght;
            }
            public string IDEditor { get; set; }
            public int Hieght { get; set; }
            public bool LoadLib { get; set; }
            public string ToolBar { get; set; } = @"[
                ['style', ['style']],
                ['font', ['bold', 'underline', 'clear']],
                ['color', ['color']],
                ['para', ['ul', 'ol', 'paragraph']],
                ['table', ['table']],
                ['insert', ['link', 'picture', 'video', 'elfinder']],
                ['view', ['fullscreen', 'codeview', 'help']]
            ]";

      }
}