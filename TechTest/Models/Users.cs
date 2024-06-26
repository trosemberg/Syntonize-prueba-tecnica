﻿using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace TechTest.Models
{
    public class Users : TEntity
    {
        public string FullName { get; set; }

        public string Password { get; set; }

        public string Email { get; set; }

        public string Phone { get; set; }

        public string Salt { get; set; }

        public int? RolesId { get; set; }
    }
}