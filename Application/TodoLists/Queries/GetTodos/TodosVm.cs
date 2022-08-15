using System.Collections.Generic;

namespace Application.TodoLists.Queries.GetTodos
{
    public class TodosVm
    {
        public IList<TodoListDto> Lists { get; set; }
    }
}
