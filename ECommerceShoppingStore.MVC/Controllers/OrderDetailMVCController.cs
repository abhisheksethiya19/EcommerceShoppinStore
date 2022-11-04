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
    public class OrderDetailMVCController : Controller
    {
        // display all OrderDetails
        public async Task<IActionResult> Index()
        {
            List<OrderDetail> objOrderDetail = await GetOrderDetails();

            return View(objOrderDetail);
        }

        // display perticular orderDetail type details
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            try
            {
                var orderDetail = await GetOrderDetails(id);

                if (orderDetail == null)
                {
                    return NotFound();
                }

                return View(orderDetail);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        // enter new OrderDetails data
        public IActionResult Create()
        {
            return View();
        }

        // bind enter data to create new record
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("OrderId,ProductId,Quantity,UnitCost")] OrderDetail orderDetail)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    await AddOrderDetail(orderDetail);
                }
                catch (Exception)
                {
                    if (OrderDetailExists(orderDetail.OrderId) == null)
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

            return View(orderDetail);
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
                var orderDetail = await GetOrderDetails(id);
                if (orderDetail == null)
                {
                    return NotFound();
                }

                return View(orderDetail);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        //  bind enter data 
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("OrderId,ProductId,Quantity,UnitCost")] OrderDetail orderDetail)
        {
            if (id != orderDetail.OrderId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    await UpdateOrderDetail(orderDetail);

                }
                catch (Exception)
                {
                    if (OrderDetailExists(orderDetail.OrderId) == null)
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

            return View(orderDetail);
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
                OrderDetail objOrderDetail = await GetOrderDetails(id);

                if (objOrderDetail == null)
                {
                    return NotFound();
                }

                return View(objOrderDetail);
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

            await DeleteOrderDetail(id);
            return RedirectToAction(nameof(Index));
        }

        private async Task<OrderDetail> OrderDetailExists(int id)
        {

            OrderDetail objorderDetail = await GetOrderDetails(id);
            return objorderDetail;

        }




        //----------Separate function used by actions methods of our controller

        // get all OrderDetails from server side
        public async Task<List<OrderDetail>> GetOrderDetails()
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

                HttpResponseMessage response = await client.GetAsync("/api/OrderDetails");
                string stringData = response.Content.ReadAsStringAsync().Result;
                List<OrderDetail> data = JsonConvert.DeserializeObject<List<OrderDetail>>(stringData);

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

        // get perticular OrderDetails from server side
        public async Task<OrderDetail> GetOrderDetails(int? id)
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

                HttpResponseMessage response = await client.GetAsync("/api/OrderDetails/" + id);
                string stringData = response.Content.ReadAsStringAsync().Result;
                OrderDetail data = JsonConvert.DeserializeObject<OrderDetail>(stringData);

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
        public async Task<OrderDetail> AddOrderDetail(OrderDetail orderDetail)
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

                string stringData = JsonConvert.SerializeObject(orderDetail);
                var contentData = new StringContent(stringData,
            System.Text.Encoding.UTF8, "application/json");
                HttpResponseMessage response = await client.PostAsync("api/OrderDetails/", contentData);

                if (response.IsSuccessStatusCode)
                {
                    
                }

                if (response.StatusCode == HttpStatusCode.Unauthorized)
                {
                    ViewBag.Message = "Unauthorized!";
                }
                return orderDetail;
            }
            catch (Exception)
            {
                throw;
            }

        }

        // send edited data to server side
        public async Task<OrderDetail> UpdateOrderDetail(OrderDetail orderDetail)
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

                string stringData = JsonConvert.SerializeObject(orderDetail);
                var contentData = new StringContent(stringData,
            System.Text.Encoding.UTF8, "application/json");
                HttpResponseMessage response = await client.PutAsync("api/orderDetails/" + orderDetail.OrderId, contentData);

                if (response.IsSuccessStatusCode)
                {
                   
                }

                if (response.StatusCode == HttpStatusCode.Unauthorized)
                {
                    ViewBag.Message = "Unauthorized!";
                }
                return orderDetail;
            }
            catch (Exception)
            {

                throw;
            }
        }

        // send request to server side to delete record
        public async Task<OrderDetail> DeleteOrderDetail(int? id)
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

                HttpResponseMessage response = await client.DeleteAsync("/api/OrderDetails/" + id);
                string stringData = response.Content.
                 ReadAsStringAsync().Result;
                OrderDetail data = JsonConvert.DeserializeObject<OrderDetail>(stringData);

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
