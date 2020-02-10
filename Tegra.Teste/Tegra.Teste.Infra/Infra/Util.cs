using System;
using System.Collections.Generic;
using System.Text;
using Tegra.Teste.Infra.Infra.Interface;

namespace Tegra.Teste.Infra.Infra
{
    public class Util : IUtil
    {
        public string Dados(string path)
        {
            using (var reader = new System.IO.StreamReader(path))
            {
                return reader.ReadToEnd();
            }
        }
    }
}
