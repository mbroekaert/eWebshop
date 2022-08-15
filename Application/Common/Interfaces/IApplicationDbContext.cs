using Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Application.Common.Interfaces
{
    public interface IApplicationDbContext
    {
        DbSet<TodoList> TodoLists { get; set; }
        DbSet<TodoItem> TodoItems { get; set; }
        DbSet<Category> Categories { get; set; }

        Task<int> SaveChangesAsync(CancellationToken cancellationToken);
    }
}
