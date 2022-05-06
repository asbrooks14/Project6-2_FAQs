﻿// Alexandra Brooks 
// FAQ App (Project 6-2)
// CIS 411-76
// Spring 2022
// Due: 5/3/2022

using FAQs.Models;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using System.Linq;
using Microsoft.EntityFrameworkCore;

namespace FAQs.Controllers
{
    public class HomeController : Controller
    {
        private FAQContext context { get; set; }
        public HomeController(FAQContext ctx)
        {
            context = ctx;
        }

        [Route("/topic/{topicId}/category/{catId}")]
        [Route("/topic/{topicId}")]
        [Route("/category/{catId}")]
        [Route("/")]

        public IActionResult Index(string category, string topic)
        {
            ViewBag.Topics = context.Topics.OrderBy(t => t.TName).ToList();
            ViewBag.Categories = context.Categories.OrderBy(c => c.CName).ToList();
            ViewBag.ActiveTopic = topic;

            IQueryable<FAQ> faqs = context.FAQs
                .Include(f => f.Topic)
                .Include(f => f.Category)
                .OrderBy(f => f.QuestionName);

            if (!string.IsNullOrEmpty(topic))
            {
                faqs = faqs
                    .Where(f => f.TopicId == topic);
            }
            
            if (!string.IsNullOrEmpty(category))
            {
                faqs = faqs
                    .Where(f => f.CategoryId == category);
            }

            return View(faqs.ToList());

        }
    }
}