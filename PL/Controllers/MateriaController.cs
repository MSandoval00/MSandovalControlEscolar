using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Net.Http;
using System.Web;
using System.Web.Mvc;

namespace PL.Controllers
{
    public class MateriaController : Controller
    {
        // GET: Materia
        //[HttpGet]
        //public ActionResult GetAll()
        //{
        //    ML.Result result = BL.Materia.GetAll();
        //    ML.Materia materia = new ML.Materia();
        //    materia.Materias = new List<object>();
        //    if (result.Correct)
        //    {
        //        materia.Materias = result.Objects;
        //    }
        //    else
        //    {
        //        ViewBag.Mensaje = "No hay registros en la tabla";
        //    }
        //    return View(materia);
        //}
        [HttpGet]
        public ActionResult GetAll()
        {
            ML.Result result=new ML.Result();
            ML.Materia materia=new ML.Materia();
            materia.Materias = new List<object>();
            using (var client =new HttpClient())
            {
                client.BaseAddress = new Uri(ConfigurationManager.AppSettings["WebApi"]);
                var responseTask = client.GetAsync("materia/");
                responseTask.Wait();

                var resultService=responseTask.Result;
                if (resultService.IsSuccessStatusCode)
                {
                    var readTask = resultService.Content.ReadAsAsync<ML.Result>();
                    readTask.Wait();
                    foreach (var resultMateria in readTask.Result.Objects)
                    {
                        ML.Materia resulItemList = Newtonsoft.Json.JsonConvert.DeserializeObject<ML.Materia>(resultMateria.ToString());
                        materia.Materias.Add(resulItemList);
                    }
                }
            }
            return View(materia);
        }

        //[HttpGet]
        //public ActionResult Form(int? IdMateria)
        //{
        //    ML.Materia materia = new ML.Materia();
        //    materia.Materias= new List<object>();
        //    if (IdMateria!=null)//update
        //    {
        //        ML.Result result = BL.Materia.GetById(IdMateria.Value);
        //        if (result.Correct)
        //        {
        //            materia = (ML.Materia)result.Object;
        //            materia.Materias = result.Objects;
        //        }
        //    }
        //    else//Add
        //    {

        //    }
        //    return View(materia);
        //}
        [HttpGet]
        public ActionResult Form(int? IdMateria)
        {
            ML.Materia materia = new ML.Materia();
            materia.Materias = new List<object>();
            if (IdMateria != null)//update
            {
                using (var client = new HttpClient())
                {
                    client.BaseAddress = new Uri(ConfigurationManager.AppSettings["WebApi"]);
                    var responseTask = client.GetAsync("materia/"+ IdMateria.Value);
                    responseTask.Wait();

                    var resultService = responseTask.Result;
                    if (resultService.IsSuccessStatusCode)
                    {
                        var readTask = resultService.Content.ReadAsAsync<ML.Result>();
                        readTask.Wait();


                        //materia = (ML.Result)readTask.Result.Object;
                        ML.Materia resultMateria = Newtonsoft.Json.JsonConvert.DeserializeObject<ML.Materia>(readTask.Result.Object.ToString());
                        materia = resultMateria;
                        //materia.Materias.Add(resultMateria);
                    }
                }
            }
            else//Add
            {

            }
            return View(materia);
        }
        //[HttpPost]
        //public ActionResult Form(ML.Materia materia)
        //{
        //    //ML.Result result = new ML.Result();
        //    if (materia.IdMateria==0)//Add
        //    {
        //        ML.Result result=BL.Materia.Add(materia);
        //        if (result.Correct)
        //        {
        //            ViewBag.Mensaje = "Materia agregada correctamente";
        //        }
        //        else
        //        {
        //            ViewBag.Mensaje = "No se pudo eliminar la materia";
        //        }
        //    }
        //    else
        //    {
        //        ML.Result result=BL.Materia.Update(materia);
        //        if (result.Correct)
        //        {
        //            ViewBag.Mensaje = "Materia actualizada correctamente";
        //        }
        //        else
        //        {
        //            ViewBag.Mensaje = "No se pudo actualizar la materia";
        //        }

        //    }
        //    return PartialView("Modal");
        //}
        [HttpPost]
        public ActionResult Form(ML.Materia materia)
        {
            //ML.Result result = new ML.Result();
            if (materia.IdMateria == 0)//Add
            {
                using (var client = new HttpClient())
                {
                    client.BaseAddress = new Uri(ConfigurationManager.AppSettings["WebApi"] + "materia");
                    var postTask = client.PostAsJsonAsync(client.BaseAddress, materia);
                    postTask.Wait();

                    var result = postTask.Result;
                    if (result.IsSuccessStatusCode)
                    {
                        return RedirectToAction("GetAll");

                    }
                }
            }
            else//update
            {
                using (var client = new HttpClient())
                {
                    client.BaseAddress = new Uri(ConfigurationManager.AppSettings["WebApi"]);
                    //Http Post
                    var postTask = client.PutAsJsonAsync("materia/"+ materia.IdMateria, materia);
                    postTask.Wait();

                    var result = postTask.Result;
                    if (result.IsSuccessStatusCode)
                    {
                        return RedirectToAction("GetAll");
                    }

                }

            }
            return PartialView("Modal");
        }
        //public ActionResult Delete(int IdMateria)
        //{
        //    ML.Result result = BL.Materia.Delete(IdMateria);
        //    if (result.Correct)
        //    {
        //        ViewBag.Mensaje = "Materia eliminada correctamente";
        //    }
        //    else
        //    {
        //        ViewBag.Mensaje = "No se pudo eliminar la materia";
        //    }
        //    return PartialView("Modal");
        //}
        public ActionResult Delete(int IdMateria)
        {
            ML.Result resultMateria = new ML.Result() ;
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri(ConfigurationManager.AppSettings["WebApi"]);
                var postTask = client.DeleteAsync("materia/" + IdMateria);
                postTask.Wait();
                var result = postTask.Result;
                if (result.IsSuccessStatusCode)
                {
                    resultMateria = BL.Materia.GetAll();
                    return RedirectToAction("GetAll", resultMateria);

                }
            }
            resultMateria = BL.Materia.GetAll();

            return View("GetAll",resultMateria);
        }
    }
}