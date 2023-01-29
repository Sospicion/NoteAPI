using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using NotesAPI.api.Data;
using NotesAPI.api.Models.DomainModel;
using NotesAPI.api.Models.DTO;

namespace NotesAPI.api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class NotesController : ControllerBase
    {
        private readonly NotesDbContext dbContext;

        public NotesController(NotesDbContext dbContext)
        {
            this.dbContext = dbContext;
        }
       
        [HttpPost]
        public IActionResult AddNote(AddNotesRequest addNotesRequest)
        {
            //Converting DTO to Domain Model
            var note = new Models.DomainModel.Note
            {
                Title = addNotesRequest.Title,
                Description = addNotesRequest.Description,
                NoteHex = addNotesRequest.NoteHex,
                CreatedDATE = DateTime.Now,
            };
            dbContext.Notes.Add(note);
            dbContext.SaveChanges();
            
            return Ok(note);
        }
        [HttpGet]
        public IActionResult GetAallNotes()
        {
            var notes= dbContext.Notes.ToList();
            var notesDTO = new List<Models.DTO.Note>();

            foreach (var note in notes) 
            {
                notesDTO.Add(new Models.DTO.Note
                {
                    Id = note.Id,
                    Title = note.Title,
                    Description = note.Description,
                    NoteHex = note.NoteHex,
                    CreatedDATE = note.CreatedDATE


                });
            }
            return Ok(notesDTO);
        }
        [HttpGet]
        [Route("{id:Guid}")]
        public IActionResult GetNoteById(Guid id)
        {
            var noteDomainObject= dbContext.Notes.Find(id);
            if (noteDomainObject != null) 
            {
                var noteDTO = new Models.DTO.Note
                {
                    Id = noteDomainObject.Id,
                    Title = noteDomainObject.Title,
                    Description = noteDomainObject.Description,
                    NoteHex = noteDomainObject.NoteHex,
                    CreatedDATE = noteDomainObject.CreatedDATE
                };

                return Ok(noteDTO);
            }
            return BadRequest("There is no Note with this ID");
        }
        [HttpPut]
        [Route("{id:Guid}")]
        public IActionResult UpdateNote(Guid id, UpdateNoteRequest updateNoteRequest)
        {
            var existingNote = dbContext.Notes.Find(id);
            if (existingNote != null)
            {
                existingNote.Title = updateNoteRequest.Title;
                existingNote.Description = updateNoteRequest.Description;
                existingNote.NoteHex= updateNoteRequest.NoteHex;
                dbContext.SaveChanges();
                return Ok(existingNote);
            }
            return BadRequest("There is no Note with this ID");
        }
        [HttpDelete]
        [Route("{id:Guid}")]
        public IActionResult DeleteNoteById(Guid id)
        {
            var existingNote = dbContext.Notes.Find(id);
            if (existingNote != null) 
            {
                dbContext.Remove(existingNote);
                dbContext.SaveChanges();
                return Ok("The Note with ID: "+ existingNote.Id+" was deleted successfully.");
            }
            return BadRequest("There is no Note with this ID");
        }
        
    }
}
