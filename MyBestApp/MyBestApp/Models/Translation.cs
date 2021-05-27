using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace MyBestApp.Models
{
    public class Translation
    {
        [Key]
        public int Id { get; set; }
        [Required]
        [StringLength(200)]
        public string Name { get; set; }
        [Required]
        [StringLength(200)]
        public string Value { get; set; }
        [Required]
        [StringLength(20)]
        public string Culture { get; set; }
    }
}
