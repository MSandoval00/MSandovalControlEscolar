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
        public static ML.Result GetAll()
        {
            ML.Result result = new ML.Result();
            try
            {
                using (SqlConnection context = new SqlConnection(DL.Conexion.Get()))
                {
                    string query ="SELECT idMateria,Nombre,Costo FROM Materias";
                   SqlCommand cmd = new SqlCommand(query,context);
                    SqlDataAdapter adapter = new SqlDataAdapter(cmd);
                    DataTable tablamateria = new DataTable();
                    adapter.Fill(tablamateria);
                    if (tablamateria.Rows.Count>0)
                    {
                        result.Objects=new List<object>();
                        foreach (DataRow row in tablamateria.Rows)
                        {
                            ML.Materia materia = new ML.Materia();
                            materia.IdMateria= int.Parse(row[0].ToString());
                            materia.Nombre= row[1].ToString();
                            materia.Costo =decimal.Parse(row[2].ToString());

                            result.Objects.Add(materia);
                        }
                        result.Correct = true;
                    }
                    else
                    {
                        result.Correct = false;
                        result.ErrorMessage = "No hay registros en la tabla";
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
    }
}
