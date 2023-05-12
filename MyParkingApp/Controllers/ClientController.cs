using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using MyParkingApp.Models;
using MyParkingApp.Models.Dto;
using MyParkingApp.Repository;
using Swashbuckle.AspNetCore.Annotations;

// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace MyParkingApp.Controllers
{
    [Route("api/[controller]")]
    public class ClientController : ControllerBase
    {
        private readonly IClient _client;
        protected ResponseDto _response;

        public ClientController(IClient client)
        {
            _client = client;
            _response = new ResponseDto();
        }

        // GET: api/Clients
        /// <summary>
        /// This method gets the list of customers
        /// </summary>
        /// <returns>Clients</returns>
        /// <response code="200">Ok</response>
        [HttpGet("GetClients")]
        public async Task <ActionResult<IEnumerable<Client>>> GetClients()
        {
            try
            {
                var list = await _client.GetClients();
                _response.Result = list;
                _response.DisplayMessage = "List of clients";
            }
            catch (Exception ex)
            {
                _response.IsSuccess = false;
                _response.ErrorMessages = new List<string> { ex.ToString() };
            }
            return Ok(_response.Result);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Client>> GetClient(int id)
        {
            var client = await _client.GetClient(id);
            if (client == null)
            {
                _response.IsSuccess = false;
                _response.DisplayMessage = "Client information";
                return NotFound(_response);
            }
            _response.Result = client;
            _response.DisplayMessage = "Client Information";
            return Ok(_response);
        }

        [HttpPost("CreateClient")]
        public async Task<ActionResult<Client>>CreateClient(
            [FromBody, SwaggerRequestBody("Customer data to create", Required =true)]ClientDto clientDto)
        {
            try
            {
                bool result = await _client.CreateClient(clientDto);
                _response.Result = result;
                return CreatedAtAction("GetClient", new {id = clientDto.Id}, _response);
            }
            catch (Exception ex)
            {
                _response.IsSuccess = false;
                _response.DisplayMessage = "Error to create the client";
                _response.ErrorMessages = new List<string> { ex.ToString() };
                return BadRequest(_response);
            }
        }
        [HttpPut("UpdateClient")]
        public async Task<IActionResult> UpdateDriver(ClientDto clientDto)
        {
            try
            {
                bool model = await _client.UpdateClient(clientDto);
                _response.Result = model;
                return Ok(_response);
            }
            catch (Exception ex)
            {
                _response.IsSuccess = false;
                _response.DisplayMessage = "Error to update the client";
                _response.ErrorMessages = new List<string> { ex.ToString() };
                return BadRequest(_response);
            }
        }
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteClient(int id)
        {
            try
            {
                bool isDeleted = await _client.DeleteClient(id);
                if (isDeleted)
                {
                    _response.Result = isDeleted;
                    _response.DisplayMessage = "Client removed successfully";
                    return Ok(_response);
                }
                else
                {
                    _response.IsSuccess = false;
                    _response.DisplayMessage = "Error to remove client";
                    return BadRequest(_response);
                }
            }
            catch (Exception ex)
            {
                _response.IsSuccess = false;
                _response.ErrorMessages = new List<string> { ex.ToString() };
                return BadRequest(_response);
            }
        }

    }
}

