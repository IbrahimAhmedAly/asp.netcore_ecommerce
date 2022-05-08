using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace  book.Models
{
    public class TbSlider
    {
        [Key]
        public int SliderId { get; set; }
        [MaxLength(200)]
        public String Title { get; set; }
        [MaxLength(500)]
        public String Description { get; set; }
        [Required]
        [MaxLength(200)]
        public string ImageName { get; set; }
    }
}
