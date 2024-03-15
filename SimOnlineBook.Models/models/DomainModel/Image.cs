using Microsoft.AspNetCore.Http;
using simple_online_book_catalog.Models;
using System.ComponentModel.DataAnnotations.Schema;

namespace simple_online_book_catalog.models.DomainModel
{
    public class Image
    {
        public Guid Id { get; set; }
        [NotMapped]
        public IFormFile File { get; set; }
        public string fileName { get; set; }
        public string? fileDescription { get; set; }
        public string fileExtension { get; set; }
        public long fileSize { get; set; }
        public string filePath { get; set; }

        public Guid bookId { get; set; }
        public Books books { get; set; }
    }
}
