using Backend.Models;
using Microsoft.AspNetCore.Mvc;

namespace Backend.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class DBController : ControllerBase
    {
        private readonly Crud _crud;

        public DBController(Crud crud)
        {
            _crud = crud;
        }

        [HttpPost]
        public IActionResult CreateEntry([FromBody] Entry model)
        {
            _crud.CreateEntry(model);
            return CreatedAtAction(nameof(GetEntryById), new { id = model.Id }, model);
        }

        [HttpPut("{id}")]
        public IActionResult UpdateEntry(int id, [FromBody] Entry model)
        {
            var existingEntry = _crud.GetEntryById(model.Id);

            if (existingEntry == null)
            {
                return NotFound();
            }

            _crud.UpdateEntry(model);
            return NoContent();
        }

        [HttpDelete("{id}")]
        public IActionResult DeleteEntry(int id)
        {
            var existingEntry = _crud.GetEntryById(id);

            if (existingEntry == null)
            {
                return NotFound();
            }

            _crud.DeleteEntry(id);
            return NoContent();
        }

        [HttpGet]
        public IActionResult GetAllEntries()
        {
            var entries = _crud.GetAllEntries();
            return Ok(entries);
        }

        [HttpGet("{id}")]
        public IActionResult GetEntryById(int id)
        {
            var entry = _crud.GetEntryById(id);

            if (entry == null)
            {
                return NotFound();
            }

            return Ok(entry);
        }
    }
}
