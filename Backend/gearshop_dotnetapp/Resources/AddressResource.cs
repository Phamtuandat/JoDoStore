﻿using System.ComponentModel.DataAnnotations;

namespace gearshop_dotnetapp.Resources
{
    public class AddressResource
    {
        public int Id { get; set; }
        [MaxLength(50)]

        public string? City { get; set; }

        [MaxLength(50)]
        public string? State { set; get; }

        [MaxLength(100)]
        public string? StreetAddress { get; set; }
    }
}
