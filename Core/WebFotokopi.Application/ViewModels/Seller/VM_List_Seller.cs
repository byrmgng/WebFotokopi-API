using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebFotokopi.Application.ViewModels.Seller
{
    public class VM_List_Seller
    {
        public string SellerName { get; set; }
        public string PaperSizeID { get; set; }
        public string PaperTypeID { get; set; }
        public string SheetsPerPageID { get; set; }
        public string DuplexMode { get; set; }
        public string ColorMode { get; set; }
    }
}
