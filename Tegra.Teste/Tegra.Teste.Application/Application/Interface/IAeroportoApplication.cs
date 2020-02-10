using System;
using System.Collections.Generic;
using System.Text;
using Tegra.Teste.ViewModel.Response;

namespace Tegra.Teste.Application.Application.Interface
{
    public interface IAeroportoApplication
    {
        List<AeroportoListagemResponse> Listagem();
    }
}
