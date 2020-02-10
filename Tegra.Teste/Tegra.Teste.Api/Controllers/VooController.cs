using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Tegra.Teste.Application.Application.Interface;
using Tegra.Teste.ViewModel.Request;
using Tegra.Teste.ViewModel.Response;

namespace Tegra.Teste.Api.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class VooController : ControllerBase
    {
        private readonly IVooApplication _vooApplication;

        public VooController(IVooApplication vooApplication)
        {
            _vooApplication = vooApplication;
        }

        [HttpPost]
        public IActionResult Post([FromBody]VooListagemRequest request) => Ok(_vooApplication.Listagem(request));
    }
}