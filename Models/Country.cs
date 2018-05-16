﻿using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace GlobalCityManager.Models
{
    [Table("country")]
    public partial class Country
    {
        public Country()
        {
            City = new HashSet<City>();
        }

        [Key]
        [Column(TypeName = "char(3)")]
        public string Code { get; set; }
        [Required]
        [Column(TypeName = "char(52)")]
        public string Name { get; set; }
        [Required]
        [Column(TypeName = "char(26)")]
        public string Region { get; set; }
        [StringLength(200)]
        public string NationalFlag { get; set; }

        [InverseProperty("CountryCodeNavigation")]
        public ICollection<City> City { get; set; }
    }
}
