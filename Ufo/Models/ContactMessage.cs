﻿using System.ComponentModel.DataAnnotations;

namespace Ufo.Models
{
    public class ContactMessage
    {
        public string Name { get; set; }
        public string Email { get; set; }
        public string Message { get; set; }
    }
}