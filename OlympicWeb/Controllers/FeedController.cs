﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using OlympicWeb.Models;
using OlympicWeb.DB;
namespace OlympicWeb.Controllers

{
    [Route("api/[controller]")]
    [ApiController]
    public class FeedController : ControllerBase
    {
        private IAppManager manager;
        public FeedController(IAppManager manger)
        {
            this.manager = manger;
        }
        // GET: api/Feed
        [HttpGet]
        public List<Post> Get()
        {
            return manager.getPosts();
        }

    }
}
