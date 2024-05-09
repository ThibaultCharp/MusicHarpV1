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
    public class UserController : Controller
    {
        private readonly UserBL UserService;

        public UserController()
        {
            UserService = new UserBL(new UserRepositiry());
        }


        public IActionResult Login(string username, string password)
        {
            var (loginSuccessful, userId) = UserService.Login(username, password);

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



        public IActionResult SignUp(User user) 
        {
            UserService.SignUp(user);
            return RedirectToAction("Login", "User");
        }

        public IActionResult SignUpPage() 
        {
            ErrorViewModel errorViewModel = new ErrorViewModel();

            return View(errorViewModel);
        }
    }

    
}
