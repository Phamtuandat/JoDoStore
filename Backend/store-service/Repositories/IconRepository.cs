using App.Data;
using App.Models.ProductModel;
using Microsoft.EntityFrameworkCore;

namespace App.Repositories
{
      public class IconRepository : GenericRepository<Icon>
      {
            public IconRepository(DataContext context) : base(context)
            {
            }
      }
}