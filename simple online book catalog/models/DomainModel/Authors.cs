using System.ComponentModel.DataAnnotations;

namespace simple_online_book_catalog.Models
{
    public class Authors
    {
       // [Key]
        public Guid Id { get; set; }
        public string Name { get; set; }
        public  string? photoOfTheAuthor { get; set; }
        public ICollection<Books> Books { get; set; }
    }
}
