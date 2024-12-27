namespace Test_Eticaret.Models
{
    public class Admin
    {
        [Key]
        public int admin_id { get; set; }
        public string? admin_password { get; set; }
        public string? admin_email { get; set; }
        public ICollection<Admin_Role> Admin_Roles { get; set; }
    }
}