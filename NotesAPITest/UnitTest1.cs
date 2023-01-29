using EntityFrameworkCoreMock;
using Microsoft.EntityFrameworkCore;
using NotesAPI.api.Controllers;
using NotesAPI.api.Data;
using NotesAPI.api.Models.DomainModel;

namespace NotesAPITest
{
    public class UnitTest1
    {
        
           private  DbContextMock<NotesDbContext> GetDbContext(Note[] initialEntities)
            {
                DbContextMock<NotesDbContext> dbContextMock = new DbContextMock<NotesDbContext>(new DbContextOptionsBuilder<NotesDbContext>().Options);
                dbContextMock.CreateDbSetMock(x => x.Notes, initialEntities);
                return dbContextMock;
            }
        

        private NotesController NotesControllerInit(DbContextMock<NotesDbContext> dbContextMock)
        {
            return new NotesController(dbContextMock.Object);
        }

        private Note[] getInitalDbEntries()
        {
            return new Note[]
            {
                new Note{Id=new Guid("cd2336eb-d1a2-4191-a655-9d2e7bde0f28"),Title="1", Description="2", NoteHex="000", CreatedDATE=DateTime.Now},
                new Note{Id=new Guid("48e9fa29-95de-42a5-a395-695678a4a23b"),Title="2", Description="2", NoteHex="FFF", CreatedDATE= DateTime.Now},
                new Note{Id=new Guid("87bfbbe8-b784-492f-8123-f9d5a6fe3082"),Title="3",Description="3", NoteHex="000",CreatedDATE=DateTime.Now}    

            };
        }
        [Fact]
        public void getAllReturnsAllResults()
        {
            //arrange
            DbContextMock<NotesDbContext> dbContextMock= GetDbContext(getInitalDbEntries());
            NotesController notesController = NotesControllerInit(dbContextMock);

            //act
            var result=notesController.GetAallNotes().ExecuteResultAsync;
            List<Note> value=result.Clone() as List<Note>;  

            //assert
            Assert.Equal(3,value.Count);

        }

        [Fact]
        public void getNoteByIdReturnsCorrectResult()
        {
            //arrange
            DbContextMock<NotesDbContext> dbContextMock = GetDbContext(getInitalDbEntries());
            NotesController notesController= NotesControllerInit(dbContextMock);

            //act
            var result = notesController.GetNoteById(new Guid("62FA647C-AD54-4BCC-A860-E5A2664B019D")).ExecuteResultAsync;
            var value = result.Clone();
            
            //assert
            Assert.IsType<Note>(value);
            Assert.Equal(new Guid("62FA647C-AD54-4BCC-A860-E5A2664B019D"), value);
        }
    }
}