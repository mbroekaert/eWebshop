using Domain.Entities;
using Shared.Contracts.Response;


namespace Application.Common.Interfaces
{
    public interface ICategoryService
    {
        Task<CategoryResponseDto[]> GetCategoriesAsync();
        Task<(bool success, string content)> CreateCategoryAsync(Category category);
        Task<CategoryResponseDto> EditCategoryAsync(int id);
        Task<(bool success, string content)> UpdateCategoryAsync(Category category);
        Task<CategoryResponseDto> GetCategoryToDeleteAsync(int id);
        Task<(bool success, string content)> DeleteCategoryAsync(Category category);

    }
}
