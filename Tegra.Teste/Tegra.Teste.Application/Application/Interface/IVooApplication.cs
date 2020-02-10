using System;
using System.Collections.Generic;
using System.Text;
using Tegra.Teste.ViewModel.Request;
using Tegra.Teste.ViewModel.Response;

namespace Tegra.Teste.Application.Application.Interface
{
    public interface IVooApplication
    {
        List<VooListagemResponse> Listagem(VooListagemRequest request);
    }
}
