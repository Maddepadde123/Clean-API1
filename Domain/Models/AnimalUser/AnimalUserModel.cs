using Domain.Models.Animal;
using Domain.Models.User;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Domain.Models.AnimalUser
{
    public class AnimalUserModel
    {
        [Key]
        public Guid Id { get; set; }
        public Guid AnimalId { get; set; }
        public AnimalModel Animal { get; set; }
        public Guid UserId { get; set; }
        public UserModel User { get; set; }

    }
}
