using ACTIVIDAD_FORMULARIO.Models;
using ACTIVIDAD_FORMULARIO.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace ACTIVIDAD_FORMULARIO.Controllers
{
    public class AlumnoController : Controller
    {
        private IConfiguration configuration;
        private AlumnoContext _context;

        public AlumnoController(IConfiguration configuration, AlumnoContext context)
        {
            this.configuration = configuration;
            _context = context;
        }

        public ActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Index(Alumno alumno)
        {
            var recaptcha = new Recaptcha(configuration);
            bool isRecaptchaValid = recaptcha.IsRecaptchaValid(Request);

            if(!isRecaptchaValid) 
            {
                ViewBag.Recaptcha = "El recaptcha NO se verifico";
                    return View();
            }

            if(ModelState.IsValid) 
            {
                ViewBag.Mensaje = "Usuario Correcto";
                _context.Alumnos.Add(alumno);
                _context.SaveChanges();
                var emailSender = new EmailSender(configuration);
                emailSender.SendEmail(alumno);
                return View("Index", new Alumno());
            }

            return View();
        }

        public ActionResult Listar()
        {
            IEnumerable<Alumno> alumnos = _context.Alumnos;
            return View("Listar", alumnos);
        }


    }
}
