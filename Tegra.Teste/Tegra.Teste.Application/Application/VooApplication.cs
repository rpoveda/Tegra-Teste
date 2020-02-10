using Microsoft.Extensions.Caching.Memory;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Tegra.Teste.Application.Application.Interface;
using Tegra.Teste.Domain;
using Tegra.Teste.Infra.Repository.Interface;
using Tegra.Teste.ViewModel.Request;
using Tegra.Teste.ViewModel.Response;

namespace Tegra.Teste.Application.Application
{
    public class VooApplication : IVooApplication
    {
        private readonly IVooRepository _vooRepository;
        private readonly IMemoryCache _cache;

        public VooApplication(IVooRepository vooRepository, IMemoryCache cache)
        {
            _vooRepository = vooRepository;
            _cache = cache;
        }

        public List<VooListagemResponse> Listagem(VooListagemRequest request)
        {
            var cache = _cache.Get($"{request.De}{request.Para}{request.Data}");

            if(cache == null)
            {
                _cache.Set($"{request.De}{request.Para}{request.Data}", ListaCache(request));
            }

            return (List<VooListagemResponse>)_cache.Get($"{request.De}{request.Para}{request.Data}");
        }

        public VooListagemResponse Busca(List<Voo> dados, VooListagemResponse item, string destinoFinal)
        {
            var _last = item.Trechos.Last();
            var _busca = dados.Where(x => x.Origem == _last.Destino).FirstOrDefault();

            if (_busca != null)
            {
                item.Trechos.Add(new Trechos
                {
                    Chegada = _busca.Chegada,
                    Destino = _busca.Destino,
                    Origem = _busca.Origem,
                    Preco = _busca.Preco,
                    Saida = _busca.Saida
                });

                if (_busca.Destino == destinoFinal)
                    return item;
                else
                    Busca(dados, item, destinoFinal);
            }

            return item;
        }

        private List<VooListagemResponse> ListaCache(VooListagemRequest request)
        {
            var ret = new List<VooListagemResponse>();
            var dados = ListaCache().Where(x => x.DataSaida.ToString("yyy-MM-dd") == request.Data).ToList();
            var dadosCompletos = dados.Where(x => x.Origem == request.De && x.Destino == request.Para).ToList();

            dadosCompletos.ForEach(item =>
            {
                ret.Add(new VooListagemResponse
                {
                    Destino = request.Para,
                    Origem = request.De,
                    Data = DateTime.Parse(request.Data),
                    Trechos = new List<Trechos> { new Trechos
                    {
                        Chegada = item.Chegada,
                        Destino = item.Destino,
                        Origem = item.Origem,
                        Preco = item.Preco,
                        Saida = item.Saida
                    } }
                });
            });

            var iniciais = dados.Where(x => x.Origem == request.De && x.Destino != request.Para).ToList();

            foreach (var item in iniciais)
            {
                var _item = new VooListagemResponse();
                _item.Destino = request.Para;
                _item.Origem = request.De;
                _item.Data = DateTime.Parse(request.Data);

                _item.Trechos.Add(new Trechos
                {
                    Chegada = item.Chegada,
                    Destino = item.Destino,
                    Origem = item.Origem,
                    Preco = item.Preco,
                    Saida = item.Saida
                });
                _item = Busca(dados, _item, request.Para);
                ret.Add(_item);
            }

            return ret;
        }

        private List<Voo> ListaCache()
        {
            var dados = _cache.Get("voos");

            if (dados == null)
            {
                _cache.Set("voos", _vooRepository.Lista());
            }

            return (List<Voo>)_cache.Get("voos");
        }
    }
}
