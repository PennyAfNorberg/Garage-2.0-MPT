using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Security.Claims;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Garage_2._0_MPT.Utils;

namespace Garage_2._0_MPT.Models
{
    public class ParkedVehiclesController : Controller
    {
        private readonly Garage_2_0_MPTContext _context;
        private ParkHouse parkhouse;
        private bool loadedSeed = false;
        private readonly UserManager<GarageUser> userManager;
        private readonly IUserClaimsPrincipalFactory<GarageUser> claimsPrincipalFactory;

        public ParkedVehiclesController(Garage_2_0_MPTContext context,
            UserManager<GarageUser> userManager,
            IUserClaimsPrincipalFactory<GarageUser> claimsPrincipalFactory)
        {
            _context = context;
            this.userManager = userManager;
            this.claimsPrincipalFactory = claimsPrincipalFactory;
            int Floor = 3;
            int[] Twos = new int[3]
                { 2,3,2
                };
            int[] Threes = new int[3]
                    { 3,2,3
                    };

            parkhouse = new ParkHouse(Floor, Twos, Threes, _context);
             InitPlots();
        }

        [HttpGet]
        public async Task<IActionResult> Register()
        {
            var res = new RegisterModel
            {
                Members = (await _context.Members.ToListAsync())
        };

            return View(res);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Register(RegisterModel model)
        {
            if (ModelState.IsValid)
            {
                var user = await userManager.FindByNameAsync(model.UserName);

                if (user == null)
                {
                    user = new GarageUser
                    {
                        Id = Guid.NewGuid().ToString(),
                        UserName = model.UserName,
                        Email = model.UserName,
                       MemberId=model.MemberId,

                    };

                    var result = await userManager.CreateAsync(user, model.Password);

                    if (result.Succeeded)
                    {
                        var token = await userManager.GenerateEmailConfirmationTokenAsync(user);
                        var confirmationEmail = Url.Action("ConfirmEmailAddress", "ParkedVehicles",
                            new { token = token, email = user.Email }, Request.Scheme);
                        System.IO.File.WriteAllText("d:\\kod\\tmp\\confirmationLink.txt", confirmationEmail);
                    }
                    else
                    {
                        foreach (var error in result.Errors)
                        {
                            ModelState.AddModelError("", error.Description);
                        }

                        return View();
                    }
                }

                return View("Success");
            }

            return View();
        }
        [HttpGet]
        public async Task<IActionResult> ConfirmEmailAddress(string token, string email)
        {
            var user = await userManager.FindByEmailAsync(email);

            if (user != null)
            {
                var result = await userManager.ConfirmEmailAsync(user, token);

                if (result.Succeeded)
                {
                    return View("Success");
                }
            }

            return View("Error");
        }

        [HttpGet]
        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Login(LoginModel model)
        {
            if (ModelState.IsValid)
            {
                var user = await userManager.FindByNameAsync(model.UserName);

                if (user != null && !await userManager.IsLockedOutAsync(user))
                {
                    if (await userManager.CheckPasswordAsync(user, model.Password))
                    {
                        if (!await userManager.IsEmailConfirmedAsync(user))
                        {
                            ModelState.AddModelError("", "Email is not confirmed");
                            return View();
                        }

                        await userManager.ResetAccessFailedCountAsync(user);

                        if (await userManager.GetTwoFactorEnabledAsync(user))
                        {
                            var validProviders =
                                await userManager.GetValidTwoFactorProvidersAsync(user);

                            if (validProviders.Contains(userManager.Options.Tokens.AuthenticatorTokenProvider))
                            {
                                await HttpContext.SignInAsync(IdentityConstants.TwoFactorUserIdScheme,
                                    Store2FA(user.Id, userManager.Options.Tokens.AuthenticatorTokenProvider));
                                return RedirectToAction("TwoFactor");
                            }

                            if (validProviders.Contains("Email"))
                            {
                                var token = await userManager.GenerateTwoFactorTokenAsync(user, "Email");
                                System.IO.File.WriteAllText("d:\\kod\\tmp\\email2sv.txt", token);

                                await HttpContext.SignInAsync(IdentityConstants.TwoFactorUserIdScheme,
                                    Store2FA(user.Id, "Email"));
                                return RedirectToAction("TwoFactor");
                            }
                        }

                        var principal = await claimsPrincipalFactory.CreateAsync(user);

                        await HttpContext.SignInAsync(IdentityConstants.ApplicationScheme, principal);

                        return RedirectToAction("Index");
                    }

                    await userManager.AccessFailedAsync(user);

                    if (await userManager.IsLockedOutAsync(user))
                    {
                        // email user, notifying them of lockout
                    }
                }

                ModelState.AddModelError("", "Invalid UserName or Password");
            }

            return View();
        }

        private ClaimsPrincipal Store2FA(string userId, string provider)
        {
            var identity = new ClaimsIdentity(new List<Claim>
            {
                new Claim("sub", userId),
                new Claim("amr", provider)
            }, IdentityConstants.TwoFactorUserIdScheme);

            return new ClaimsPrincipal(identity);
        }

        [HttpGet]
        public IActionResult ForgotPassword()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> ForgotPassword(ForgotPasswordModel model)
        {
            if (ModelState.IsValid)
            {
                var user = await userManager.FindByEmailAsync(model.Email);

                if (user != null)
                {
                    var token = await userManager.GeneratePasswordResetTokenAsync(user);
                    var resetUrl = Url.Action("ResetPassword", "ParkedVehicles",
                        new { token = token, email = user.Email }, Request.Scheme);

                    System.IO.File.WriteAllText("d:\\kod\\tmp\\resetLink.txt", resetUrl);
                }
                else
                {
                    // email user and inform them that they do not have an account
                }

                return View("Success");
            }
            return View();
        }

        [HttpGet]
        public IActionResult ResetPassword(string token, string email)
        {
            return View(new ResetPasswordModel { Token = token, Email = email });
        }

        [HttpPost]
        public async Task<IActionResult> ResetPassword(ResetPasswordModel model)
        {
            if (ModelState.IsValid)
            {
                var user = await userManager.FindByEmailAsync(model.Email);

                if (user != null)
                {
                    var result = await userManager.ResetPasswordAsync(user, model.Token, model.Password);

                    if (!result.Succeeded)
                    {
                        foreach (var error in result.Errors)
                        {
                            ModelState.AddModelError("", error.Description);
                        }
                        return View();
                    }

                    if (await userManager.IsLockedOutAsync(user))
                    {
                        await userManager.SetLockoutEndDateAsync(user, DateTimeOffset.UtcNow);
                    }
                    return View("Success");
                }
                ModelState.AddModelError("", "Invalid Request");
            }
            return View();
        }

        [HttpGet]
        public IActionResult TwoFactor()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> TwoFactor(TwoFactorModel model)
        {
            var result = await HttpContext.AuthenticateAsync(IdentityConstants.TwoFactorUserIdScheme);
            if (!result.Succeeded)
            {
                ModelState.AddModelError("", "You login request has expired, please start over");
                return View();
            }

            if (ModelState.IsValid)
            {
                var user = await userManager.FindByIdAsync(result.Principal.FindFirstValue("sub"));

                if (user != null)
                {
                    var isValid = await userManager.VerifyTwoFactorTokenAsync(user,
                        result.Principal.FindFirstValue("amr"), model.Token);

                    if (isValid)
                    {
                        await HttpContext.SignOutAsync(IdentityConstants.TwoFactorUserIdScheme);

                        var claimsPrincipal = await claimsPrincipalFactory.CreateAsync(user);
                        await HttpContext.SignInAsync(IdentityConstants.ApplicationScheme, claimsPrincipal);

                        return RedirectToAction("Index");
                    }

                    ModelState.AddModelError("", "Invalid token");
                    return View();
                }

                ModelState.AddModelError("", "Invalid Request");
            }

            return View();
        }

        [HttpGet]
        [Authorize]
        public async Task<IActionResult> RegisterAuthenticator()
        {
            var user = await userManager.GetUserAsync(User);

            var authenticatorKey = await userManager.GetAuthenticatorKeyAsync(user);

            if (authenticatorKey == null)
            {
                await userManager.ResetAuthenticatorKeyAsync(user);
                authenticatorKey = await userManager.GetAuthenticatorKeyAsync(user);
            }

            return View(new RegisterAuthenticatorModel { AuthenticatorKey = authenticatorKey });
        }

        [HttpPost]
        [Authorize]
        public async Task<IActionResult> RegisterAuthenticator(RegisterAuthenticatorModel model)
        {
            var user = await userManager.GetUserAsync(User);

            var isValid = await userManager.VerifyTwoFactorTokenAsync(user,
                userManager.Options.Tokens.AuthenticatorTokenProvider, model.Code);

            if (!isValid)
            {
                ModelState.AddModelError("", "Code is invalid");
                return View(model);
            }

            await userManager.SetTwoFactorEnabledAsync(user, true);
            return View("Success");
        }

        [HttpGet]
        public IActionResult ExternalLogin(string provider)
        {
            var properties = new AuthenticationProperties
            {
                RedirectUri = Url.Action("ExternalLoginCallback"),
                Items = { { "scheme", provider } }
            };
            return Challenge(properties, provider);
        }

        [HttpGet]
        public async Task<IActionResult> ExternalLoginCallback()
        {
            var result = await HttpContext.AuthenticateAsync(IdentityConstants.ExternalScheme);

            var externalUserId = result.Principal.FindFirstValue("sub")
                                 ?? result.Principal.FindFirstValue(ClaimTypes.NameIdentifier)
                                 ?? throw new Exception("Cannot find external user id");
            var provider = result.Properties.Items["scheme"];

            var user = await userManager.FindByLoginAsync(provider, externalUserId);

            if (user == null)
            {
                var email = result.Principal.FindFirstValue("email")
                            ?? result.Principal.FindFirstValue(ClaimTypes.Email);
                if (email != null)
                {
                    user = await userManager.FindByEmailAsync(email);

                    if (user == null)
                    {
                        user = new GarageUser { UserName = email, Email = email };
                        await userManager.CreateAsync(user);
                    }

                    await userManager.AddLoginAsync(user,
                        new UserLoginInfo(provider, externalUserId, provider));
                }
            }

            if (user == null) return View("Error");

            await HttpContext.SignOutAsync(IdentityConstants.ExternalScheme);

            var claimsPrincipal = await claimsPrincipalFactory.CreateAsync(user);
            await HttpContext.SignInAsync(IdentityConstants.ApplicationScheme, claimsPrincipal);

            return RedirectToAction("Index");
        }

        private ParkingsHouseStatusViewModel GetParkingsHouseStatus()
        {

            var work = parkhouse.GetNextFreeSpaces();
            ParkingsHouseStatusViewModel svar = new ParkingsHouseStatusViewModel();
            foreach (var item in work)
            {
                svar.NextFree[TranslateSize(item.Key)] = item.Value == null ? "Full" : item.Value.ToString();
            }

            return svar;

        }

        private string TranslateSize(int Size)
        {
            switch (Size)
            {
                case -3:
                    return "MC";
                case 1:
                    return "Car";
                case 2:
                    return "Truck";
                case 3:
                    return "Bus";
                default:
                    return "";
            }
        }

        private async Task InitPlots()
        {
            if (!loadedSeed)
            {

                var res = (await AddTimeAndPrice());
                if (res != null)
                {


                    var res2 = res.Select(o => o.ParkedVehicles.Where(pw => pw.ParkedVehicle.Where == null))

                     .Select(p => p.Select(pw => pw.ParkedVehicle))
                    .ToList();

                    var needtosavetoo = new List<ParkedVehicle>();

                    foreach (var item in res2)
                    {
                        foreach (var item2 in item)
                        {
                            parkhouse.Park(item2);
                            needtosavetoo.Add(item2);
                        }

                    }


                    try
                    {
                        _context.ParkedVehicle.UpdateRange(needtosavetoo);
                        _context.SaveChanges();
                    }
                    catch (DbUpdateConcurrencyException)
                    {

                        throw;
                    }
                    loadedSeed = true;
                }
            }
        }


        // GET: ParkedVehicles

        public async Task<IActionResult> Index()
        {
            var res = await AddTimeAndPrice();
        

            var svar = new ListViewModel
            {
                ParkedViewModel = (IEnumerable<ParkedViewModel>)res,
                Message = "Parked Vehicles"

            };

            return View(svar);
        }



        // GET: ParkedVehicles
        [Authorize]
        public async Task<IActionResult> Overview()
        {
            var svar2 = parkhouse.GetFreeSpaces();
            var res = await AddTimeAndPrice(true);
            var id = userManager.GetUserId(User);
            var curruser= await userManager.FindByIdAsync(id);
            var res3

            var role = await userManager.GetRolesAsync(curruser);

            var svar = new ListViewModel
            {
                ParkedViewModel = res
            };

            return View(svar);
        }



        private string PrettyPrintTime(TimeSpan? timespan)
        {
            if (timespan == null)
                throw new ArgumentNullException();

            if (timespan.Value.Days > 0)
            {
                return $"{timespan.Value.Days} d " + PrettyPrintTime(timespan - timespan.Value.Days * new TimeSpan(1, 0, 0, 0));
            }
            else
            {

                return $"{timespan.Value.Hours:D2}:{timespan.Value.Minutes:D2}:{timespan.Value.Seconds:D2}";

            }


        }


        public async Task<ActionResult> VehicleDetails(int? vehicleid)
        {

            if (vehicleid == null)
            {
                return NotFound();
            }

            var res = _context.Vehicles
                .Include(v => v.VehicleTyp)
                .Include(v => v.Member)
                .Where(v => v.Id == vehicleid);

            if (res.Count() == 0)
            {
                return NotFound();
            }

            var svar = new SingelViewModel
            {
                ParkedVehicle = new ParkedViewModel
                {
                    //        ParkedVehicles = res.
                    //   Select(o => o.ParkedVehicles).Select(pw => pw.Where(pwm => pwm.ParkedVehicle.Id == id))
                    //   .FirstOrDefault().ToList(),
                    Vehicle = await res.FirstOrDefaultAsync()
                ,
                    VehicleTyp = await res.Select(v => v.VehicleTyp).FirstOrDefaultAsync()
                ,
                    Member = await res.Select(v => v.Member).FirstOrDefaultAsync()
                }

            };
            svar.ParkedVehicle.ParkedVehicles = new List<SubParkedViewModel>
            {
             new SubParkedViewModel{
                 ParkedTime =null,
                 ParkedVehicle= new ParkedVehicle
                 {
                     ParkInDate=null,
                     Where=null

                 }
             }

            };



            return View("Details", svar);
        }

        // GET: ParkedVehicles/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var res = (await AddTimeAndPrice(true));



            var svar = new SingelViewModel
            {
                ParkedVehicle = new ParkedViewModel
                {
                    //        ParkedVehicles = res.
                    //   Select(o => o.ParkedVehicles).Select(pw => pw.Where(pwm => pwm.ParkedVehicle.Id == id))
                    //   .FirstOrDefault().ToList(),
                    Vehicle = res.Select(o => o.Vehicle).Where(v => v.ParkedVehicles.Any(pv => pv.Id == id)).FirstOrDefault()
                ,
                    VehicleTyp = res.Select(o => o.VehicleTyp).Where(vt => vt.Vehicles.Any(v => v.ParkedVehicles.Any(pv => pv.Id == id))).FirstOrDefault()
                ,
                    Member = res.Select(o => o.Member).Where(m => m.Vehicles.Any(v => v.ParkedVehicles.Any(pv => pv.Id == id))).FirstOrDefault()
                }

            };


            svar.ParkedVehicle.ParkedVehicles = svar.ParkedVehicle.Vehicle.ParkedVehicles.Where(pv => pv.Id == id).Select(x => new SubParkedViewModel
            {
                ParkedVehicle = x

            }).ToList();
            //await RePark();
            if (svar.ParkedVehicle == null)
            {
                return NotFound();
            }

            return View(svar);
        }

