
namespace simple_online_book_catalog.Models
{
    public class Authors
    {
       // [Key]
        public Guid Id { get; set; }
        public string Name { get; set; }
        public  string? photoOfTheAuthor { get; set; }
        public List<Books> Books { get; set; }
    }
}
