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
        public ActionResult GetMateriasAsignadas(int IdAlumno)
        {
           
            ML.Result result = BL.AlumnoMateria.GetMateriasAsignadas(IdAlumno);
            ML.AlumnoMateria alumnoMateria=new ML.AlumnoMateria();
            if (result.Correct)
            {
                alumnoMateria.AlumnosMaterias = result.Objects;   
            }
            //ML.AlumnoMateria alumnoMateria=new ML.AlumnoMateria();
            //alumnoMateria = (ML.AlumnoMateria)result.Object;
            //alumnoMateria.Alumno.IdAlumno=IdAlumno;
            return View(alumnoMateria);
        }
        public ActionResult Delete(int IdMateria)
        {
            ML.Result result=BL.AlumnoMateria.DeleteMateria(IdMateria);
            if (result.Correct)
            {
                ViewBag.Mensaje = "Materia eliminada correctamente";
            }
            else
            {

                ViewBag.Mensaje = "Materia no eliminada correctamente";
            }
            return PartialView("Modal");
        }
    }
}