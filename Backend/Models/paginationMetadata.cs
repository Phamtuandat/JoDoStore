namespace App.Models.ProductModel
{
      public class PaginationMetadata
      {
            public int CurrentPage { get; set; }
            public int PageSize { get; set; }
            public int TotalItems { get; set; }
            public int TotalPages { get; set; }
      }

}