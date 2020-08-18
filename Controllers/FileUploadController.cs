using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace MVCFlowerShop.Controllers
{
    public class FileUploadController : Controller
    {
        // GET: /<controller>/
        public IActionResult Index()
        {
            return View();
        }

        public IActionResult AboutUs()
        {
            int a = 0;
            if (a == 0)
                return RedirectToAction("display");
            return View();
        }

        public string display()
        {
            return "Hello!";
        }
        [HttpPost("UploadFiles")]
        public async Task<IActionResult> Post(List<IFormFile> files)
        {
            long sizes = files.Sum(f => f.Length);

            var filepath = Path.GetTempFileName();

            int i = 1; TempData["message"] =null;
            foreach(var Formfile in files)
            {
                if (Formfile.ContentType.ToLower() != "text/plain")
                {
                    TempData["message"] = "The " + Path.GetFileName(filepath) + " is a wrong file";
                    break;
                }
                else if(Formfile.Length <= 0)
                {
                    TempData["message"] = "The " + Path.GetFileName(filepath) + " is empty";
                    break;
                }
                else if(Formfile.Length > 1048576)
                {
                    TempData["message"] = "The " + Path.GetFileName(filepath) + " is too big";
                    break;
                }
                else
                {
                    var filedest = "/Users/harrydevarakarianingcipto/Codes/ASP.NET/ASP.NET file upload" + i + ".txt";

                    using (var stream = new FileStream(filedest, FileMode.Create))
                    {
                        await Formfile.CopyToAsync(stream);
                    }
                    using (var reader = new StreamReader(Formfile.OpenReadStream()))
                    {
                        TempData["message"] = TempData["message"] + "\\n" + await reader.ReadToEndAsync();
                    }
                }
                i++;
            }
            return RedirectToAction("Index");
        }

    }
}
