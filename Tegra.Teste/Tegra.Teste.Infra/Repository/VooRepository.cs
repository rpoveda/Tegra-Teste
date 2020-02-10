using Microsoft.AspNetCore.Hosting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Tegra.Teste.Domain;
using Tegra.Teste.Infra.Infra.Interface;
using Tegra.Teste.Infra.Repository.Interface;

namespace Tegra.Teste.Infra.Repository
{
    public class VooRepository : IVooRepository
    {

        private readonly IUtil _util;
        private readonly IHostingEnvironment _env;

        public VooRepository(IUtil util, IHostingEnvironment env)
        {
            _util = util;
            _env = env;
        }

        public IEnumerable<Voo> Lista()
        {
            var ret = new List<Voo>();

            ret.AddRange(Lista99Planes().ToList());
            ret.AddRange(ListaUberAir().ToList());

            return ret;
        }

        private IEnumerable<Voo> Lista99Planes() => Newtonsoft.Json.JsonConvert.DeserializeObject<List<Voo>>(_util.Dados(System.IO.Path.Combine(_env.ContentRootPath, @"arquivos\99planes.json")));

        private IEnumerable<Voo> ListaUberAir()
        {
            var dados = _util.Dados(System.IO.Path.Combine(_env.ContentRootPath, @"arquivos\uberair.csv")).Split(Environment.NewLine);
            var count = 0;
            foreach(var item in dados)
            {
                if (count == 0)
                {
                    count += 1;
                    continue;
                }


                var linha = item.Split(",");
                yield return new Voo(linha[0], linha[1], linha[2], DateTime.Parse(linha[3].ToString()), linha[4], linha[5], Decimal.Parse(linha[6]), "UberAir");
            }
        }
    }
}
