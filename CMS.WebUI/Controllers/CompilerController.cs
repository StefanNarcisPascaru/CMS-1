using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using CMS.Domain.Models;


// For more information on enabling MVC for empty projects, visit http://go.microsoft.com/fwlink/?LinkID=397860

namespace CMS.WebUI.Controllers
{
    public class ChatController : Controller
    {
        private CMS.Domain.CmsContext _context = new CMS.Domain.CmsContext();
        public static string Materie { set; get; }
        public static bool Verificare { set; get; }
        

 
        public IActionResult Choose()
        {
            if (Verificare == true)
                ViewBag.Verificare = "No discipline";
            return View();
        }

        [HttpPost]
        public IActionResult Choose(Comment materie)
        {
            Materie = materie.message;

            Verificare = true;
            List<Subject> lines;
            lines=_context.Subjects.ToList();
            foreach (Subject line in lines)
            {
                if (line.subjectName == Materie)
                    Verificare = false;
            }
            if (Verificare == false)
            {
               Verificare = true;
               return RedirectToAction("Chat");
                
            }
            else
                return RedirectToAction("Choose");
        }
        // GET: /<controller>/
        public IActionResult Chat()
        {
            List<string> mesaje=new List<string>();//lista cu mesaje 
            List<Comment> lines;//liniile din tabela
           


            lines=_context.Comments.ToList();//informatiile din tabela
            lines = lines.OrderBy(x => x.date).ToList();//sortam dupa data

            foreach (Comment line in lines)
            {
                if (String.Compare(line.subject,Materie)==0)

                    mesaje.Add(line.username+"::"+line.date+":"+line.message+ "\n"); 
            }

          

            ViewBag.chat = mesaje;
            ViewBag.materie = Materie;


            
            return View();
        }
        [HttpPost]
        public IActionResult Chat(Comment model)
        {
            model.UserId = Guid.Parse("EBF73255-1252-4FD0-AAD5-7C9D61E38824");
            
            model.date = DateTime.Now;
            model.subject = Materie;
          

            if (User.Identity.IsAuthenticated)
                model.username=User.Identity.Name;
            else
               model.username="No user identity available.";
            

            _context.Comments.Add(model);
            _context.SaveChanges();
            ModelState.Clear();


            return RedirectToAction("Chat");
        }
    }
}

