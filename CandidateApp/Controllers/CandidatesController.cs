using CandidateApp.Business.Contracts;
using CandidateApp.Business.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace CandidateApp.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class CandidatesController(ICrudeService<Candidate> candidateService) : ControllerBase
    {

        private readonly ICrudeService<Candidate> _candidateService = candidateService;

        [HttpGet]
        public IActionResult Get() => Ok(_candidateService.GetAll());

        [HttpGet("{id}")]
        public IActionResult GetById(long id) => Ok(_candidateService.Get(id));


        [HttpPost]
        public IActionResult Create(Candidate model)
        {
            Candidate createdEntity = _candidateService.Create(model);

            return CreatedAtAction(nameof(GetById), new { id = createdEntity.Id }, createdEntity);
        }

        [HttpPut]
        public IActionResult Update(Candidate entityToUpdate) => Ok(_candidateService.Update(entityToUpdate));

        [HttpDelete("{id}")]
        public IActionResult Delete(long id)
        {
            _candidateService.Delete(id);
            return NoContent();
        }
    }
}