        // GET: ParkedVehicles/Receipt
        public async Task<IActionResult> Receipt(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            await InitPlots();
            var res = await AddTimeAndPrice(true);
            var svar = new SingelViewModel
            {
                ParkedVehicle = new ParkedViewModel
                {
                    //    ParkedVehicles = res.
                    //Select(o => o.ParkedVehicles).Select(pw => pw.Where(pwm => pwm.ParkedVehicle.Id == id))
                    //.FirstOrDefault().ToList()
                    //               ,
                    Vehicle = res.Select(o => o.Vehicle).Where(v => v.ParkedVehicles.Any(pv => pv.Id == id)).FirstOrDefault()
                ,
                    VehicleTyp = res.Select(o => o.VehicleTyp).Where(vt => vt.Vehicles.Any(v => v.ParkedVehicles.Any(pv => pv.Id == id))).FirstOrDefault()
                ,
                    Member = res.Select(o => o.Member).Where(m => m.Vehicles.Any(v => v.ParkedVehicles.Any(pv => pv.Id == id))).FirstOrDefault()
                }

            };


            var CostPerHour = res.Where(o => o.ParkedVehicles.Any(pv => pv.ParkedVehicle.Id == id)).Select(o => o.ParkedVehicles).Select(spv => spv.Select(pv => pv.CostPerHour)).FirstOrDefault().FirstOrDefault();
            var Price = res.Where(o => o.ParkedVehicles.Any(pv => pv.ParkedVehicle.Id == id)).Select(o => o.ParkedVehicles).Select(spv => spv.Select(pv => pv.Price)).FirstOrDefault().FirstOrDefault();
            var ParkedTime = res.Where(o => o.ParkedVehicles.Any(pv => pv.ParkedVehicle.Id == id)).Select(o => o.ParkedVehicles).Select(spv => spv.Select(pv => pv.ParkedTime)).FirstOrDefault().FirstOrDefault();

            svar.ParkedVehicle.ParkedVehicles = svar.ParkedVehicle.Vehicle.ParkedVehicles.Where(pv => pv.Id == id).Select(x => new SubParkedViewModel
            {
                ParkedVehicle = x,
                CostPerHour = CostPerHour,
                Price = Price,
                ParkedTime = ParkedTime


            }).ToList();


            return View(svar);
        }

