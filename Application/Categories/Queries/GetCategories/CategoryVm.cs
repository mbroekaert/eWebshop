using Shared.Contracts.Response;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Categories.Queries.GetCategories
{ /* to delete */
    public class CategoryVm
    {
        public IList<CategoryResponseDto> Lists { get; set; }
    }
}
