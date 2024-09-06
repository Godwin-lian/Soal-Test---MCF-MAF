using System;
namespace BankMegaTechTest.Models.DTO
{
    public class InsertBpkbRequestDTO
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
        public string CreatedBy { get; set; } = string.Empty;
        public DateTime CreatedOn { get; set; }
        public string LastUpdatedBy { get; set; } = string.Empty;
        public DateTime? LastUpdatedOn { get; set; }
        public long userId { get; set; }

    }

    public class InsertBpkbResponseDTO
    {
        public bool Success { get; set; }
        public string Message { get; set; } = string.Empty;
    }

}

