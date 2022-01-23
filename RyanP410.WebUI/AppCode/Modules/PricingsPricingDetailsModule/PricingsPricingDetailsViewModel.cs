﻿using RyanP410.WebUI.Areas.Admin.Models.FormModels;
using RyanP410.WebUI.Models.Entities;
using System.ComponentModel.DataAnnotations;

namespace RyanP410.WebUI.AppCode.Modules.PricingsPricingDetailsModule
{
    public class PricingsPricingDetailsViewModel
    {
        public int? Id { get; set; }

        public int PricingId { get; set; }

        public int PricingDetailsId { get; set; }

        [Required(ErrorMessage = "Bu xana doldurulmalıdır!")]
        public bool Exists { get; set; }

        [Required(ErrorMessage = "Bu xana doldurulmalıdır!")]
        public bool New { get; set; }

        public List<PricingsPricingDetailsCollection> Collections { get; set; }
    }
}
