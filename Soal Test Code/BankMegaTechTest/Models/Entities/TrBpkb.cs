using BankMegaTechTest.Models.Entities;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

[Table("TR_BPKB", Schema = "dbo")]
public class TrBpkb
{
    [Key]
    [Column("agreement_number")]
    [StringLength(100)]
    public string AgreementNumber { get; set; } = string.Empty;

    [Column("bpkb_no")]
    [StringLength(100)]
    public string BpkbNo { get; set; } = string.Empty;

    [Column("branch_id")]
    [StringLength(10)]
    public string BranchId { get; set; } = string.Empty;

    [Column("bpkb_date")]
    public DateTime BpkbDate { get; set; }

    [Column("faktur_no")]
    [StringLength(100)]
    public string FakturNo { get; set; } = string.Empty;

    [Column("faktur_date")]
    public DateTime? FakturDate { get; set; }

    [Column("location_id")]
    [StringLength(10)]
    public string LocationId { get; set; } = string.Empty;

    [ForeignKey("LocationId")]
    public MsStorageLocation MsStorageLocation { get; set; }

    [Column("police_no")]
    [StringLength(20)]
    public string PoliceNo { get; set; } = string.Empty;

    [Column("bpkb_date_in")]
    public DateTime? BpkbDateIn { get; set; }

    [Column("user_id")]
    public long? UserId { get; set; }

    [ForeignKey("UserId")]
    public MsUser MsUser { get; set; }
}
