using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Tegra.Teste.Application.Application.Interface;

namespace Tegra.Teste.Api.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class AeroportoController : ControllerBase
    {
        private readonly IAeroportoApplication _aeroportoApplication;

        public AeroportoController(IAeroportoApplication aeroportoApplication)
        {
            _aeroportoApplication = aeroportoApplication;
        }

        [HttpGet]
        public IActionResult Get() => Ok(_aeroportoApplication.Listagem());

    }
}