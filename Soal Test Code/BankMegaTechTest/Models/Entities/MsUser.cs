using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BankMegaTechTest.Models.Entities
{
    [Table("MS_USER", Schema = "dbo")]
    public class MsUser
    {
        [Key]
        [Column("user_id")]
        public long UserId { get; set; }

        [Column("user_name")]
        [StringLength(20)]
        public string UserName { get; set; }

        [Column("password")]
        [StringLength(50)]
        public string Password { get; set; }

        [Column("is_active")]
        public bool IsActive { get; set; }
    }
}