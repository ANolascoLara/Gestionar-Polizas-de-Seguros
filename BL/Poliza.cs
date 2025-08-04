using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DL;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using ML;

namespace BL
{
    public class Poliza
    {
        private readonly SistemaGestionPolizaContext _context;
        public Poliza(SistemaGestionPolizaContext context)
        {
            _context = context;
        }

        public ML.Result GetAllByIdUsuario(int IdUsuario)
        {
            ML.Result result = new ML.Result();

            try
            {
                var query = _context.PolizaDTO.FromSqlRaw($"PolizaGetAllByIdUsuario {IdUsuario}").ToList();

                if (query.Count > 0)
                {
                    result.Objects = new List<object>();

                    foreach (var item in query)
                    {
                        ML.Poliza poliza = new ML.Poliza();
                        poliza.TipoPoliza = new ML.TipoPoliza();
                        poliza.Estatus = new ML.Estatus();

                        // Asignar valores
                        poliza.NumeroPoliza = item.NumeroPoliza;
                        poliza.TipoPoliza.IdTipoPoliza = item.IdTipoPoliza;
                        poliza.TipoPoliza.Nombre = item.TipoPolizaNombre;
                        poliza.Estatus.IdEstatus = item.IdEstatus;
                        poliza.Estatus.NombreEstatus = item.EstatusNombre;
                        poliza.FechaInicio = item.FechaInicio;
                        poliza.FechaFinal = item.FechaFin;
                        poliza.MontoPrima = item.MontoPrima ?? 0;

                        result.Objects.Add(poliza);
                    }

                    result.Correct = true;
                }
                else
                {
                    result.Correct = false;
                    result.ErrorMessage = "No hay polizas";
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

        public ML.Result GetById(int numeroPoliza)
        {
            ML.Result result = new ML.Result();

            try
            {
                var query = _context.PolizaDTO.FromSqlRaw("EXEC PolizaGetById @NumeroPoliza", new SqlParameter("NumeroPoliza", numeroPoliza)).AsEnumerable().SingleOrDefault();


                if (query != null)
                {

                    ML.Poliza poliza = new ML.Poliza();
                    poliza.TipoPoliza = new ML.TipoPoliza();
                    poliza.Estatus = new ML.Estatus();
                    poliza.Usuario = new ML.Usuario();

                    // Asignar valores
                    poliza.NumeroPoliza = query.NumeroPoliza;
                    poliza.TipoPoliza.IdTipoPoliza = query.IdTipoPoliza;
                    poliza.TipoPoliza.Nombre = query.TipoPolizaNombre;
                    poliza.Estatus.IdEstatus = query.IdEstatus;
                    poliza.Estatus.NombreEstatus = query.EstatusNombre;
                    poliza.FechaInicio = query.FechaInicio;
                    poliza.FechaFinal = query.FechaFin;
                    poliza.MontoPrima = query.MontoPrima ?? 0;
                    poliza.Usuario.IdUsuario = query.IdUsuario;



                    result.Object = poliza;
                    result.Correct = true;
                }
                else
                {
                    result.Correct = false;
                    result.ErrorMessage = "No hay polizas";
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

        public ML.Result Add(ML.Poliza poliza)
        {
            ML.Result result = new ML.Result();
            try
            {
                int filasAfectadas = _context.Database.ExecuteSqlRaw(
                    "EXEC PolizaAdd @NumeroPoliza, @FechaInicio, @FechaFin, @MontoPrima, @IdTipoPoliza, @IdEstatus, @IdUsuario",
                    new SqlParameter("@NumeroPoliza", poliza.NumeroPoliza ?? (object)DBNull.Value),
                    new SqlParameter("@FechaInicio", DateTime.TryParse(poliza.FechaInicio, out DateTime fechaInicio) ? fechaInicio.Date : (object)DBNull.Value),
                    new SqlParameter("@FechaFin", DateTime.TryParse(poliza.FechaFinal, out DateTime fechaFin) ? fechaFin.Date : (object)DBNull.Value),
                    new SqlParameter("@MontoPrima", poliza.MontoPrima ?? (object)DBNull.Value),
                    new SqlParameter("@IdTipoPoliza", poliza.TipoPoliza.IdTipoPoliza ?? (object)DBNull.Value),
                    new SqlParameter("@IdEstatus", 1),
                    new SqlParameter("@IdUsuario", poliza.Usuario.IdUsuario ?? (object)DBNull.Value)
                );



                result.Correct = true;



            }
            catch (Exception ex)
            {
                result.Correct = false;
                result.ErrorMessage = ex.Message;
                result.Ex = ex;
            }

            return result;
        }

        public ML.Result Update(ML.Poliza poliza)
        {
            ML.Result result = new ML.Result();
            try
            {
                int filasAfectadas = _context.Database.ExecuteSqlRaw(
                    "EXEC PolizaUpdate @NumeroPoliza, @FechaInicio, @FechaFin, @MontoPrima, @IdTipoPoliza, @IdEstatus",
                    new SqlParameter("@NumeroPoliza", poliza.NumeroPoliza ?? (object)DBNull.Value),
                    new SqlParameter("@FechaInicio", poliza.FechaInicio ?? (object)DBNull.Value),
                    new SqlParameter("@FechaFin", poliza.FechaFinal ?? (object)DBNull.Value),
                    new SqlParameter("@MontoPrima", poliza.MontoPrima ?? (object)DBNull.Value),
                    new SqlParameter("@IdTipoPoliza", poliza.TipoPoliza.IdTipoPoliza ?? (object)DBNull.Value),
                    new SqlParameter("@IdEstatus", poliza.Estatus.IdEstatus ?? (object)DBNull.Value)
                );


                if (filasAfectadas > 0)
                {
                    result.Correct = true;
                }
                else
                {
                    result.Correct = false;
                    result.ErrorMessage = "No se pudo actualizar";
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

        public ML.Result Delete(int numeroPoliza)
        {
            ML.Result result = new ML.Result();
            try
            {
                int filasAfectadas = _context.Database.ExecuteSqlRaw(
                    "EXEC PolizaDelete @NumeroPoliza",
                    new SqlParameter("@NumeroPoliza", numeroPoliza)
                );

                result.Correct = true;

            }
            catch (Exception ex)
            {
                result.Correct = false;
                result.ErrorMessage = ex.Message;
                result.Ex = ex;
            }

            return result;
        }

        public ML.Result ObtenerNumeroPoliza()
        {
            ML.Result result = new ML.Result();

            try
            {
                var numeroPoliza = _context.Database
                    .SqlQuery<string>($"EXEC GenerarNumeroPoliza")
                    .AsEnumerable()
                    .FirstOrDefault();

                if (!string.IsNullOrEmpty(numeroPoliza))
                {
                    result.Object = numeroPoliza;
                    result.Correct = true;
                }
                else
                {
                    result.Correct = false;
                    result.ErrorMessage = "No se generó el número de póliza.";
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

    }
}
