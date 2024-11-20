using System.ComponentModel.DataAnnotations;

namespace ApplicationCore.Models
{
    public class UserRegisterRequestModel
    {
        [Required]
        [EmailAddress]
        [StringLength(50)]
        public string Email{get;set;}

        [Required]
        [StringLength(100)]
        [RegularExpression()]
        public string Password{get;set;}
        public string name{get;set;}
        
    }
}