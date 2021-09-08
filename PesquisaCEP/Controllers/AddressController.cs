using Domain.Contract;
using Entities;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Threading.Tasks;

namespace PesquisaCEP.Controllers
{
    /// <summary>
    /// Controller para consulta de endereços
    /// </summary>
    [ApiController]
    [Route("[controller]")]
    [ProducesResponseType(typeof(string), StatusCodes.Status500InternalServerError)]
    public class AddressController : ControllerBase
    {
        private readonly ILogger<AddressController> _logger;
        private readonly IAddressRepository _addressRepository;
        public AddressController(IAddressRepository addressRepository, ILogger<AddressController> logger)
        {
            _logger = logger;
            _addressRepository = addressRepository;
        }

        /// <summary>
        /// Api para consulta do cep 
        /// </summary>
        /// <param name="cep">string com o valor do cep a ser consultado</param>
        /// <returns></returns>
        [HttpGet]
        public async Task<IActionResult> Get(string cep)
        {
            if (cep is null)
            {
                _logger.LogError("O CEP está nulo! Envie o CEP para consulta!");
                return Problem("O CEP está nulo! Envie o CEP para consulta!");
            }

            cep = cep.Replace("-", "");//retira traço do cep

            if (cep.Length != 8)
            {
                _logger.LogError("Tamanho do CEP inválido");
                return Problem("Tamanho do CEP inválido");
            }

            if (!int.TryParse(cep, out _))//verifica se ha somente números
            {
                _logger.LogError("CEP inválido");
                return Problem("CEP inválido");
            }

            Address result = new Address();

            CultureInfo cult = new CultureInfo("pt-BR");
            string data = DateTime.Now.ToString("dd/MM/yyyy HH:mm:ss", cult);
            _logger.LogInformation("Iniciando consulta às {0}", data);

            await Task.Factory.StartNew(() =>
            {
                result = _addressRepository.GetAddress(cep);
            });

            data = DateTime.Now.ToString("dd/MM/yyyy HH:mm:ss", cult);
            _logger.LogInformation("Consulta encerrada às {0}", data);

            if (result is null)
            {
                _logger.LogError("Servidor ViaCEP inacessível!");
                return Problem("Servidor ViaCEP inacessível!");
            }

            return Ok(result);
        }
    }
}
