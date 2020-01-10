using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Modelo.Entidades;
using Servico.Entidades;
using Servico.Tabelas;
using System.Net;

namespace GCO.Controllers
{
    public class OcorrenciaController : Controller
    {
        private OcorrenciaServico ocorrenciaServico = new OcorrenciaServico();
        private IteracaoOcorrenciaServico iteracaoServico = new IteracaoOcorrenciaServico();

        // Métodos do Controlador Ocorrencia
        private ActionResult GravarOcorrencia(Ocorrencia ocorrencia)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    ocorrencia.NumeroOcorrencia = ocorrenciaServico.GerarNumeroOcorrencia();
                    ocorrencia.StatusOcorrencia = 0;
                    ocorrencia.DataHora = DateTime.Now;
                    ocorrenciaServico.GravarOcorrencia(ocorrencia);
                    return RedirectToAction("ConsultarOcorrencia");
                }
                return View(ocorrencia);
            }
            catch
            {
                return View(ocorrencia);
            }
        }

        private ActionResult ObterVisaoOcorrenciaPorId(long? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Ocorrencia produto = ocorrenciaServico.ObterOcorrenciaPorId((long)id);
            if (produto == null)
            {
                return HttpNotFound();
            }
            return View(produto);
        }

        [HttpGet]
        public ActionResult ConsultarOcorrencia()
        {
            return View(ocorrenciaServico.ObterOcorrenciasClassificadasPorDataHora());
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult AdicionarOcorrencia()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult AdicionarOcorrencia(Ocorrencia ocorrencia)
        {
            if (ocorrencia != null)
            {
                return GravarOcorrencia(ocorrencia);
            }
            else
            {
                return HttpNotFound();
            }
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult DetalharOcorrencia(long? id)
        {
            return ObterVisaoOcorrenciaPorId(id);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public JsonResult ListarIteracoes(long? Id)
        {
            var iteracoes = iteracaoServico.ObterIteracoes(Id);
            return Json(iteracoes);
        }

        //[HttpPost]
        //public ActionResult AdicionarIteracoes(IteracaoOcorrencia iteracao)
        //{

        //    return iteracaoServico.GravarIteracao(iteracao);

        //}

    }
}
}