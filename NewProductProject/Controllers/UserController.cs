using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using NewProductProject.Models;
using System;
using System.Collections.Generic;
using System.Security.Claims;

namespace NewProductProject.Controllers
{
    public class UserController : Controller
    {
        CartDAL cd = new CartDAL();
        ProductDAL pd = new ProductDAL();


        [Authorize]

        public ActionResult Cart(int pid, int qty)
        {



            string userId = User.FindFirst(ClaimTypes.NameIdentifier).Value;
            List<Product> plist = new List<Product>();

            int res = pd.AddToCart(pid, userId, qty);
            if (res == 1)
            {


                var m = cd.GetCartProduct(userId);

                foreach (var item in m)
                {
                    Product p = pd.GetProductById(item.Pid);
                    p.Quantity = item.Quantity;
                    plist.Add(p);

                }
                ViewBag.Product = plist;

            }
            return View();
        }
        [Authorize]
        public ActionResult Order()
        {
            List<Product> plist = new List<Product>();
            string userId = User.FindFirst(ClaimTypes.NameIdentifier).Value;
            var m = cd.GetCartProduct(userId);

            foreach (var item in m)
            {
                Product p = pd.GetProductById(item.Pid);
                p.Quantity = item.Quantity;
                int res = cd.PlaceOrder(item.Pid, userId, p.Quantity);
                if (res >= 1)
                {
                    int res1 = cd.EmptyCart(userId);
                }
                plist.Add(p);


            }
            ViewBag.Product = plist;

            return View();
        }











        public ActionResult Delete(int id)
        {
            return View();
        }

        
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id, IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }
    
}
}
