
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BankMegaTechTest.Models.Entities
{
    [Table("MS_STORAGE_LOCATION", Schema = "dbo")]
    public class MsStorageLocation
    {
        [Key]
        [Column("location_id")]
        [StringLength(10)]
        public string LocationId { get; set; }

        [Column("location_name")]
        [StringLength(100)]
        public string LocationName { get; set; }
    }
}    
