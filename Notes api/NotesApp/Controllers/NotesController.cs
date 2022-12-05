using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using NotesApp.Data;
using NotesApp.Models.Entities;

namespace NotesApp.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class NotesController : Controller
    {
        private readonly NotesDbContext notesDbContext;

        // create a constructor and add the dbcontext as a parameter
        public NotesController(NotesDbContext notesDbContext)
        {
            this.notesDbContext = notesDbContext;
        }

        // action method to get all the notes from the db
        [HttpGet]
        public async Task<IActionResult> GetAllNotes()
        {
            return Ok(await notesDbContext.Notes.ToArrayAsync());
        }

        // action method to get a single note from the db
        [HttpGet("{id:Guid}")]
        [ActionName("GetNoteById")]
        public async Task<IActionResult> GetNoteById(Guid id)
        {
            var note = await notesDbContext.Notes.FindAsync(id);
            if (note == null)
            {
                return NotFound();
            }

            return Ok(note);
        }

        // action method to create a new note
        [HttpPost]
        public async Task<IActionResult> AddNote([FromBody] Note note)
        {
            note.Id = Guid.NewGuid(); 

            if (note == null)
            {
                return BadRequest();
            }

            await notesDbContext.Notes.AddAsync(note);
            await notesDbContext.SaveChangesAsync();

            return CreatedAtAction(nameof(GetNoteById), new { id = note.Id }, note);
        }
        [HttpPut]
        [Route("{id:Guid}")]
        // action method to update an existing note
        public async Task<IActionResult> UpdateNote([FromRoute] Guid id, [FromBody] Note note)
        {
            if (note == null)
            {
                return BadRequest();
            }

            var noteToUpdate = await notesDbContext.Notes.FindAsync(id);
            if (noteToUpdate == null)
            {
                return NotFound();
            }

            noteToUpdate.Title = note.Title;
            noteToUpdate.Description = note.Description;
            noteToUpdate.isVisibile = note.isVisibile;

            await notesDbContext.SaveChangesAsync();

            return NoContent();
        }

        // action method to delete a note
        [HttpDelete("{id:Guid}")]
        public async Task<IActionResult> DeleteNoteById(Guid id)
        {
            var note = await notesDbContext.Notes.FindAsync(id);
            if (note == null)
            {
                return NotFound();
            }

            notesDbContext.Notes.Remove(note);
            await notesDbContext.SaveChangesAsync();

            return NoContent();
        }
        

    }


}