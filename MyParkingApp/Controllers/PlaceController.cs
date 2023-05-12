using System;
using System.Collections.Generic;
using System.Diagnostics.Metrics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using MyParkingApp.Models;
using MyParkingApp.Models.Dto;
using MyParkingApp.Repository;
using MySqlX.XDevAPI;

// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace MyParkingApp.Controllers
{
    [Route("api/[controller]")]
    public class PlaceController : ControllerBase
    {
        private readonly IPlace _place;
        protected ResponseDto _response;

        public PlaceController(IPlace place)
        {
            _place = place;
            _response = new ResponseDto();
        }

        // GET: /<controller>/
        [HttpGet("GetPlaces")]
        public async Task<ActionResult<IEnumerable<Place>>> GetPlaces()
        {
            try
            {
                var list = await _place.GetPlaces();
                _response.Result = list;
                _response.DisplayMessage = "List of places avaliable";
            }
            catch (Exception ex)
            {
                _response.IsSuccess = false;
                _response.ErrorMessages = new List<string> { ex.ToString() };
            }

            return Ok(_response.Result);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Place>> GetPlace(int id)
        {
            var client = await _place.GetPlace(id);
            if (client == null)
            {
                _response.IsSuccess = false;
                _response.DisplayMessage = "Place information";
                return NotFound(_response);
            }
            _response.Result = client;
            _response.DisplayMessage = "Place Information";
            return Ok(_response);
        }
    }
}

