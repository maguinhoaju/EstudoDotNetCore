using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AlugaTudo.Modelo;
using AlugaTudo.Manager;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using FluentValidation;
using FluentValidation.Results;

namespace AlugaTudo.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CarrosController : ControllerBase
    {
        private List<Carro> Carros;
        private readonly CarroValidator CarroValido = new CarroValidator();

        public CarrosController()
        {
            Carros = new List<Carro>();

            var carro = new Carro()
            {
                Id = 1,
                Nome = "Uno",
                Fabricante = "Fiat",
                Ano = 1997
            };

            Carros.Add(carro);

            carro = new Carro()
            {
                Id = 2,
                Nome = "X1",
                Fabricante = "BMW",
                Ano = 2018
            };

            Carros.Add(carro);
        } 


        [HttpGet]
        public ActionResult<List<Carro>> Get()
        {
            return Ok(Carros);
        }

        [HttpGet("{id}", Name = "Get")]
        public ActionResult<Carro> Get(int id)
        {
            var carro = Carros.Find(p => p.Id == id);

            if (carro == null)
            {
                return NotFound("Carro não encontrado.");
            }

            return Ok(carro);
        }

        [HttpPost]
        public ActionResult<ValidationResult> Post(Carro carro)
        {
            ValidationResult results = CarroValido.Validate(carro);
            if (!results.IsValid)
            {
                return BadRequest(results);
            }

            Carros.Add(carro);

            //Retorna o resultado contendo o objeto inserido. Para isso é necessário informar o método que irá consultar, o Id e o objeto passado como parâmetro
            return CreatedAtAction("Get", new { id = carro.Id }, carro);
        }

        [HttpPut("{id}")]
        public ActionResult Put(int id, [FromBody] Carro carro)
        {
            if (id != carro.Id)
            {
                return BadRequest();
            }

            var carroBase = Carros.Find(x => x.Id == id);

            if (carroBase == null)
            {
                return NotFound("Carro não encontrado");
            }

            Carros[Carros.FindIndex(x => x.Id == id)] = carro;

            // Retorno padrão do Put
            return NoContent();
        }

        [HttpDelete("{id}")]
        public ActionResult Delete(int id)
        {
            var carro = Carros.Find(x => x.Id == id);

            if (carro == null)
            {
                return NotFound("Carro não encontrado.");
            }

            Carros.Remove(carro);

            return Ok(carro);
        }
    }
}