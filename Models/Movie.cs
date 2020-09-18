using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace RazorPagesMovie.Models
{
    public class Movie
    {
        //The [quoted] strings are used for input-validation without even contacting the server
        //Validation needs only changes to this models' file, no other file needs to be changed
        public int ID { get; set; }

        [StringLength(60, MinimumLength =3), Required]
        public string Title { get; set; } //Adding Validation for specific datatypes


        /*The DataType attribute is used to specify a data type that's more specific than the database intrinsic type. DataType attributes are NOT VALIDATION attributes. In the sample application, only the date is displayed, without time.*/
        [Display(Name = "Release Date"), DataType(DataType.Date)]
        public DateTime ReleaseDate { get; set; }

        [RegularExpression(@"^[A-Z]+[a-zA-Z]*$"), Required, StringLength(30)]
        public string Genre { get; set; }

        [Range(1, 100), DataType(DataType.Currency)]
        [Column(TypeName = "decimal(18, 2)")]
        public decimal Price { get; set; }

        [RegularExpression(@"^[A-Z]+[a-zA-Z0-9""'\s-]*$"), StringLength(5), Required]
        public string Rating { get; set; }
    }
}