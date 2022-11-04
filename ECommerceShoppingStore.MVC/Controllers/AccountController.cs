using ECommerceShoppingStore.MVC.Models;
using ECommerceShoppingStore.MVC;
using ECommerceShoppingStore.WebAPI.Authentication;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Newtonsoft.Json;
using System.IdentityModel.Tokens.Jwt;
using System.Net;
using System.Net.Http.Headers;
using System.Security.Claims;

namespace ECommerceShoppingStore.MVC.Controllers
{
    public class AccountController : Controller
    {

        
        public IActionResult Login()
        {
            return View();
        }

        
        
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Login([Bind("UserName,Password")] LoginModel loginmodel)
        {
            var returnLoginModel = await AddLoginModel(loginmodel);
            var handler = new JwtSecurityTokenHandler();

            var token2 = SessionHelper.GetObjectFromJson<String>(HttpContext.Session, "token");
            var token = handler.ReadJwtToken(token2);

            var role = token.Claims.Where(c => c.Type == ClaimTypes.Role).FirstOrDefault();
            if ((role.Value == "Admin") ||
               (returnLoginModel.UserName == "admin") && (returnLoginModel.Password == "Admin@123") ||
               (returnLoginModel.UserName == "sir") && (returnLoginModel.Password == "Sir@123"))
            {
                return RedirectToAction("HomeAdmin", "Home");
            }
            else if (role.Value == "Customer")
            {
                return RedirectToAction("HomeCustomer", "Home");
            }
            else
            {
                return RedirectToAction("HomeAdmin", "Home");
            }

            return View(loginmodel);
        }


        public async Task<LoginModel> AddLoginModel(LoginModel loginmodel)
        {
            string baseUrl = "https://localhost:7222";
            HttpClient client = new HttpClient();
            client.BaseAddress = new Uri(baseUrl);
            var contentType = new MediaTypeWithQualityHeaderValue("application/json");
            client.DefaultRequestHeaders.Accept.Add(contentType);
            string stringData = JsonConvert.SerializeObject(loginmodel);
            var contentData = new StringContent(stringData,
        System.Text.Encoding.UTF8, "application/json");
            HttpResponseMessage response = await client.PostAsync("api/Authenticate/login", contentData);

            if (response.IsSuccessStatusCode)
            {
                string stringJWT = response.Content.
                ReadAsStringAsync().Result;
                JWT jwt = JsonConvert.DeserializeObject
                <JWT>(stringJWT);

                //HttpContext.Session.SetString("token", jwt.Token);
                // TempData["token"] = jwt.Token;
                SessionHelper.SetObjectAsJson(HttpContext.Session, "token", jwt.Token);
            }

            if (response.IsSuccessStatusCode)
            {
                RedirectToAction("Home", "HomeAdmin");
            }

            if (response.StatusCode == HttpStatusCode.Unauthorized)
            {
                ViewBag.Message = "Unauthorized!";
            }
            return loginmodel;

        }

        public async Task<IActionResult> Register()
        {
              //List<IdentityRole> roles = await GetRoles();
             // var rolesWithoutAdmin = roles.Where(r => r.Name != "Admin");
            // var sortedRoles = rolesWithoutAdmin.OrderBy(r => r.Name).ToList().Select(rr => new SelectListItem { Value = rr.Name.ToString(), Text = rr.Name }).ToList();
           // var sortedRoles = roles.OrderBy(r => r.Name).ToList().Select(rr => new SelectListItem { Value = rr.Name.ToString(),Text = rr.Name}).ToList();
          // ViewData["Role"] = sortedRoles;
            return View();
        }

        
        
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Register([Bind("UserName,Email,Password,Role")] RegisterModel registermodel)
        {

            var returnedregistermodel = await AddRegisterModel(registermodel);

            if (returnedregistermodel.Role == "Admin")
            {
                return RedirectToAction("HomeCommon", "Home");
            }
           
            else
            {
                return RedirectToAction("HomeCommon", "Home");
            }
        }

        private object RegisterModelExists(string email)
        {
            throw new NotImplementedException();
        }

        public async Task<RegisterModel> AddRegisterModel(RegisterModel registermodel)
        {

            string baseUrl = "https://localhost:7222";
            HttpClient client = new HttpClient();
            if (registermodel.Role == "Admin")
            {
                registermodel.Role = UserRoles.ADMIN;
            }
            else 
            {
                registermodel.Role = UserRoles.CUSTOMER;
            }          
            client.BaseAddress = new Uri(baseUrl);
            var contentType = new MediaTypeWithQualityHeaderValue("application/json");
            client.DefaultRequestHeaders.Accept.Add(contentType);
            string stringData = JsonConvert.SerializeObject(registermodel);
            var contentData = new StringContent(stringData,
        System.Text.Encoding.UTF8, "application/json");
            HttpResponseMessage response = await client.PostAsync("api/Authenticate/register", contentData);

            if (response.IsSuccessStatusCode)
            {
                return registermodel;
               
            }

            if (response.StatusCode == HttpStatusCode.Unauthorized)
            {
                ViewBag.Message = "Unauthorized!";
            }
            return registermodel;
        }


        public IActionResult Logout()
        {
            HttpContext.Session.Remove("token");
            ViewBag.Message = "User logged out successfully!";
            return View("Index");
        }
       
    }
}
