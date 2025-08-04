using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DL;
using Microsoft.EntityFrameworkCore;
using ML;

namespace BL
{
    public class Estatus
    {
        private readonly SistemaGestionPolizaContext _context;
        public Estatus(SistemaGestionPolizaContext context)
        {
            _context = context;
        }

        public ML.Result GetAll()
        {
            ML.Result result = new ML.Result();

            try
            {

                var usuarios = _context.Estatuses.ToList();

                if(usuarios.Count > 0)
                {
                    result.Objects = new List<object>();

                    foreach (var item in usuarios)
                    {
                        ML.Estatus estatus = new ML.Estatus();
                        estatus.IdEstatus = item.IdEstatus;
                        estatus.NombreEstatus = item.Nombre;
                        result.Objects.Add(estatus);
                    }

                    result.Correct = true;
                } else
                {
                    result.Correct = false;
                    result.ErrorMessage = "No hay estatus";
                }

                    result.Correct = true;
            }
            catch (Exception ex)
            {
                result.Correct = false;
                result.ErrorMessage = "Ocurrió un error al obtener los usuarios.";
                result.Ex = ex;
            }

            return result;
        }

        public ML.Result CambiarEstatus(int numeroPoliza, int idEstatus)
        {
            ML.Result result = new ML.Result();

            try
            {
                var query = _context.Polizas.FirstOrDefault(p => p.NumeroPoliza == numeroPoliza);
                if (query == null)
                {

                    result.Correct = false;
                } else
                {
                    query.IdEstatus = idEstatus;
                    _context.SaveChanges();
                    result.Correct = true;
                }

                    

            } catch(Exception ex)
            {
                result.Correct = false;
                result.ErrorMessage = ex.Message;
                result.Ex = ex;
            }

            return result;
        }
    }
}
