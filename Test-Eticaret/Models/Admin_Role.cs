using System.ComponentModel.DataAnnotations.Schema;

namespace Test_Eticaret.Models
{
    public class Admin_Role
    {
        [Key]
        public int role_id { get; set; } 
        public string? role {  get; set; }

        [ForeignKey("Admin")]
        public int admin_id { get; set; }
        public required Admin Admin { get; set; } // Navigation Property
        public required ICollection<Admin> Admins { get; set; } // One-to-Many Relationship
    }
}