        // GET: ParkedVehicles/Create
        public async Task<IActionResult> Create()
        {

            var res = new CreateViewModel
            {
                ParkedVehicle = new ParkedVehicle(),
                vehicleTypes = await _context.VehicleTyp.OrderBy(vt => vt.Name).ToListAsync(),
                Members = await _context.Members.ToListAsync()
            };

            return View(res);
        }

        // POST: ParkedVehicles/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,VehicleTypId,VehicleTyp,RegNr,VehicleColor,VehicleModel,VehicleBrand,NumberOfWheels,ParkInDate,ParkOutDate,MemberId")] CreateSetViewModel parkedVehicle)
        //   public async Task<IActionResult> Create(CreateSetViewModel parkedVehicle)
        {
            var reg_bussey = await _context.ParkedVehicle.Where(v => v.Vehicle.RegNr == parkedVehicle.RegNr && v.ParkOutDate == null).ToListAsync();

            var svar = new SingelViewModel
            {
                ParkedVehicle = new ParkedViewModel
                {
                    Vehicle = new Vehicle
                    {
                        RegNr = parkedVehicle.RegNr
                    }
                }
            };


            if (reg_bussey.Count > 0)
            {
                // return RedirectToAction(nameof(Create));

                return View("NotCreate", svar);
            }

            if (ModelState.IsValid)
            {


                await InitPlots();
                // if model does not exist.
                if (!(_context.Vehicles.Any(v => v.RegNr == parkedVehicle.RegNr)))
                {
                    var initVehicle = new Vehicle
                    {
                        VehicleTypId = parkedVehicle.VehicleTypId,
                        RegNr = parkedVehicle.RegNr,
                        VehicleColor = parkedVehicle.VehicleColor,
                        VehicleModel = parkedVehicle.VehicleModel,
                        VehicleBrand = parkedVehicle.VehicleBrand,
                        NumberOfWheels = parkedVehicle.NumberOfWheels,
                        MemberId = parkedVehicle.MemberId
                    };
                    _context.Vehicles.Add(initVehicle);
                    await _context.SaveChangesAsync();
                }


                parkedVehicle.VehicleTyp = await _context.VehicleTyp.Where(v => v.VehicleTypId == parkedVehicle.VehicleTypId).FirstOrDefaultAsync();

                ParkedVehicle InparkedVehicle = new ParkedVehicle
                {
                    MemberId = parkedVehicle.MemberId,
                    Member = await _context.Members.Where(m => m.Id == parkedVehicle.MemberId).FirstOrDefaultAsync(),
                    VehicleId = await _context.Vehicles.Where(m => m.RegNr == parkedVehicle.RegNr).Select(m => m.Id).FirstOrDefaultAsync(),
                    Vehicle = await _context.Vehicles.Where(m => m.RegNr == parkedVehicle.RegNr).FirstOrDefaultAsync()

                };

                if (parkhouse.Park(InparkedVehicle))
                {
                    _context.Add(InparkedVehicle);
                    await _context.SaveChangesAsync();
                    return RedirectToAction(nameof(Details), new { id = InparkedVehicle.Id });
                }
                else
                {

                    return View("GarageFull", svar);
                }
            }



            return View(svar);
        }

