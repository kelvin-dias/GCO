using Microsoft.AspNetCore.Mvc;
using Servico.Tabelas;

namespace GCO.Controllers
{
    public class DashboardController : Controller
    {
        private DashboardServico dashboardServico = new DashboardServico();

        [HttpGet]
        public ActionResult Index()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public JsonResult ObterDados()
        {
            ViewBag.QtdOcorrencias = dashboardServico.ObterQuantidadeOcorrenciaStatusAberto();

            return Json(ViewBag.QtdOcorrencias);
        }
    }
}