using AutoMapper;
using gearshop_dotnetapp.Models.Product;
using gearshop_dotnetapp.Repositories;
using gearshop_dotnetapp.Resources;
using gearshop_dotnetapp.Services.Communications;

namespace gearshop_dotnetapp.Services.ProductServices
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

        public async Task<BrandRes> CreateAsync(SaveBrandResource brand)
        {
            var existed = _uniOfWork.BrandRepository.Find(b => b.Name== brand.Name)?.FirstOrDefault();
            if (existed == null)
            {
                try
                {
                    var newBrand = new Brand() { Name = brand.Name, Description = brand.Description };
                    _uniOfWork.BrandRepository.Add(newBrand);
                    await _uniOfWork.CompleteAsync();
                    var brandResource = _mapper.Map<Brand, BrandResource>(newBrand);
                    return new BrandRes(brandResource);
                }
                catch (Exception ex)
                {

                    return new BrandRes($"Something went wrong while saving brand, please try again later! \n message:{ex.Message}");
                }
               
            };
            return new BrandRes("Brand is already existed!");
        }

        public async Task<BrandRes> DeleteAsync(int id)
        {
            var existed = _uniOfWork.BrandRepository.Get(id);
            if (existed == null) return new BrandRes("Id is invalid, please try again!");
            _uniOfWork.BrandRepository.Delete(existed);
            await _uniOfWork.CompleteAsync();
            return new BrandRes(true);
        }

        public  IEnumerable<BrandResource> FindByName(string name)
        {
            var brands = _uniOfWork.BrandRepository.Find(b => b.Name.Contains(name))?.ToList();
            if(brands != null)
            {
                var list = _mapper.Map<IEnumerable<Brand>, IEnumerable<BrandResource>>(brands);
                return list;
            }
            return new List<BrandResource>();
        }

        public IEnumerable<BrandResource> GetAll()
        {
            var list = _uniOfWork.BrandRepository.All();
            return _mapper.Map<IEnumerable<Brand>, IEnumerable<BrandResource>>(list);
        }

        public BrandRes GetById(int id)
        {
            var brand = _uniOfWork.BrandRepository.Get(id);
            if (brand == null) return new BrandRes("The id is invalid!0");
            var brandResource = _mapper.Map<Brand,BrandResource>(brand);
            return new BrandRes(brandResource);
        }

        public Task<BrandRes> UpdateAsync(SaveBrandResource brand, int id)
        {
            throw new NotImplementedException();
        }
    }
}
