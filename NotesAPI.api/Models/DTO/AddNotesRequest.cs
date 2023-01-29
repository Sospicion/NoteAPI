namespace NotesAPI.api.Models.DTO
{
    public class AddNotesRequest
    {
        public string Title { get; set; }
        public string Description { get; set; }
        public string NoteHex { get; set; }
    }
}
