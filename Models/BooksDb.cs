using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Assignment7_DevynSmith_Section3.Models
{
    //book class to ensure the data is entered into the database correctly
    public class Book
    {
        //this field is the key for each entry, auto entered by having the key tag
        [Key]
        [Required]
        public int BookId { get; set; }
        [Required]
        public string Title { get; set; }
        [Required]
        public string AuthorFirstName { get; set; }
        public string? AuthorMiddleName { get; set; }
        [Required]
        public string AuthorLastName { get; set; }
        [Required]
        public string Publisher { get; set; }
        [Required]
        //regex to make sure the ISBN is entered correctly
        [RegularExpression(@"\d{3}-\d{10}", ErrorMessage = "ISBN incorrect, please use this format: 000-0000000000")]
        public string Isbn { get; set; }
        [Required]
        public string Classification { get; set; }
        [Required]
        public string Category { get; set; }
        [Required]
        public double Price { get; set; }
        [Required]
        public int Pages { get; set; }

    }
}
