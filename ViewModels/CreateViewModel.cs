﻿using Microsoft.AspNetCore.Http;
using PersonalBlog.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace PersonalBlog.ViewModels
{
    public class CreateViewModel
    {
        [Required, Display(Name ="Header Image")]
        public IFormFile BlogHeaderImage { get; set; }
        public Post Post { get; set; }
    }
}
 