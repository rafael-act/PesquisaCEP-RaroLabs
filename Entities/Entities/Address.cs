using System;

namespace Entities
{
    /// <summary>
    /// Classe de retorno da consulta de endereço
    /// </summary>
    public class Address
    {
        // Address myDeserializedAddress = JsonConvert.DeserializeObject<Address>(myResponse); 

        public string Cep { get; set; }
        public string Logradouro { get; set; }
        public string Complemento { get; set; }
        public string Bairro { get; set; }
        public string Localidade { get; set; }
        public string Uf { get; set; }
        public string Ibge { get; set; }
        public string Gia { get; set; }
        public string DDD { get; set; }
        public string Siafi { get; set; }
    }
}
