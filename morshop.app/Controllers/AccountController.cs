using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using morshop.app.EmailServices;
using morshop.app.Identity;
using morshop.app.Models;
using morshop.business.Abstract;
using morshop.entity;

namespace morshop.app.Controllers
{
    //[ValidateAntiForgeryToken]//Bu özellik formun post methodlarına karşı bir savunma işlemidir bunu aktif ettiğimizde formlara özel benzersiz bir token oluşturur ve o token sunucudaki token ile eşeşiyor mu kontrol eder.
    //Cross-Site Request Forgery (CSRF) saldırılarına karşı koruma sağlar.
    public class AccountController:Controller
    {
        private readonly UserManager<AppUser> _userManager;
        private readonly SignInManager<AppUser> _signInManager;
        private readonly IEmailSender _emailSender;
        private readonly ICardService _cardService;

        public AccountController(UserManager<AppUser> UserManager, SignInManager<AppUser> signInManager,IEmailSender emailSender, ICardService cardService)
        {
            _userManager=UserManager;
            _signInManager=signInManager;
            _emailSender=emailSender;
            _cardService=cardService;
        }
        public IActionResult Login(string? ReturnUrl)
        {
            return View(new LoginModel()
            {
                ReturnUrl=ReturnUrl
            });
        }
        [HttpPost]
        public async Task<IActionResult> Login(LoginModel loginModel)
        {
            if(!ModelState.IsValid)
            {
                return View(loginModel);
            }
            // var user = await _userManager.FindByNameAsync(loginModel.UserName);
            var user = await _userManager.FindByEmailAsync(loginModel.Email); //bu maile ait user varmı?
            if(user==null)
            {
                ModelState.AddModelError("","Girilen kullanıcı adının kayıdı bulunmuyor!");
                return View(loginModel);
            }

            if(!await _userManager.IsEmailConfirmedAsync(user))
            {
                ModelState.AddModelError("","Girilen kullanıcının email adresi onaylanmamış! Lütfen email hesabınıza gelen link ile üyeliğinizi onaylayın..");
                return View(loginModel);
            }

            var result = await _signInManager.PasswordSignInAsync(user,loginModel.Password,true,false); //giriş yap
            if(result.Succeeded)
            {                
                return Redirect(loginModel.ReturnUrl??"/");
            }
            else
            {
                ModelState.AddModelError("","Kullanıcı adı veya şifre yanlış!");
            }
            
            return View(loginModel);
        }
        public async Task<IActionResult> Logout()
        {
            await _signInManager.SignOutAsync();
            return Redirect("~/");
        }

        public IActionResult Register()
        {
            return View("Register");
        }

        [HttpPost]
        public async Task<IActionResult> Register(RegisterModel registerModel)
        {
            if(!ModelState.IsValid)
            {
                return View("Register",registerModel);
            }
            var appUser = new AppUser()
            {
                FirstName=registerModel.FirstName,
                LastName=registerModel.LastName,
                UserName=registerModel.UserName,
                Email=registerModel.Email
            };
            var result= await _userManager.CreateAsync(appUser,registerModel.Password);//kullanıcıyı veritabanına ekler
            if(result.Succeeded)//işlem başarılı mı?
            {
                
                var card=new Card()
                {
                    UserId=appUser.Id,
                };
                _cardService.Create(card);
                // await _userManager.AddToRoleAsync(appUser,"customer");
                 //generate token
                var code = await _userManager.GenerateEmailConfirmationTokenAsync(appUser);
                var url=Url.Action("ConfirmEmail","Account",new{
                    userId=appUser.Id,
                    token=code
                });
                //email

                await _emailSender.SenderEmailAsync(appUser.Email,"MorShop.com",$"Hesabınızı onaylamak için <a href='https://localhost:7040{url}'>tıklayınız.</a>");
        
                return RedirectToAction("Login","Account");
            }
            Console.WriteLine(result);
            
            ModelState.AddModelError("Password","En az 6 karakter olmalı! Özel Karakter ve Sayı İçermelidir! Büyük ve küçük karakter içermelidir!");
            ModelState.AddModelError("Email","Bu mail zaten kayıtlı!");
            ModelState.AddModelError("UserName","Bu kullanıcı adı zaten kayıtlı!");
            return View("Register",registerModel);
        }

        public async Task<IActionResult> ConfirmEmail(string userId, string token)
        {
            if(userId==null||token==null)
            {
                ViewBag.Message="Geçersiz Link";
                return View();
            }
            var user = await _userManager.FindByIdAsync(userId);
            if(user==null)
            {
                ViewBag.Message="Geçersiz Link";
                return View();
            }
            var result = await _userManager.ConfirmEmailAsync(user,token);
            if(result.Succeeded)
            {
                ViewBag.Message="Tebrikler Hesabınız Onaylandı!";
                return View();
            }
            ViewBag.Message="Hesabınız Onaylanmadı!";
            return View();
        }
        public IActionResult ForgotPassword()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> ForgotPassword(string email)
        {
            if(email==null)
            {
                return View();
            }
            var user = await _userManager.FindByEmailAsync(email);
            if(user==null)
            {
                return View();
            }
            var _token = await _userManager.GeneratePasswordResetTokenAsync(user);
            var url=Url.Action("ResetPassword","Account",new {
                userId=user.Id,
                token=_token
            });
            await _emailSender.SenderEmailAsync(user.Email,"MorShop.com / Şifre yenileme!",$"Şifreni yenilemek için <a href='https://localhost:7040{url}'>tıklayınız.</a>");
            return View();
        }

        public IActionResult ResetPassword(string userId,string token)
        {
            if(userId==null||token==null)
            {
                return RedirectToAction("Home","Index");
            }
            var model= new ResetPasswordModel(){Token=token,};
            return View("ResetPassword",model);
        }
        [HttpPost]
        public async Task<IActionResult> ResetPassword(ResetPasswordModel resetPasswordModel)
        {
            if(!ModelState.IsValid)
            {
                return View(resetPasswordModel);
            }
            var user = await _userManager.FindByEmailAsync(resetPasswordModel.Email);
            if(user==null)
            {
                return View(resetPasswordModel);
            }
            var result = await _userManager.ResetPasswordAsync(user,resetPasswordModel.Token,resetPasswordModel.Password);
            if(result.Succeeded)
            {
                return RedirectToAction("Login","Account");
            }
            return View(resetPasswordModel);
        }
    }
}