using CandidateApp.Business.Contracts;
using CandidateApp.Business.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace CandidateApp.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class DegreesController : ControllerBase
    {
        private readonly ICrudeService<Degree> _degreeService;

        public DegreesController(ICrudeService<Degree> degreeService)
        {
            _degreeService = degreeService;
        }

        [HttpGet]
        public IActionResult Get() => Ok(_degreeService.GetAll());

        [HttpGet("{id}")]
        public IActionResult GetById(long id) => Ok(_degreeService.Get(id));


        [HttpPost]
        public IActionResult Create(Degree model)
        {
            Degree createdEntity = _degreeService.Create(model);

            return CreatedAtAction(nameof(GetById), new { id = createdEntity.Id }, createdEntity);
        }

        [HttpPut]
        public IActionResult Update(Degree entityToUpdate) => Ok(_degreeService.Update(entityToUpdate));

        [HttpDelete("{id}")]
        public IActionResult Delete(long id)
        {
            _degreeService.Delete(id);
            return NoContent();
        }
    }
}
