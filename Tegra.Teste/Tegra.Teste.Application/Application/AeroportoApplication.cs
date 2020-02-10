using Microsoft.Extensions.Caching.Memory;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Tegra.Teste.Application.Application.Interface;
using Tegra.Teste.Infra.Repository.Interface;
using Tegra.Teste.ViewModel.Response;

namespace Tegra.Teste.Application.Application
{
    public class AeroportoApplication : IAeroportoApplication
    {

        private readonly IAeroportoRepository _aeroportoRepository;
        private readonly IMemoryCache _cache;

        public AeroportoApplication(IAeroportoRepository aeroportoRepository, IMemoryCache cache)
        {
            _aeroportoRepository = aeroportoRepository;
            _cache = cache;
        }

        public List<AeroportoListagemResponse> Listagem()
        {
            var aeroportos = _cache.Get("aeroportos");

            if (aeroportos == null)
            {
                _cache.Set("aeroportos", _aeroportoRepository.Lista().Select(x => new AeroportoListagemResponse(x.Codigo, x.Nome)).ToList());
            }

            return (List<AeroportoListagemResponse>)_cache.Get("aeroportos");
        }
    }
}
