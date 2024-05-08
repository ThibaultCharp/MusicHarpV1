using Microsoft.AspNetCore.Mvc;
using MusicHarpV1.Models;
using System.Diagnostics;
using BusinessLogicLayer.Classes;
using BusinessLogicLayer.Entitys;
using PresentationLayer.Models;
using DataLogicLayer.DAL;
using System.Reflection.Metadata;
using BusinessLogicLayer.Repo_Interfaces;
using Microsoft.EntityFrameworkCore.Storage.Json;
using MySql.Data.MySqlClient;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Session;

namespace PresentationLayer.Controllers
{
    public class LoginController : Controller
    {
        private readonly LoginBL loginService;

        public LoginController()
        {
            loginService = new LoginBL(new LoginRepositiry());
        }


        public IActionResult Login(string username, string password)
        {
            var (loginSuccessful, userId) = loginService.Login(username, password);

            if (loginSuccessful)
            {
                HttpContext.Session.SetInt32("User_Id", userId);
                HttpContext.Session.SetString("Username", username);

                return RedirectToAction("HomePage", "Home");
            }

            else
            {
                return RedirectToAction("Index", "Home");
            }
        }
    }
}
