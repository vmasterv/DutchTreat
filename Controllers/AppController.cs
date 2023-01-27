using DutchTreat.Data;
using DutchTreat.Services;
using DutchTreat.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.Update;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DutchTreat.Controllers
{
    public class AppController : Controller
    {
        private readonly INullMailService _mailService;
        private readonly IDutchRepository _repository;

        public AppController(INullMailService mailService, IDutchRepository repository)
        {
            _mailService = mailService;
            _repository = repository;
            
        }
        
        public IActionResult Index() 
        {
            //throw new InvalidProgramException("Bad things happen to good developers");
            //var results = _repository.Products.ToList();
            return View();
        }

        [HttpGet("contact")]
        public IActionResult Contact()
        {
            
            return View();
        }

        [HttpPost("contact")]
        public IActionResult Contact(ContactViewModel model)
        {
            if (ModelState.IsValid)
            {
                _mailService.SendMessage("shawn@wildermuth.com", model.Subject, $"From: {model.Name} - {model.Email}, Message: {model.Message}");
                ViewBag.UserMessage = "Mail sent";
                ModelState.Clear();
            }
            else
            {

            }
            return View();
        }

        public IActionResult About()
        {
            ViewBag.Title = "About Us";

            return View();
        }

        
        public IActionResult Shop()
        {
            var results = _repository.GetAllProducts();
            return View(results.ToList());
        }

        

    }
}
