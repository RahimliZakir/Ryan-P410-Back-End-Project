﻿using RyanP410.WebUI.Models.Entities;
using System.ComponentModel.DataAnnotations;

namespace RyanP410.WebUI.Areas.Admin.Models.FormModels
{
    public class PricingCollectionFormModel
    {
        public int Id { get; set; }

        public int PricingDetailsId { get; set; }

        [Required(ErrorMessage = "Bu xana doldurulmalıdır!")]
        public bool Exists { get; set; }

        [Required(ErrorMessage = "Bu xana doldurulmalıdır!")]
        public bool New { get; set; }

        public Pricing Pricing { get; set; }

        public List<Tuple<PricingDetail, bool>> PricingDetails { get; set; }

        public List<PricingsPricingDetailsCollection> Collection { get; set; }
    }
}
