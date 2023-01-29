namespace NotesAPI.api.Models.DomainModel
{
    public class Note
    {
        public Guid Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public string NoteHex { get; set; }
        public DateTime CreatedDATE { get; set; }
    }
}
