using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace PL_MVC.Controllers
{
    [Authorize]
    public class PolizaController : Controller
    {
        private readonly IConfiguration _configuration;

        public PolizaController(IConfiguration configuration)
        {
            _configuration = configuration;
        }
        [HttpGet]
        public IActionResult GetAllByUsuario(int idUsuario)
        {
            ML.Poliza poliza = new ML.Poliza();
            poliza.Estatus = new ML.Estatus();

            ML.Result result = GetAllPolizaAPI(idUsuario);
            ML.Result resultEstatus = GetAllEstatusAPI();

            poliza.Polizas = result.Objects;
            poliza.Estatus.Estatuss = resultEstatus.Objects;

            ViewBag.IdUsuario = idUsuario;
            //CRUD POLIZA

            ViewBag.RolSession = HttpContext.Session.GetString("Rol");

            ViewBag.PolizaGetAllEndPoint = _configuration["EndPointsPoliza:PolizaGetAllEndPoint"];
            ViewBag.PolizaGetByIdEndPoint = _configuration["EndPointsPoliza:PolizaGetByIdEndPoint"];
            ViewBag.PolizaAddEndPoint = _configuration["EndPointsPoliza:PolizaAddEndPoint"];
            ViewBag.PolizaUpdateEndPoint = _configuration["EndPointsPoliza:PolizaUpdateEndPoint"];
            ViewBag.PolizaDeleteEndPoint = _configuration["EndPointsPoliza:PolizaDeleteEndPoint"];
            ViewBag.GeneroGetAllEndPoint = _configuration["EndPointsPoliza:GeneroGetAllEndPoint"];

            ViewBag.EstatusGetAllEndPoint = _configuration["EndPointsPoliza:EstatusGetAllEndPoint"];
            ViewBag.EstatusCambioEndPoint = _configuration["EndPointsPoliza:EstatusCambioEndPoint"];
            ViewBag.TipoPolizaGetAllEndPoint = _configuration["EndPointsPoliza:TipoPolizaGetAllEndPoint"];
            ViewBag.ObtenerNumeroPolizaEndPoint = _configuration["EndPointsPoliza:ObtenerNumeroPolizaEndPoint"];
            return View(poliza);
        }

        private ML.Result GetAllPolizaAPI(int idUsuario)
        {
            ML.Result resultPoliza = new ML.Result();
            resultPoliza.Objects = new List<Object>();

            try
            {
                using (var client = new HttpClient())
                {
                    string endpointPoliza = _configuration["EndPointsPoliza:PolizaGetAllEndPoint"] ?? "";

                    client.BaseAddress = new Uri(endpointPoliza);

                    var responseTask = client.GetAsync("" +idUsuario);
                    responseTask.Wait();

                    var result = responseTask.Result;

                    if (result.IsSuccessStatusCode)
                    {
                        var jsonString = result.Content.ReadAsStringAsync().Result;

                        // Deserializa toda la respuesta directamente como ML.Result
                        var resultFromApi = JsonConvert.DeserializeObject<ML.Result>(jsonString);

                        foreach (var resultItem in resultFromApi.Objects)
                        {
                            ML.Poliza resultItemList = JsonConvert.DeserializeObject<ML.Poliza>(resultItem.ToString());
                            resultPoliza.Objects.Add(resultItemList);
                        }

                        resultPoliza.Correct = true;
                    }
                    else
                    {
                        resultPoliza.Correct = false;
                        resultPoliza.ErrorMessage = "Error al obtener las pólizas.";
                    }
                }
            }
            catch (Exception ex)
            {
                resultPoliza.Correct = false;
                resultPoliza.ErrorMessage = ex.Message;
                resultPoliza.Ex = ex;
            }

            return resultPoliza;
        }

        private ML.Result GetAllEstatusAPI()
        {
            ML.Result resultPoliza = new ML.Result();
            resultPoliza.Objects = new List<Object>();

            try
            {
                using (var client = new HttpClient())
                {
                    string endpointPoliza = _configuration["EndPointsPoliza:EstatusGetAllEndPoint"] ?? "";

                    client.BaseAddress = new Uri(endpointPoliza);

                    var responseTask = client.GetAsync("");
                    responseTask.Wait();

                    var result = responseTask.Result;

                    if (result.IsSuccessStatusCode)
                    {
                        var jsonString = result.Content.ReadAsStringAsync().Result;

                        // Deserializa toda la respuesta directamente como ML.Result
                        var resultFromApi = JsonConvert.DeserializeObject<ML.Result>(jsonString);

                        foreach (var resultItem in resultFromApi.Objects)
                        {
                            ML.Estatus resultItemList = JsonConvert.DeserializeObject<ML.Estatus>(resultItem.ToString());
                            resultPoliza.Objects.Add(resultItemList);
                        }

                        resultPoliza.Correct = true;
                    }
                    else
                    {
                        resultPoliza.Correct = false;
                        resultPoliza.ErrorMessage = "Error al obtener las pólizas.";
                    }
                }
            }
            catch (Exception ex)
            {
                resultPoliza.Correct = false;
                resultPoliza.ErrorMessage = ex.Message;
                resultPoliza.Ex = ex;
            }

            return resultPoliza;
        }

    }
}
