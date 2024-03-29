﻿using App.Dtos;
using App.Models.ProductModel;
using App.Services.ProductServices;
using MediatR;

namespace App.Queries
{
      public class GetCategoryByIdQuery : IRequest<Category>
      {
            public int Id { get; set; }
            public class GetCategoryByIdQueryHandler : IRequestHandler<GetCategoryByIdQuery, Category>
            {
                  private readonly ICategoryService _categoryService;
                  public GetCategoryByIdQueryHandler(ICategoryService categoryService)
                  {
                        _categoryService = categoryService;
                  }

#pragma warning disable CS1998 // Async method lacks 'await' operators and will run synchronously
                  public async Task<Category> Handle(GetCategoryByIdQuery request, CancellationToken cancellationToken)
#pragma warning restore CS1998 // Async method lacks 'await' operators and will run synchronously
                  {
                        return _categoryService.GetById(request.Id);
                  }
            }
      }
}
