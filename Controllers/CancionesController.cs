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
        [HttpPost]
        public IActionResult Create (Cancion CancionACrear)
        {
            string Errores;
            Cancion CancionCreada = CancionesModel.CreateNew(CancionACrear, out Errores);

            if (CancionCreada != null)
            {
                return Ok(CancionCreada);
            } else
            {
                return StatusCode(409, Errores);
            }
        }

        [HttpGet("{yearFrom}/{YearTo}")]
        public IActionResult GetById(int YearFrom, int YearTo)
        {
            List<Cancion> listaPorAnio = new List<Cancion>();

            return Ok("Ok Anio desde " + YearFrom + " hasta " + YearTo);
        }

        [HttpGet("{id}")]
        public IActionResult GetById (int Id)
        {
            Cancion unaCancion = CancionesModel.GetById(Id);
            if (unaCancion != null)
            {
                return Ok(unaCancion);
            } else
            {
                return StatusCode(404, "Id inexistente");
            }
        }



        [HttpGet]
        public IActionResult Get()
        {
            List<Cancion> listaCanciones = CancionesModel.getAll();

            return Ok(listaCanciones);
        }
    }
}
