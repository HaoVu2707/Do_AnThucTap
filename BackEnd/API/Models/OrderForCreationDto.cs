using API.Infrastructure;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace API.Models
{
    public class OrderForCreationDto : BaseDto
    {

        public List<ServiceForPackageDto> ServiceList { get; set; }
    }
}
