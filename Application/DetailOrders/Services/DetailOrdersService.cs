using Application.Common.Interfaces;
using Domain.Entities;
using Shared.Contracts.Response;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace Application.DetailOrders.Services
{
    public class DetailOrdersService : IDetailOrdersService
    {
        private readonly HttpClient _httpClient;

        public DetailOrdersService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<DetailOrdersResponseDto[]> GetDetailsOrder(int orderId)
        {
            if (orderId == null || orderId <= 0)
            {
                return null;
            }
            var httpResponse = await _httpClient.GetAsync($"detailOrder/{orderId}");
            var responseAsString = await httpResponse.Content.ReadAsStringAsync();
            return JsonSerializer.Deserialize<DetailOrdersResponseDto[]>(responseAsString);
        }

        public async Task<(bool success, string content)> DeleteDetailsOrder(int orderId)
        {
            var httpResponse = await _httpClient.DeleteAsync($"detailOrder/{orderId}");
            if (httpResponse.IsSuccessStatusCode)
            {
                return (true, "Details from the order deleted successfully");
            }
            return (false, await httpResponse.Content.ReadAsStringAsync());
        }
    }
}
