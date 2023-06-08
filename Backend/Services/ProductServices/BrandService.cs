using AutoMapper;
using App.Models.ProductModel;
using App.Repositories;
using App.Dtos;

namespace App.Services.ProductServices
{
      public class BrandService : IBrandService
      {
            private readonly IUnitOfWork _uniOfWork;
            private readonly IMapper _mapper;

            public BrandService(IUnitOfWork uniOfWork, IMapper mapper)
            {
                  _uniOfWork = uniOfWork;
                  _mapper = mapper;
            }

            public Task CreateAsync(Brand brand)
            {
                  throw new NotImplementedException();
            }

            public Task DeleteAsync(int id)
            {
                  throw new NotImplementedException();
            }

            public IEnumerable<BrandDto> FindByName(string name)
            {
                  throw new NotImplementedException();
            }

            public IEnumerable<BrandDto> GetAll()
            {
                  throw new NotImplementedException();
            }

            public BrandDto GetById(int id)
            {
                  throw new NotImplementedException();
            }

            public Task UpdateAsync(Brand brand, int id)
            {
                  throw new NotImplementedException();
            }
      }
}
