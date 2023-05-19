﻿using Backend.Models.Identity;
using CloudinaryDotNet.Actions;
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

        public async Task<AddressBook> CreateAddressAsync(SaveAddressResource address, User user)
        {
            try
            {
                var newAddress = new AddressBook()
                {
                    User = user,
                    District = address.District,
                    Province = address.Province,
                    Address = address.Address,
                    Name = address.Name,
                    Ward = address.Ward,
                    PhoneNumber = address.PhoneNumber,
                };
                _unitOfWork.AddressRepository.Add(newAddress);
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
                var address = _unitOfWork.AddressRepository.Get(id);
                if (address == null) throw new Exception("could not found address");
                _unitOfWork.AddressRepository.Delete(address);
                await _unitOfWork.CompleteAsync();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public AddressBook GetAddressById(int id)
        {
            try
            {
                var address = _unitOfWork.AddressRepository.Get(id);
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
            var list = _unitOfWork.AddressRepository.Find(addr => addr.User.Id == userId)?.ToList();
            if (list == null) return null;
            IEnumerable<AddressResource> addressList = list.Select(a => new AddressResource()
            {
                District = a.District,
                Province = a.Province,
                Ward = a.Ward,
                Id = a.Id,
                Address = a.Address,
                Name = a.Name,
                PhoneNumber = a.PhoneNumber,
            });
            return addressList;


        }

        public async Task<AddressBook> UpdateAddressAsync(int id, SaveAddressResource mode, string userId)
        {
            try
            {
                var address = _unitOfWork.AddressRepository.All().FirstOrDefault(x => x.Id == id);
                if (address == null) throw new Exception("Could not found address");
                var isEditale = address.User.Id == userId;
                if (!isEditale) throw new Exception("You can not update address!");
                address.Province = mode.Province;
                address.Address = mode.Address;
                address.District = mode.District;
                address.Name = mode.Name;
                address.Ward = mode.Ward;
                address.PhoneNumber = mode.PhoneNumber;
                var addressUpdated = _unitOfWork.AddressRepository.Update(address);
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
