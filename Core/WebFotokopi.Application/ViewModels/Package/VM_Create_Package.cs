using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebFotokopi.Application.ViewModels.Package
{
    public class VM_Create_Package
    {
        public required string Name { get; set; }
        public float Price { get; set; }
        public bool DuplexMode { get; set; }
        public bool ColorMode { get; set; }
        public string SheetsPerPageID { get; set; }
        public string PaperSizeID { get; set; }
        public string PaperTypeID { get; set; }
        public bool isActive { get; set; }
    }
}
