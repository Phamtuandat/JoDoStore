using Backend.Models.Identity;
using gearshop_dotnetapp.Models.Identity;
using gearshop_dotnetapp.Repositories;
using gearshop_dotnetapp.Resources;

namespace gearshop_dotnetapp.Services.OrderServices
{
    public class AddressService : IAddredssService
    {
        private readonly IUnitOfWork _unitOfWork;
        public AddressService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<Address> CreateAddressAsync(SaveAddressResource address, User user)
        {
            try
            {
                var newAddress = new Address()
                {
                    User = user,
                    City = address.City,
                    State = address.State,
                    StreetAddress = address.StreetAddress,
                };
                _unitOfWork.AdressRepository.Add(newAddress);
                await _unitOfWork.CompleteAsync();
                return newAddress;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task DeleteAddressAsync(int id)
        {
            try
            {
                var address = _unitOfWork.AdressRepository.Get(id);
                if (address != null) throw new Exception("could not found address");
                _unitOfWork.AdressRepository.Delete(address);
                await _unitOfWork.CompleteAsync();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public Address GetAddressById(int id)
        {
            try
            {
                var address = _unitOfWork.AdressRepository.Get(id);
                if (address == null) throw new Exception("could not found address");
                return address;
            }
            catch (Exception ex)
            {

                throw new Exception(ex.Message);
            }
        }

        public IEnumerable<AddressResource>? GetAddressByUserIdAsync(string userId)
        {
            var list = _unitOfWork.AdressRepository.Find(addr => addr.User.Id == userId)?.ToList();
            if(list == null) return null;
            IEnumerable<AddressResource> addressList = list.Select(a => new AddressResource()
            {
                City = a.City,
                State = a.State,
                Id= a.Id,
                StreetAddress = a.StreetAddress
            });
            return addressList;


        }

        public async Task<Address> UpdateAddressAsync(int id, SaveAddressResource mode)
        {
            try
            {
                var address = _unitOfWork.AdressRepository.Get(id);
                if (address == null) throw new Exception("Could not found address");
                address.State = mode.State;
                address.StreetAddress = mode.StreetAddress;
                address.City = mode.City;

                var addressUpdated = _unitOfWork.AdressRepository.Update(address);
                await _unitOfWork.CompleteAsync();
                return address;
            }
            catch (Exception ex)
            {

                throw new Exception(ex.Message);
            }
        }
    }
}
