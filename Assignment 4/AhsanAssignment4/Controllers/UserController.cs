using BAL;
using POKOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using TokenJWT;

namespace AhsanAssignment4.Controllers
{
    public class UserController : Controller
    {
        [HttpGet]
        public ActionResult login()
        {
            return View();
        }

        [HttpPost]
        [ActionName("login")]
        public ActionResult login1(String login,String password)
        {
            UserPoko userPoko = BO.UserValidation(login, password);
            if(userPoko!=null)
            {
                Object jwt=Token.GetToken(userPoko);

                Session["user"] = userPoko;
                TempData["token"] = jwt;
                TempData["name"] = userPoko.name;
                return Redirect("~/User/home");
            }
            else
            {
                Session["user"] = null; 
                ViewBag.error= "Credentials are not correct!";
            }
            return View();
        }


        [HttpGet]
        public ActionResult register()
        {
            return View();
        }

        [HttpPost]
        [ActionName("register")]
        public ActionResult register1(String username,String login, String password)
        {
            if(String.IsNullOrEmpty(username) || String.IsNullOrEmpty(login) || String.IsNullOrEmpty(password))
            {
                ViewBag.error = "Empty Fields!";
            }
            else
            {
                UserPoko userPoko = new UserPoko();
                userPoko.userId = 0;
                userPoko.name = username;
                userPoko.password = password;
                userPoko.login = login;
                int uid = BO.SaveUser(userPoko);
                if(uid!=0)
                {
                    return Redirect("~/User/login");
                }
            }
            return View();
        }

        [HttpGet]
        public ActionResult home()
        {
            if(Session["user"]==null)
            {
                return Redirect("~/User/login");
            }
            return View();
        }
    }
}