﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using OlympicWeb.Models;

namespace OlympicWeb.Controllers
{
    [Route("api/[controller]/[action]")]
    //[ApiController]
    public class UsersController : ControllerBase
    {
        private IAppManager manager;
        public UsersController(IAppManager manger)
        {
            this.manager = manger;
        }

        [HttpGet("{username}")]
        // /api/Users
        [ActionName("login")]

        //returns user with the info if this user is admin      
        public User Post(string username)
        {
            string[] temp = username.Split('&', 2);
            string user_name = temp[0];
            string password = temp[1];
            return manager.UserLogin(user_name, password);
        }

        [HttpPost("{username}")]
        // /api/Users/sign_up
        [ActionName("sign_up")]
        public bool SignupPost(string username)
        {
            //User user = new User();
            string[] temp = username.Split('&', 2);
            string user_name = temp[0];
            string password = temp[1];
            return manager.UserSignup(user_name, password);
        }
        [HttpPost]
        // /api/Users/change_password
        [ActionName("change_password")]
        public bool UpdatePassword(User user)
        {
            return manager.ChangePassword(user.Username, user.Password);
        }
        [HttpDelete("{username}")]
        [ActionName("delete")]
        // /api/Users
        public void DeleteUser(string username)
        {
            manager.DeleteUser(username);
        }

        [HttpPost]
        // /api/Users/admin
        [ActionName("admin")]
        public bool UpdateAdmin(User user, string sport, bool isAdmin)
        {
            return manager.UpdateAdmin(user, sport, isAdmin);
        }

        public List<string> GetAdminList(string username)
        {
            return manager.GetAdminList(username);

        }

    }
}
