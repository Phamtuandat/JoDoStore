using Backend.Models.Identity;
using gearshop_dotnetapp.Models.Identity;
using gearshop_dotnetapp.Resources;

namespace gearshop_dotnetapp.Services.OrderServices
{
    public interface IAddredssService
    {
        AddressBook GetAddressById(int id);
        Task<AddressBook> CreateAddressAsync(SaveAddressResource address, User user);
        Task<AddressBook> UpdateAddressAsync(int id, SaveAddressResource address);
        Task DeleteAddressAsync(int id);
        IEnumerable<AddressResource>? GetAddressByUserIdAsync(string userId);
    }
}
