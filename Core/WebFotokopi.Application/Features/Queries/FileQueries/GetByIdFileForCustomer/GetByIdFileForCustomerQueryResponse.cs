using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebFotokopi.Application.Features.Queries.FileQueries.GetByIdFileForCustomer
{
    public class GetByIdFileForCustomerQueryResponse
    {
        public string FileID { get; set; }
        public string FileTitle { get; set; }
        public string FileNote { get; set; }
        public string FileContent { get; set; }
        public string ContentType { get; set; }
        public string FileName { get; set; }
    }
}
