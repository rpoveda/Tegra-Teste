using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace Tegra.Teste.Domain
{
    public class Aeroporto
    {
        public string Nome { get; private set; }
        [JsonProperty("Aeroporto")]
        public string Codigo { get; private set; }
        public string Cidade { get; private set; }

        public Aeroporto(string nome, string codigo, string cidade)
        {
            Nome = nome;
            Codigo = codigo;
            Cidade = cidade;
        }
    }
}
