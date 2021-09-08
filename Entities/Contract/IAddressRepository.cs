using Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Contract
{
    /// <summary>
    /// Interface de Contrato de Métodos do Endereço
    /// </summary>
    public interface IAddressRepository
    {
        public Address GetAddress(string cep);
    }
}
