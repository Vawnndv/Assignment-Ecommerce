﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shared_ViewModels.Product
{
    public class UpdateProductImageVmDto
    {
        public int Id { get; set; }
        [Required]
        public string ImageUrl { get; set; }
    }
}
