using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BL
{
    public class Materia
    {
        public static ML.Result Add(ML.Materia materia)
        {
            ML.Result result = new ML.Result();
            try
            {
                using (DL.MSandovalControlEscolarEntities context=new DL.MSandovalControlEscolarEntities())
                {
                    var query = context.MateriaAdd(materia.Nombre,materia.Costo);
                    if (query>=1)
                    {
                        result.Correct = true;
                    }
                    else
                    {
                        result.Correct=false;
                        result.ErrorMessage = "No se pudo insertar materia";
                    }

                }
            }
            catch (Exception ex)
            {
                result.Correct = false;
                result.ErrorMessage = ex.Message;
                result.Ex=ex;
            }
            return result;
        }
        public static ML.Result Update(ML.Materia materia)
        {
            ML.Result result = new ML.Result();
            try
            {
                using (DL.MSandovalControlEscolarEntities context = new DL.MSandovalControlEscolarEntities())
                {
                    var query = context.MateriaUpdate(materia.IdMateria,materia.Nombre,materia.Costo);
                    if (query > 0)
                    {
                        result.Correct = true;
                    }
                    else
                    {
                        result.Correct = false;
                        result.ErrorMessage = "No se pudo actualizar materia";
                    }

                }
            }
            catch (Exception ex)
            {
                result.Correct = false;
                result.ErrorMessage = ex.Message;
                result.Ex = ex;
            }
            return result;
        }
        public static ML.Result Delete(int IdMateria)
        {
            ML.Result result = new ML.Result();
            try
            {
                using (DL.MSandovalControlEscolarEntities context=new DL.MSandovalControlEscolarEntities())
                {
                    var query = context.MateriaDelete(IdMateria);
                    if (query>0)
                    {
                        result.Correct = true;
                    }
                    else
                    {
                        result.Correct= false;
                        result.ErrorMessage = "No se pudo eliminar la materia";
                    }
                }
            }
            catch (Exception ex)
            {

                result.Correct = false;
                result.ErrorMessage= ex.Message;
                result.Ex= ex;
            }
            return result;
        }
        public static ML.Result GetAll()
        {
            ML.Result result = new ML.Result();
            try
            {
                using(DL.MSandovalControlEscolarEntities context=new DL.MSandovalControlEscolarEntities())
                {
                    var query=context.MateriaGetAll().ToList();
                    result.Objects = new List<object>();
                    foreach (var row in query)
                    {
                        ML.Materia materia =new ML.Materia();
                        materia.IdMateria = row.IdMateria;
                        materia.Nombre = row.Nombre;
                        materia.Costo = decimal.Parse(row.Costo.ToString());

                        result.Objects.Add(materia);

                    }
                    result.Correct = true;
                }

            }
            catch (Exception ex)
            {
                result.Correct = false;
                result.ErrorMessage = ex.Message;
                result.Ex = ex;

            }
            return result;
        }
        public static ML.Result GetById(int IdMateria)
        {
            ML.Result result=new ML.Result();
            try
            {
                using (DL.MSandovalControlEscolarEntities context=new DL.MSandovalControlEscolarEntities())
                {
                    var query = context.MateriaGetById(IdMateria).FirstOrDefault();
                    result.Object=new List<object>();
                    if (query!=null)
                    {
                        ML.Materia materia=new ML.Materia();
                        materia.IdMateria=query.IdMateria;
                        materia.Nombre=query.Nombre;
                        materia.Costo=decimal.Parse(query.Costo.ToString());

                        result.Object = materia;
                        result.Correct = true;
                    }

                }

            }
            catch (Exception ex)
            {
                result.Correct = false;
                result.ErrorMessage=ex.Message;
                result.Ex=ex;
               
            }
            return result ;
        }
    }
}
