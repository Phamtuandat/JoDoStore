using App.Models.ProductModel;
using App.Repositories;

namespace App.Services.ProductServices
{
      public class IConService : IIconService
      {
            private readonly IUnitOfWork _unitOfWOrk;

            public IConService(IUnitOfWork unitOfWOrk)
            {
                  _unitOfWOrk = unitOfWOrk;
            }

            public async Task CreateAsync(Icon icon)
            {
                  _unitOfWOrk.IconRepository.Add(icon);
                  await _unitOfWOrk.CompleteAsync();
            }

            public async Task DeleteAsync(int id)
            {
                  var icon = _unitOfWOrk.IconRepository.Get(id);
                  if (icon == null) throw new ArgumentNullException();
                  _unitOfWOrk.IconRepository.Delete(icon);
                  await _unitOfWOrk.CompleteAsync();
            }

            public IEnumerable<Icon> FindByName(string name)
            {
                  return _unitOfWOrk.IconRepository.Find(i => i.Name == name).ToList();
            }

            public List<Icon> GetAll()
            {
                  return _unitOfWOrk.IconRepository.All().ToList();
            }

            public Icon? GetById(int id)
            {
                  return _unitOfWOrk.IconRepository.Get(id);
            }

            public async Task UpdateAsync(Icon icon)
            {
                  _unitOfWOrk.IconRepository.Update(icon);
                  await _unitOfWOrk.CompleteAsync();
            }
      }
}