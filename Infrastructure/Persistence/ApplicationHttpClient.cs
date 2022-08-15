using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Persistence
{
    public class ApplicationHttpClient : HttpClient
    {
        private string Url = "https://localhost:7060/Api/Category";

        public ApplicationHttpClient (string BaseUrl)
        {
            this.Url = BaseUrl;
        }
    }
}
