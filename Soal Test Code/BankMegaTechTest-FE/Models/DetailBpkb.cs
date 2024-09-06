using System;
namespace BankMegaTechTest_FE.Models
{
	public class DetailBpkb
	{
        public List<LocationDTO> Locations { get; set; } = new List<LocationDTO>();
        public TrBpkbDTO Bpkb { get; set; } = new();
    }

    public class TrBpkbDTO
    {
        public string AgreementNumber { get; set; } = string.Empty;
        public string BpkbNo { get; set; } = string.Empty;
        public string BranchId { get; set; } = string.Empty;
        public DateTime BpkbDate { get; set; }
        public string FakturNo { get; set; } = string.Empty;
        public DateTime? FakturDate { get; set; }
        public string LocationId { get; set; } = string.Empty;
        public string PoliceNo { get; set; } = string.Empty;
        public DateTime? BpkbDateIn { get; set; }
        public long UserId { get; set; }
    }

}

