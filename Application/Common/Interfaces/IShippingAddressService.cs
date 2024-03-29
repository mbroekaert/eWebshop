﻿using Shared.Contracts.Response;

namespace Application.Common.Interfaces
{
    public interface IShippingAddressService
    {
        Task<ShippingAddressResponseDto[]> GetShippingAddressAsync(string userId);
        Task<(bool success, string content)> CreateShippingAddressAsync(Domain.Entities.ShippingAddress shippingAddress);
        Task<ShippingAddressResponseDto> EditShippingAddressAsync(int id);
        Task<(bool success, string content)> UpdateShippingAddressAsync(Domain.Entities.ShippingAddress shippingAddress);
        Task<ShippingAddressResponseDto> GetShippingAddressToDeleteAsync(int id);
        Task<(bool success, string content)> DeleteShippingAddressAsync(Domain.Entities.ShippingAddress shippingAddress);
        Task<ShippingAddressResponseDto> GetShippingAddressById(int shippingAddressId);
    }
}
