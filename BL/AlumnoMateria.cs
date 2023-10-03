using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BL
{
    public class AlumnoMateria
    {
        public static ML.Result GetMateriasAsignadas(int IdAlumno)
        {
            ML.Result result = new ML.Result();
            try
            {
                using (DL.MSandovalControlEscolarEntities context =new DL.MSandovalControlEscolarEntities())
                {
                    var query = context.GetMateriasAsignadas(IdAlumno).ToList();
                    result.Objects=new List<object>();
                        foreach (var row in query)
                        {
                        ML.AlumnoMateria alumnoMateria = new ML.AlumnoMateria();
                        alumnoMateria.Alumno = new ML.Alumno();
                        alumnoMateria.Alumno.IdAlumno = row.IdAlumno;
                        alumnoMateria.Materia = new ML.Materia();
                        alumnoMateria.Materia.IdMateria = row.IdMateria;
                        alumnoMateria.Materia.Nombre = row.Nombre;
                        alumnoMateria.Materia.Costo = decimal.Parse(row.Costo.ToString());
                        result.Objects.Add(alumnoMateria);
                    }
                        result.Correct = true; 
                    
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
        public static ML.Result DeleteMateria(int IdMateria)
        {
            ML.Result result = new ML.Result();
            try
            {
                using (DL.MSandovalControlEscolarEntities context =new DL.MSandovalControlEscolarEntities())
                {
                    var query = context.AlumnoMateriaDelete(IdMateria);
                    if (query>0)
                    {
                        result.Correct=true;
                    }
                    else
                    {
                        result.Correct=false;
                        result.ErrorMessage = "No se pudo eliminar la materia";
                    }

                }
            }
            catch (Exception ex)
            {
                result.Correct = false;
                result.ErrorMessage=ex.Message;
                result.Ex= ex;
            }
            return result;

        }
    }
}
