﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace KitchenEquipmentManager.Domain.Models
{
    public class User : BaseDomain
    {
        [Required]
        [StringLength(50)]
        public string UserType { get; set; }

        [Required]
        [StringLength(100)]
        public string FirstName { get; set; }

        [Required]
        [StringLength(100)]
        public string LastName { get; set; }

        [Required]
        [StringLength(50)]
        public string EmailAddress { get; set; }

        [Required]
        [StringLength(50)]
        public string UserName { get; set; }

        [Required]
        [StringLength(100)]
        public string Password { get; set; }

        // Navigation properties
        public ICollection<Site> Sites { get; set; }
        public ICollection<Equipment> Equipment { get; set; }
    }
}
