using System;
using System.Collections.Generic;
using System.Text;

namespace Tegra.Teste.ViewModel.Response
{
    public class AeroportoListagemResponse
    {
        public string Codigo { get; private set; }
        public string Nome { get; private set; }

        public AeroportoListagemResponse(string codigo, string nome)
        {
            Codigo = codigo;
            Nome = nome;
        }
    }
}