        // GET: ParkedVehicles/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var parkedVehicle = await _context.ParkedVehicle.FindAsync(id);
            if (parkedVehicle == null)
            {
                return NotFound();
            }
            return View(parkedVehicle);
        }


        public async Task<IActionResult> Parkthis(int vehicleid)
        {
            var thisvehicle = await _context.Vehicles.Where(v => v.Id == vehicleid).FirstOrDefaultAsync();
            var memberid = thisvehicle.MemberId;
            ParkedVehicle InparkedVehicle = new ParkedVehicle
            {
                MemberId = memberid,
                Member = await _context.Members.Where(m => m.Id == memberid).FirstOrDefaultAsync(),
                VehicleId = vehicleid,
                Vehicle = thisvehicle

            };
            if (parkhouse.Park(InparkedVehicle))
            {
                _context.Add(InparkedVehicle);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Details), new { id = InparkedVehicle.Id });
            }
            else
            {
                var svar = new SingelViewModel
                {
                    ParkedVehicle = new ParkedViewModel
                    {
                        Vehicle = new Vehicle
                        {
                            RegNr = thisvehicle.RegNr
                        }
                    }
                };


                return View("GarageFull", svar);
            }

        }
        // POST: ParkedVehicles/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,VehicleTypId,VehicleTyp,RegNr,VehicleColor,VehicleModel,VehicleBrand,NumberOfWheels,ParkInDate,ParkOutDate")] ParkedVehicle parkedVehicle)
        {
            if (id != parkedVehicle.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(parkedVehicle);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ParkedVehicleExists(parkedVehicle.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            return View(parkedVehicle);
        }


        public async Task<IActionResult> Check_Out(int? id)
        {

            var res = await AddTimeAndPrice();

            /* var p1 = res.
                     Select(o => o.ParkedVehicles).Select(pw => pw.Where(pwm => pwm.ParkedVehicle.Id == id)).ToList();

             var p2 = p1.FirstOrDefault(p => p.Any(pv=> pv.ParkedVehicle.Id==id));

             var p3 = p2.FirstOrDefault(p => p != null);

             var parkedVehicle = p3.ParkedVehicle;*/
            var parkedVehicle = res.
                    Select(o => o.ParkedVehicles).Select(pw => pw.Where(pwm => pwm.ParkedVehicle.Id == id))
                    .FirstOrDefault(p => p.Any(pv => pv.ParkedVehicle.Id == id))
                    .FirstOrDefault(p => p != null)
                    .ParkedVehicle;

            await InitPlots();
            parkedVehicle.ParkOutDate = DateTime.Now;
            try
            {

                parkhouse.Leave(parkedVehicle);
                _context.Update(parkedVehicle);
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ParkedVehicleExists(parkedVehicle.Id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return RedirectToAction(nameof(Receipt), new { id = id });
        }

        // GET: ParkedVehicles/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var parkedVehicle = await _context.ParkedVehicle
                .FirstOrDefaultAsync(m => m.Id == id);
            if (parkedVehicle == null)
            {
                return NotFound();
            }

            return View(parkedVehicle);
        }

        // POST: ParkedVehicles/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var parkedVehicle = await _context.ParkedVehicle.FindAsync(id);
            _context.ParkedVehicle.Remove(parkedVehicle);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ParkedVehicleExists(int id)
        {
            return _context.ParkedVehicle.Any(e => e.Id == id);
        }
        public async Task<IActionResult> Test(string SearchString)
        {
            var reta = await AddTimeAndPrice();
            return View("Index", reta);
        }

        private async Task<ParkedViewModel[]> AddTimeAndPrice(bool includeparkedout = false)
        {
            var res = _context.Vehicles
                .Include(v => v.ParkedVehicles)
                .Include(v => v.VehicleTyp)
                .Include(v => v.Member);

            var res2 = res
                .Where(v => (includeparkedout || (v.ParkedVehicles != null && v.ParkedVehicles.Any(pw => pw.ParkOutDate == null))));

            IQueryable<ParkedViewModel> res3;

            res3 = res2.Select(x => new ParkedViewModel()
            {
                ParkedVehicles = (x.ParkedVehicles == null) ? null : x.ParkedVehicles.Select(pv => new SubParkedViewModel
                {
                    ParkedVehicle = pv,
                    ParkedTime = (pv == null) ? null : PrettyPrintTime(((pv.ParkOutDate == null) ? DateTime.Now : pv.ParkOutDate) - pv.ParkInDate),
                    Price = pv.Vehicle.VehicleTyp.CostPerHour * (int)Math.Ceiling((((pv.ParkOutDate == null) ? DateTime.Now : pv.ParkOutDate) - pv.ParkInDate).Value.TotalHours),
                    CostPerHour = pv.Vehicle.VehicleTyp.CostPerHour
                }).ToList(),
                Vehicle = x,
                VehicleTyp = x.VehicleTyp,
                Member = x.Member
            });

            if (res3.Count() == 0)
                return null;
            else

                return res3.ToArray();

            //   .ToArrayAsync();
        }
        /*        [NotMapped]
        [Display(Name = "Parked Time")]
        public string ParkedTime { get; set; } // viewmodel

        [NotMapped]
        [Display(Name = "Price")]
        public int Price { get; set; }  // viewmodel
        [NotMapped]
        [Display(Name = "CostPerHour")]
        public int CostPerHour { get; set; } // viewmodel*/

        public async Task<IActionResult> SeekAndSort(string Message, string Sort = "Name", string SearchString = "")
        {
            var reta = await AddTimeAndPrice();
            string txt;
            if (SearchString != "") txt = $"Serch resultat of reg nr: {SearchString}";
            else txt = $"Parked Vehicles sorted by {Message}";

            var svar = new ListViewModel
            {
                ParkedViewModel = reta.Where(o => o.Vehicle.VehicleTyp.Name.ToLower().Equals(SearchString.ToLower())
                || o.Vehicle.RegNr.ToLower().Contains(SearchString.ToLower())).OrderBy(s => Get_seek(s, Sort)),

                Message = txt

            };
            return View("Index", svar);
        }

        private static string Get_seek(ParkedViewModel s, string sort)
        {
            if (sort.Equals("Name")) return s.Vehicle.VehicleTyp.Name;
            else if (sort.Equals("FirstName")) return s.Member.LastName;
            else if (sort.Equals("RegNr")) return s.Vehicle.RegNr;
            else if (sort.Equals("VehicleColor")) return s.Vehicle.VehicleColor;
            else if (sort.Equals("VehicleModel")) return s.Vehicle.VehicleModel;
            else if (sort.Equals("VehicleBrand")) return s.Vehicle.VehicleBrand;
            else
                return s.Vehicle.VehicleTyp.Name;
        }

        public async Task<IActionResult> Statistik()
        {
            var reta = await AddTimeAndPrice(true);
            // var reta_no = await AddTimeAndPrice();
            //   reta.Select(o => o.NumberOfWheels).Sum();
            StatViewModel stat = new StatViewModel();
            stat.TotalWeels = reta
                .Where(o => o.ParkedVehicles.Any(pv => pv.ParkedVehicle.ParkOutDate == null))
                .Select(o => o.Vehicle.NumberOfWheels)
                .Sum();

            stat.TotalIncome = reta
                .Select(o => o.ParkedVehicles)
                .Select(p => p.Select(pw => pw.Price).Sum())
                .Sum();
            stat.TodayTotalIncome = stat.TotalIncome -
                reta
                 .Where(o => o.ParkedVehicles.Any(pv => pv.ParkedVehicle.ParkOutDate?.Date <= DateTime.Now.Date.AddDays(-1)))
                 .Select(p => p.ParkedVehicles.Select(pw => pw.Price).Sum())
                 .Sum();
            stat.myTypes = reta
                .Where(o => o.ParkedVehicles.Any(pv => pv.ParkedVehicle.ParkOutDate == null))
                .GroupBy(v => v.VehicleTyp.Name)
                .Select(g => new MyTypes { Name = g.Key, Count = g.Count() });

            stat.members_count = reta.Select(members => members.Member).Count();

            stat.ParkingSpaces = parkhouse.GetSpaces();
            stat.FreeParkingSpaces = parkhouse.GetFreeSpaces();

            return View(stat);
        }
    }

}
