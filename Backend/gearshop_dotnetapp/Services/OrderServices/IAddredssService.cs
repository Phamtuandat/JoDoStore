using Backend.Models.Identity;
using gearshop_dotnetapp.Models.Identity;
using gearshop_dotnetapp.Resources;

namespace gearshop_dotnetapp.Services.OrderServices
{
    public interface IAddredssService
    {
        Address GetAddressById(int id);
        Task<Address> CreateAddressAsync(SaveAddressResource address, User user);
        Task<Address> UpdateAddressAsync(int id, SaveAddressResource address);
        Task DeleteAddressAsync(int id);
        IEnumerable<AddressResource>? GetAddressByUserIdAsync(string userId);
    }
}
