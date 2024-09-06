using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BankMegaTechTest_FE.Models
{
	public class LocationDTO
	{
        public string LocationName { get; set; } = String.Empty;
        public string LocationId { get; set; } = String.Empty;
    }
}

