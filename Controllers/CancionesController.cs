﻿using ApiNetCore.Models;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using static ApiNetCore.Models.CancionesModel;

namespace ApiNetCore.Controllers
{
    [ApiController]
    [Route("api/canciones")]
    public class CancionesController : ControllerBase 
    {
        [HttpDelete("/delete/{Id}")]
        public IActionResult Delete (int cancionABorrar)
        {
            bool cancionBorrada = DeleteById(cancionABorrar);

            if (cancionBorrada == true)
            {
                return Ok("Se borró con éxito");
            } else
            {
                return StatusCode(404, "Error, no se borró el registro");
            }
        }

        [HttpPut("{id}")]
        public IActionResult Update(Cancion CancionACrear)
        {
            string Errores;

            Cancion CancionCreada = CancionesModel.Update(CancionACrear, out Errores);

            if (CancionCreada != null)
            {
                return Ok(CancionCreada);
            }
            else
            {
                return StatusCode(409, Errores);
            }
        }

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
