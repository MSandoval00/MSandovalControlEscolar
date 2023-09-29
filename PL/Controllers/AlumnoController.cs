using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace PL.Controllers
{
    public class AlumnoController : Controller
    {
        // GET: Alumno
        [HttpGet]
        public ActionResult GetAll()
        {
            //ML.Result result= BL.Alumno.GetAll();
            ML.Alumno alumno=new ML.Alumno();
            //alumno.Alumnos = new List<object>();
            ServiceReferenceAlumno.ServiceAlumnoClient alumnoWCF=new ServiceReferenceAlumno.ServiceAlumnoClient();
            var result =alumnoWCF.GetAll();
            if (result.Correct)
            {
                //result.Correct = true;
                alumno.Alumnos = result.Objects.ToList();
            }
            else
            {
                
                ViewBag.Mensaje = "No hay registros por mostrar";
            }
            return View(alumno);
        }
        [HttpGet]
        public ActionResult Form(int? IdAlumno)
        {
            ML.Alumno alumno = new ML.Alumno();
            
            //alumno.Alumnos=new List<object>();
            ServiceReferenceAlumno.ServiceAlumnoClient alumnoWCF = new ServiceReferenceAlumno.ServiceAlumnoClient();
            var resultadoAlumno = alumnoWCF.GetAll();

            if (IdAlumno!=null)//update
            {
                //ML.Result result=BL.Alumno.GetById(IdAlumno.Value);
                var result = alumnoWCF.GetById(IdAlumno.Value);
                if (result.Correct)
                {
                    alumno = (ML.Alumno)result.Object;
                    //alumno.Alumnos = result.Objects.ToList();
                    /* alumno.Alumnos = result.Objects.ToList();*/
                }
            }
            else//add
            {
               
            }
            return View(alumno);
        }
        [HttpPost]
        public ActionResult Form(ML.Alumno alumno) {
            ServiceReferenceAlumno.ServiceAlumnoClient alumnoWCF = new ServiceReferenceAlumno.ServiceAlumnoClient();

            if (alumno.IdAlumno==0)//Add
            {
                
                var result = alumnoWCF.Add(alumno);

                //ML.Result result=BL.Alumno.Add(alumno);
                if (result.Correct)
                {
                    ViewBag.Mensaje = "Alumno registrado correctamente";
                }
                else
                {
                    ViewBag.Mensaje = "Alumno no registrado correctamente";
                }
            }
            else
            {
                var result =alumnoWCF.Update(alumno);
                //ML.Result result = BL.Alumno.Update(alumno);
                if (result.Correct)
                {
                    ViewBag.Mensaje = "Alumno actualizado correctamente";
                }
                else
                {
                    ViewBag.Mensaje = "Alumno no actualizado correctamente";
                }
            }
            
            return PartialView("Modal");
        }
        [HttpGet]
        public ActionResult Delete(int IdAlumno)
        {
            ServiceReferenceAlumno.ServiceAlumnoClient alumnoWCF = new ServiceReferenceAlumno.ServiceAlumnoClient();
            var result = alumnoWCF.Delete(IdAlumno);
            //ML.Result result = BL.Alumno.Delete(IdAlumno);
            if (result.Correct)
            {
                ViewBag.Mensaje = "Alumno eliminado correctamente";
            }
            else
            {
                ViewBag.Mensaje = "Alumno no fue eliminado correctamente";
            }

            return PartialView("Modal");
        }
    }
}