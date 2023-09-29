using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace PL.Controllers
{
    public class AlumnoMateriaController : Controller
    {
        // GET: AlumnoMateria
        public ActionResult GetAll()
        {
            ML.Result result=BL.Alumno.GetAll();
            ML.Alumno alumno=new ML.Alumno();
            alumno.Alumnos=new List<object>();
            if (result.Correct)
            {
                alumno.Alumnos=result.Objects.ToList();
            }
            else
            {
                ViewBag.Mensaje = "No hay registros en la tabla";
            }

            return View(alumno);
        }
    }
}