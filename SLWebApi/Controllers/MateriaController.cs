using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace SLWebApi.Controllers
{
    [RoutePrefix("api/materia")]
    public class MateriaController : ApiController
    {
        [HttpPost]
        [Route("")]
        public IHttpActionResult Add([FromBody] ML.Materia materia)
        {
            var result = BL.Materia.Add(materia);
            if (result.Correct)
            {
                return Ok(result);
            }
            else
            {
                return NotFound();
            }
        }
        [HttpDelete]
        [Route("{Idmateria}")]
        public IHttpActionResult Delete(int IdMateria)
        {
            ML.Materia materia = new ML.Materia();
            materia.IdMateria = IdMateria;
            var result = BL.Materia.Delete(materia.IdMateria);
            if (result.Correct)
            {
                return Ok(result);
            }
            else
            {
                return NotFound();
            }
        }
        [HttpPut]
        [Route("{Idmateria}")]
        public IHttpActionResult Update(int IdMateria, [FromBody] ML.Materia materia)
        {
            var result = BL.Materia.Update(materia);
            if (result.Correct)
            {
                return Ok(result);
            }
            else
            {
                return NotFound();
            }
        }
        [HttpGet]
        [Route("")]
        public IHttpActionResult GetAll()
        {
            ML.Result result = BL.Materia.GetAll();
            if (result.Correct)
            {
                return Ok(result);
            }
            else
            {
                return NotFound();
            }
        }
        [HttpGet]
        [Route("{IdMateria}")]
        public IHttpActionResult GetById(int IdMateria)
        {
            ML.Result result = BL.Materia.GetById(IdMateria);
            if (result.Correct)
            {
                return Ok(result);
            }
            else
            {
                return NotFound();
            }
        }

    }
}
