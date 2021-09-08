using Domain.Contract;
using Entities;
using Newtonsoft.Json;
using System;
using System.IO;
using System.Net;
using System.Text.RegularExpressions;

namespace Repository
{
    public class AddressRepository : IAddressRepository
    {
        /// <summary>
        /// Implementação de consulta ao servidor externo ViaCEP
        /// </summary>
        /// <param name="cep">numero do cep a pesquisar</param>
        /// <returns>instancia de Address com o retorno da consulta externa</returns>
        public Address GetAddress(string cep)
        {
            try
            {
                //Criando uma instancia de requisição para o endereço do ViaCep
                HttpWebRequest request = (HttpWebRequest)WebRequest.Create(
                                            string.Concat("https://viacep.com.br/ws/", cep, "/json/"));
                request.AllowAutoRedirect = false;
                HttpWebResponse serverStatus = (HttpWebResponse)request.GetResponse();

                //valida se o servidor esta respondendo
                if (serverStatus.StatusCode != HttpStatusCode.OK)
                {
                    return null; // caso não responda retorna nulo
                }

                using (Stream streamResponse = serverStatus.GetResponseStream())//consulta a resposta do servidor
                {
                    if (streamResponse != null)//se houver resposta
                    {
                        using (StreamReader reader = new StreamReader(streamResponse))
                        {
                            Address myDeserializedAddress = JsonConvert.DeserializeObject<Address>(reader.ReadToEnd());
                            return myDeserializedAddress;
                        }
                    }
                }

                return new Address();
            }
            //caso haja erro de resposta do servidor (ocorre na falta de internet por parte do servidor)
            catch (WebException)
            {
                return null;
            }
        }
    }
}

