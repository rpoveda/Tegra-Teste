using Microsoft.AspNetCore.Hosting;
using System;
using System.Collections.Generic;
using System.Text;
using Tegra.Teste.Domain;
using Tegra.Teste.Infra.Infra.Interface;
using Tegra.Teste.Infra.Repository.Interface;

namespace Tegra.Teste.Infra.Repository
{
    public class AeroportoRepository : IAeroportoRepository
    {

        private readonly IUtil _util;
        private IHostingEnvironment _env;

        public AeroportoRepository(IUtil util, IHostingEnvironment env)
        {
            _util = util;
            _env = env;
        }

        public IEnumerable<Aeroporto> Lista() => Newtonsoft.Json.JsonConvert.DeserializeObject<IEnumerable<Aeroporto>>(_util.Dados(System.IO.Path.Combine(_env.ContentRootPath, @"arquivos\aeroportos.json")));
    }
}
