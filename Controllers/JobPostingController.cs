﻿using Microsoft.AspNetCore.Mvc;

namespace First_Step_API.Controllers
{
    public class JobPostingController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }

        public IActionResult CreateOrEditJobPosting(int id)
        {
            return View();
        }
    }
}
