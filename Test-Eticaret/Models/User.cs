
namespace Test_Eticaret.Models

{
    public class User
    {
        [Key]
        public int user_id { get; set; }
        public string? first_name { get; set; }
        public string? last_name { get; set; }
        public int tel_no { get; set; }
        public string? user_email { get; set; }
        public string? user_password { get; set; }

    }
}
