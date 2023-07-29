using Domain.Entities;
using Shared.Contracts.Response;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Common.Interfaces
{
    public interface IBillingAddressService
    {
        Task<BillingAddressResponseDto[]> GetBillingAddressAsync();
        Task<(bool success, string content)> CreateBillingAddressAsync(Domain.Entities.BillingAddress BillingAddress);
        Task<BillingAddressResponseDto> EditBillingAddressAsync(int id);
        Task<(bool success, string content)> UpdateBillingAddressAsync(Domain.Entities.BillingAddress BillingAddress);
        Task<BillingAddressResponseDto> GetBillingAddressToDeleteAsync(int id);
        Task<(bool success, string content)> DeleteBillingAddressAsync(Domain.Entities.BillingAddress BillingAddress);
    }
}
