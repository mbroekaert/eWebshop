//using Application.Common.Exceptions;
//using Application.Common.Interfaces;
//using Domain.Entities;
//using MediatR;

//namespace Application.Categories.Commands.UpdateCategory
//{
//    public class UpdateCategoryCommand : IRequest
//    {
//        public int Id { get; set; }
//        public string Name { get; set; }
//        public int DisplayOrder { get; set; }
        
//    }

//    public class UpdateCategoryCommandHandler : IRequestHandler<UpdateCategoryCommand>
//    {
//        private readonly IApplicationDbContext _context;

//        public UpdateTodoListCommandHandler(IApplicationDbContext context)
//        {
//            _context = context;
//        }

//        public async Task<Unit> Handle(UpdateCategoryCommand request, CancellationToken cancellationToken)
//        {
//            var entity = await _context.TodoLists.FindAsync(request.Id);
//            if (entity == null)
//            {
//                throw new NotFoundException(nameof(Category), request.Id);
//            }
//            entity.Name = request.Name;
//            await _context.SaveChangesAsync(cancellationToken);
//            return Unit.Value;
//        }
//    }
//}
