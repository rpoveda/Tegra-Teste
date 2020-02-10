using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Tegra.Teste.ViewModel.Response
{
    public class VooListagemResponse
    {
		public string Origem { get; set; }
		public string Destino { get; set; }
		public DateTime Data { get; set; }
		public decimal Total
		{
			get
			{
				return Trechos.Sum(x => x.Preco);
			}
		}
		public List<Trechos> Trechos { get; set; }

		public VooListagemResponse()
		{
			Trechos = new List<Trechos>();
		}
	}
}
