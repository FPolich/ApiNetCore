using ApiNetCore.Models;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using static ApiNetCore.Models.CancionesModel;

namespace ApiNetCore.Controllers
{
    [ApiController]
    [Route("api/canciones")]
    public class CancionesController : ControllerBase
    {


        [HttpGet]
        public IActionResult Get()
        {
            List<Cancion> listaCanciones = CancionesModel.getAll();
            
            return Ok(listaCanciones);
        }
    }
}
