using System;
using System.Collections.Generic;
using System.Text;
using Tegra.Teste.Domain;

namespace Tegra.Teste.Infra.Repository.Interface
{
    public interface IVooRepository
    {
        IEnumerable<Voo> Lista();
    }
}
