using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using ECommerceShoppingStore.EFCore;
using ECommerceShoppingStore.Entities;
using System.Net;
using Newtonsoft.Json;
using System.Net.Http.Headers;

namespace ECommerceShoppingStore.MVC.Controllers
{
    public class ShoppingCartMVCController : Controller
    {
        // display all ShoppingCart Data
        public async Task<IActionResult> Index()
        {
            List<ShoppingCart> objShoppingCart = await GetShoppingCarts();

            return View(objShoppingCart);
        }

        // display perticular shoppingCart  details
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            try
            {
                var shoppingCart = await GetShoppingCarts(id);

                if (shoppingCart == null)
                {
                    return NotFound();
                }

                return View(shoppingCart);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        // enter new ShoppingCart data
        public IActionResult Create()
        {
            return View();
        }

        // bind enter data to create new record
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("RecordId,CartId,Quantity,Productid,DateCreated")] ShoppingCart shoppingCart)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    await AddShoppingCart(shoppingCart);
                }
                catch (Exception)
                {
                    if (ShoppingCartExists(shoppingCart.RecordId) == null)
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

            return View(shoppingCart);
        }

        // edit perticular data
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }


            try
            {
                var shoppingCart = await GetShoppingCarts(id);
                if (shoppingCart == null)
                {
                    return NotFound();
                }

                return View(shoppingCart);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        //  bind enter data 
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("RecordId,CartId,Quantity,Productid,DateCreated")] ShoppingCart shoppingCart)
        {
            if (id != shoppingCart.CartId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    await UpdateShoppingCart(shoppingCart);

                }
                catch (Exception)
                {
                    if (ShoppingCartExists(shoppingCart.RecordId) == null)
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

            return View(shoppingCart);
        }

        // delete perticular data
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            try
            {
                ShoppingCart objShoppingCart = await GetShoppingCarts(id);

                if (objShoppingCart == null)
                {
                    return NotFound();
                }

                return View(objShoppingCart);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }



        // this function give confirmation 
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {

            await DeleteShoppingCart(id);
            return RedirectToAction(nameof(Index));
        }

        private async Task<ShoppingCart> ShoppingCartExists(int id)
        {

            ShoppingCart objshoppingCart = await GetShoppingCarts(id);
            return objshoppingCart;

        }




        //----------Separate function used by actions methods of our controller

        // get all Shopping Cart from server side
        public async Task<List<ShoppingCart>> GetShoppingCarts()
        {
            try
            {
                string baseUrl = "https://localhost:7222";
                HttpClient client = new HttpClient();
                client.BaseAddress = new Uri(baseUrl);
                var contentType = new MediaTypeWithQualityHeaderValue("application/json");
                client.DefaultRequestHeaders.Accept.Add(contentType);
                var token = SessionHelper.GetObjectFromJson<String>(HttpContext.Session, "token");
                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);

                HttpResponseMessage response = await client.GetAsync("/api/ShoppingCart");
                string stringData = response.Content.ReadAsStringAsync().Result;
                List<ShoppingCart> data = JsonConvert.DeserializeObject<List<ShoppingCart>>(stringData);

                if (response.StatusCode == HttpStatusCode.Unauthorized)
                {
                    ViewBag.Message = "Unauthorized!";
                }
                else
                {
                    return data;
                }

                return data;
            }
            catch (Exception)
            {
                throw;
            }
        }

        // get perticular Shopping Cart data from server side
        public async Task<ShoppingCart> GetShoppingCarts(int? id)
        {
            try
            {
                string baseUrl = "https://localhost:7222";
                HttpClient client = new HttpClient();
                client.BaseAddress = new Uri(baseUrl);
                var contentType = new MediaTypeWithQualityHeaderValue("application/json");
                client.DefaultRequestHeaders.Accept.Add(contentType);
                var token = SessionHelper.GetObjectFromJson<String>(HttpContext.Session, "token");
                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);

                HttpResponseMessage response = await client.GetAsync("/api/ShoppingCart/" + id);
                string stringData = response.Content.ReadAsStringAsync().Result;
                ShoppingCart data = JsonConvert.DeserializeObject<ShoppingCart>(stringData);

                if (response.StatusCode == HttpStatusCode.Unauthorized)
                {
                    ViewBag.Message = "Unauthorized!";
                }
                else
                {

                    return data;
                }

                return data;
            }
            catch (Exception)
            {
                throw;
            }
        }

        // send enter data to server side
        public async Task<ShoppingCart> AddShoppingCart(ShoppingCart shoppingCart)
        {
            try
            {
                string baseUrl = "https://localhost:7222";
                HttpClient client = new HttpClient();
                client.BaseAddress = new Uri(baseUrl);
                var contentType = new MediaTypeWithQualityHeaderValue("application/json");
                client.DefaultRequestHeaders.Accept.Add(contentType);
                var token = SessionHelper.GetObjectFromJson<String>(HttpContext.Session, "token");
                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);

                string stringData = JsonConvert.SerializeObject(shoppingCart);
                var contentData = new StringContent(stringData,
            System.Text.Encoding.UTF8, "application/json");
                HttpResponseMessage response = await client.PostAsync("api/ShoppingCart/", contentData);

                if (response.IsSuccessStatusCode)
                {
                    
                }

                if (response.StatusCode == HttpStatusCode.Unauthorized)
                {
                    ViewBag.Message = "Unauthorized!";
                }
                return shoppingCart;
            }
            catch (Exception)
            {
                throw;
            }

        }

        // send edited data to server side
        public async Task<ShoppingCart> UpdateShoppingCart(ShoppingCart shoppingCart)
        {
            try
            {
                string baseUrl = "https://localhost:7222";
                HttpClient client = new HttpClient();
                client.BaseAddress = new Uri(baseUrl);
                var contentType = new MediaTypeWithQualityHeaderValue
            ("application/json");
                client.DefaultRequestHeaders.Accept.Add(contentType);
                var token = SessionHelper.GetObjectFromJson<String>(HttpContext.Session, "token");
                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);

                string stringData = JsonConvert.SerializeObject(shoppingCart);
                var contentData = new StringContent(stringData,
            System.Text.Encoding.UTF8, "application/json");
                HttpResponseMessage response = await client.PutAsync("api/shoppingCart/" + shoppingCart.CartId, contentData);

                if (response.IsSuccessStatusCode)
                {
                    
                }

                if (response.StatusCode == HttpStatusCode.Unauthorized)
                {
                    ViewBag.Message = "Unauthorized!";
                }
                return shoppingCart;
            }
            catch (Exception)
            {

                throw;
            }
        }

        // send request to server side to delete record
        public async Task<ShoppingCart> DeleteShoppingCart(int? id)
        {
            try
            {
                string baseUrl = "https://localhost:7222";
                HttpClient client = new HttpClient();
                client.BaseAddress = new Uri(baseUrl);
                var contentType = new MediaTypeWithQualityHeaderValue
            ("application/json");
                client.DefaultRequestHeaders.Accept.Add(contentType);
                var token = SessionHelper.GetObjectFromJson<String>(HttpContext.Session, "token");
                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);

                HttpResponseMessage response = await client.DeleteAsync("/api/ShoppingCart/" + id);
                string stringData = response.Content.
            ReadAsStringAsync().Result;
                ShoppingCart data = JsonConvert.DeserializeObject<ShoppingCart>(stringData);

                if (response.StatusCode == HttpStatusCode.Unauthorized)
                {
                    ViewBag.Message = "Unauthorized!";
                }
                else
                {

                    return data;
                }

                return data;
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}
