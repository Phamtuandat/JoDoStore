namespace App.Models
{
      public class ProductQs
      {
            public int[]? ProductIds { get; set;}
            public string? Name { get; set; }
            public decimal? MinPrice { get; set; }
            public decimal? MaxPrice { get; set; }
            public int[]? CategoryIds { get; set; }
            public string? OrderBy { get; set; }
            public int[]? IConId { get; set; }
            public int PageSize { get; set; } = 50;
            public int CurrentPage { get; set; } = 1;
      }
}