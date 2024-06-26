﻿using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace KitchenEquipmentManager.Domain.Models
{
    public class RegisteredEquipment : BaseDomain
    {
        [Required]
        public Guid EquipmentId { get; set; }

        [Required]
        public Guid SiteId { get; set; }

        // Navigation properties
        [ForeignKey("EquipmentId")]
        public Equipment Equipment { get; set; }

        [ForeignKey("SiteId")]
        public Site Site { get; set; }
    }
}
