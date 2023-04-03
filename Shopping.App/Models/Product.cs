using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace Shopping.App.Models
{
    [Table("Product")]
    [Index(nameof(Name), IsUnique = true)]

    public class Product 
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [Required]
        public string Name { get; set; }

        [Required]
        public double Price { get; set; }

        public string ImageUrl { get; set; }

        public byte[] ImageData { get; set; }

        [NotMapped]
        private ImageSource _imageSource;

        [NotMapped]
        public ImageSource Image
        { 
            get => _imageSource ??= TryGetImage();
            set => _imageSource = value;
        }


        private ImageSource TryGetImage()
        {
            try
            {
                //using var ms = new MemoryStream(ImageData);

                //return ImageSource.FromStream(() => ms);
                return ImageSource.FromFile(ImageUrl);
            }
            catch 
            {
                return ImageSource.FromFile("product.png");
            }
        }
    }
}
