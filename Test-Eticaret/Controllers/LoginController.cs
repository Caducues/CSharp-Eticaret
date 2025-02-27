using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Text;
using Test_Eticaret.Data;
using Test_Eticaret.Models;
using XSystem.Security.Cryptography;

namespace Test_Eticaret.Controllers
{
    public class LoginController : Controller
    {

        private readonly ApplicationDbContext _context;
        public LoginController(ApplicationDbContext context)
        {
            _context = context;
        }

        public IActionResult login()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> login(Models.User usermodel)
        {
            using (var client = new HttpClient())
            {
                MD5 md5 = new MD5CryptoServiceProvider();

                //compute hash from the bytes of text  
                md5.ComputeHash(ASCIIEncoding.ASCII.GetBytes(usermodel.user_password));

                //get hash result after compute it  
                byte[] result = md5.Hash;

                StringBuilder strBuilder = new StringBuilder();
                for (int i = 0; i < result.Length; i++)
                {
                    //change it into 2 hexadecimal digits  
                    //for each byte  
                    strBuilder.Append(result[i].ToString("x2"));
                }

                var response = await client.PostAsync("http://localhost:8000/login?email=" + usermodel.user_email + "&password=" + strBuilder, null);
                var contents = await response.Content.ReadAsStringAsync();
                if (contents.Length < 3)
                {
                    ModelState.AddModelError("", "Geçersiz kullanıcı adı veya şifre.");
                    return View(usermodel);
                }
                else if (usermodel.user_email == "admin@admin.com" && usermodel.user_password == "123456")
                {
                    string[] parse = contents.Split('"');

                    TempData["Message"] = "Welcome " + usermodel.user_email; // tempdata kullanımı


                    return RedirectToAction("index", "Admin", new { name = parse[3] });



                   
                }



                else
                {

                    string[] parse = contents.Split('"');

                    TempData["Message"] = "Welcome " + usermodel.user_email;



                    return RedirectToAction("index", "Website", new { name = parse[3] });
                }


            }
            return View();
        }
        [HttpGet]
        public IActionResult Signup()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Signup(User user)
        {
            if (ModelState.IsValid == false)
            {
                try
                {
                    // Email'in benzersiz olduğunu kontrol et
                    var existingUser = await _context.Users
                                                     .FirstOrDefaultAsync(u => u.user_email == user.user_email);

                    if (existingUser != null)
                    {
                        ModelState.AddModelError("user_email", "Bu e-posta adresi zaten kullanılmakta.");
                        return View(user); // Hata varsa tekrar aynı sayfaya dön
                    }

                    if (user.user_password == null || user.user_password == string.Empty)
                    {
                        ModelState.AddModelError("user_email", "Bu e-posta adresi zaten kullanılmakta.");
                        return View(user); // Hata varsa tekrar aynı sayfaya dön
                    }


                    MD5 md5 = new MD5CryptoServiceProvider();

                    //compute hash from the bytes of text  
                    md5.ComputeHash(ASCIIEncoding.ASCII.GetBytes(user.user_password));

                    //get hash result after compute it  
                    byte[] result = md5.Hash;

                    StringBuilder strBuilder = new StringBuilder();
                    for (int i = 0; i < result.Length; i++)
                    {
                        //change it into 2 hexadecimal digits  
                        //for each byte  
                        strBuilder.Append(result[i].ToString("x2"));
                    }

                    user.user_password = strBuilder.ToString();

                    // Yeni kullanıcıyı ekle
                    _context.Users.Add(user);
                    await _context.SaveChangesAsync();

                    return RedirectToAction("Index", "Website");
                }
                catch (Exception ex)
                {
                    // Hata durumunda log yazdırma
                    Console.WriteLine($"Veritabanı hatası: {ex.Message}");
                    ModelState.AddModelError("", "Kullanıcı kaydederken bir hata oluştu.");
                    return View(user); // Hata durumunda tekrar aynı sayfaya dön
                }
            }

            // Eğer model geçerli değilse, hata mesajlarını göster
            foreach (var error in ModelState.Values.SelectMany(v => v.Errors))
            {
                Console.WriteLine(error.ErrorMessage);
            }

            return View(user);
        }

    }
}
