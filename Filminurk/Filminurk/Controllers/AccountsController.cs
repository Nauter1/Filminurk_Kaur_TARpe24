using Filminurk.Core.Domain;
using Filminurk.Core.ServiceInterface;
using Filminurk.Data;
using Filminurk.Models.Accounts;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Org.BouncyCastle.Bcpg;

namespace Filminurk.Controllers
{
    public class AccountsController : Controller
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly SignInManager<ApplicationUser> _signInManager;
        private readonly FilminurkTARpe24Context _context;
        private readonly IEmailsServices _emailsServices;  //HOMEWORK LOCATION ???

        public AccountsController(UserManager<ApplicationUser> userManager, SignInManager<ApplicationUser> signInManager, FilminurkTARpe24Context context, IEmailsServices emailsServices)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _context = context;
            _emailsServices = emailsServices;
        }
        [HttpGet]
        public IActionResult Register()
        {
            return View();
        }
        [HttpPost]
        [AllowAnonymous]
        public async Task<IActionResult> Register(RegisterViewModel model)
        {
            if (ModelState.IsValid)
            {
                var user = new ApplicationUser()
                {
                    UserName = model.DisplayName,
                    Email = model.Email,
                    ProfileType = model.ProfileType,
                };
                var result = await _userManager.CreateAsync(user, model.Password);
                if (result.Succeeded)
                { 
                    var token = await _userManager.GenerateEmailConfirmationTokenAsync(user);
                    var confirmationLink = Url.Action("ConfirmEmail", "Accounts", new { userID = user.Id, token = token }, Request.Scheme);
                    // HOMEWORK TASK: Koosta email kasutajalt pärineva aadressile saatmiseks, kasutaja saab oma postkastist kätte emaili
                    // kinituslingiga, mille jaoks kasutatakse tokenit. siin tuleb välja kutsuda vastav, uus, emaili saatmise meetod, mis saadab
                    // õige sisuga kirja
                }
                return RedirectToAction("Index","Home");
            }
        }
    }
