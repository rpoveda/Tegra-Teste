using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using System;
using System.Collections.Generic;
using System.Text;

namespace Tegra.Teste.Domain
{
    public class Voo
    {
        [JsonProperty("Voo")]
        public string Id { get; private set; }
        public string Origem { get; private set; }
        public string Destino { get; private set; }
        [JsonConverter(typeof(DateFormatConverter), "yyyy-MM-dd")]
        [JsonProperty("Data_Saida")]
        public DateTime DataSaida { get; private set; }
        [JsonIgnore]
        public DateTime Saida
        {
            get
            {
                return DateTime.Parse($"{this.DataSaida.ToString("yyyy-MM-dd")} {this.HoraSaida}");
            }

        }
        [JsonIgnore]
        public DateTime Chegada
        {
            get
            {
                return DateTime.Parse($"{this.DataSaida.ToString("yyyy-MM-dd")} {this.HoraChegada}");
            }
        }
        [JsonProperty("Saida")]
        public string HoraSaida { get; private set; }
        [JsonProperty("Chegada")]
        public string HoraChegada { get; private set; }
        public string Operadora
        {
            get; set;
        } = "99Planes";
        [JsonProperty("Valor")]
        public Decimal Preco { get; private set; }

        public Voo(string id, string origem, string destino, DateTime dataSaida, string horaSaida, string horaChegada, decimal preco, string operadora = "99Planes")
        {
            Id = id;
            Origem = origem;
            Destino = destino;
            DataSaida = dataSaida;
            HoraSaida = horaSaida;
            HoraChegada = horaChegada;
            Operadora = operadora;
            Preco = preco;
        }
    }

    public class DateFormatConverter : IsoDateTimeConverter
    {
        public DateFormatConverter(string format)
        {
            DateTimeFormat = format;
        }
    }
}
