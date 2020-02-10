namespace Tegra.Teste.ViewModel.Response
{
    public class Trechos
    {

		public string Origem { get; set; }
		public string Destino { get; set; }
		public System.DateTime Saida { get; set; }
		public System.DateTime Chegada { get; set; }
		public decimal Preco { get; set; }
		public string HoraSaida
		{
			get
			{
				return Saida.ToString("HH:mm");
			}
		}
		public string HoraChegada
		{
			get
			{
				return Chegada.ToString("HH:mm");
			}
		}
		public Trechos()
		{

		}
	}
}