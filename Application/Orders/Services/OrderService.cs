using Application.Common.Interfaces;
using Domain.Entities;
using Shared.Contracts.Response;
using System.Text;
using System.Text.Json;

namespace Application.Orders.Services
{
    public class OrderService : IOrderService
    {
        private readonly HttpClient _httpClient;
        private readonly IProductService _productService;
        private double totalAmount = 0;

        public OrderService (HttpClient httpClient, IProductService productService)
        {
            _httpClient = httpClient;
            _productService = productService;
        }

        public async Task<OrderResponseDto[]> GetOrdersAsync ()
        {
            var httpResponse = await _httpClient.GetAsync("order");
            var responseAsString = await httpResponse.Content.ReadAsStringAsync();
            return JsonSerializer.Deserialize<OrderResponseDto[]>(responseAsString);
        }

        public async Task<OrderResponseDto[]> GetCustomerOrders(string customerId)
        {
            var httpResponse = await _httpClient.GetAsync($"order/{customerId}");
            var responseAsString = await httpResponse.Content.ReadAsStringAsync();
            var temp = JsonSerializer.Deserialize<OrderResponseDto[]>(responseAsString);
            return JsonSerializer.Deserialize<OrderResponseDto[]>(responseAsString);
        }

        public async Task<(bool success, string content)> CreateOrder(Order order, List<int> productIds, List<int> quantities)
        {
            /* Calculate full amount of the order */

            totalAmount = await CalculateTotalAmount(productIds, quantities);
            order.OrderAmount = totalAmount;

            /* Create order */
            var content = JsonSerializer.Serialize(order);
            var httpResponse = await _httpClient.PostAsync("order", new StringContent(content, Encoding.Default, "application/json"));
            if (httpResponse.IsSuccessStatusCode)
            {
                /* Retrieve the orderId of the created order */

                var responseContent = await httpResponse.Content.ReadAsStringAsync();
                int orderId = JsonSerializer.Deserialize<int>(responseContent);
                order.OrderId = orderId;

                /* Create order detail - call the service */

                var httpResponseDetails = await CreateOrderDetail(order, productIds, quantities);
                                
                /* Update stock */

                for (int i = 0; i<productIds.Count;i++)
                {
                    // Retrieve the product
                    var product = await _productService.EditProductAsync(productIds[i]);
                    //Create a new product
                    Product newProduct = new Product
                    {
                        ProductId = product.ProductId,
                        ProductName = product.ProductName,
                        ProductReference = product.ProductReference,
                        ProductPrice = product.ProductPrice,
                        ProductQuantity = product.ProductQuantity - quantities[i],
                        CategoryId = product.CategoryId
                    };
                    // Update the product
                    var productResult = await _productService.UpdateProductAsync(newProduct);
                }
                return (true, "Order created successfully");
            }
            /* Delete order if not successfull */
            else
            {
                var deleteResult = await DeleteOrderAsync(order);
            }

            return (false, await httpResponse.Content.ReadAsStringAsync());

            
        }

        public async Task<(bool success, string content)> CreateOrderDetail(Order order, List<int> productIds, List<int> quantities)
        {
            bool success = true;
            for (int i = 0; i < productIds.Count; i++)
            {
                DetailOrder detailOrder = new DetailOrder()
                {
                    OrderId = order.OrderId,
                    ProductId = productIds[i],
                    Quantity = quantities[i]
                };
                var detailedContent = JsonSerializer.Serialize(detailOrder);
                var httpResponseSeparateDetails = await _httpClient.PostAsync("detailOrder", new StringContent(detailedContent, Encoding.Default, "application/json"));
                if (!httpResponseSeparateDetails.IsSuccessStatusCode)
                {
                    success = false;
                }
            }
            return (success, "All details inserted successfully!");
        }

        public async Task<(bool success, string content)> DeleteOrderAsync(Order order)
        {
            var httpResponse = await _httpClient.DeleteAsync($"order/{order.OrderId}");
            if (httpResponse.IsSuccessStatusCode)
            {
                return (true, "Product deleted successfully");
            }
            return (false, await httpResponse.Content.ReadAsStringAsync());
        }

        public async Task<double> CalculateTotalAmount(List<int> productIds, List<int> quantities)
        {
            double finalAmount = 0;
            for (int i = 0; i < productIds.Count; i++)
            {
                int productId = productIds[i];
                double productPrice = await _productService.GetProductPrice(productId);
                finalAmount = finalAmount + productPrice * quantities[i];
            }
            return finalAmount;

        }
        
    }
}
