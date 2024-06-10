using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebFotokopi.Application.DTOs.CityDTOs;
using WebFotokopi.Application.DTOs.DistrictDTOs;
using WebFotokopi.Application.DTOs.PaperSizeDTOs;
using WebFotokopi.Application.DTOs.PaperTypeDTOs;
using WebFotokopi.Application.DTOs.SheetsPerPageDTOs;
using WebFotokopi.Domain.Entities;

namespace WebFotokopi.Application.DTOs.PackageDTOs
{
    public class GetPackageFeaturesDTO
    {
        public IEnumerable<ListPaperSizeDTO> PaperSizes { get; set; }
        public IEnumerable<ListPaperTypeDTO> PaperTypes { get; set; }
        public IEnumerable<ListSheetsPerPageDTO> SheetsPerPages { get; set; }

    }
}
