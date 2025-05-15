using Online_lib.Domain.Enums;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace Online_lib.Domain.Entities.User
{
    public class UDbTable
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [Required]
        [Display(Name = "Username")]
        [StringLength(30, MinimumLength = 5, ErrorMessage = "Username can't be longer than 30 symbols and shorter than 5 symbols!")]
        public string? Username { get; set; }

        [Required]
        [Display(Name = "Password")]
        [StringLength(50, MinimumLength = 8, ErrorMessage = "Password can't be shorter than 8 symbols!")]
        public string? Password { get; set; }

        [Required]
        [Display(Name = "Email")]
        [StringLength(50)]
        public string? Email { get; set; }

        [StringLength(15)]
        public string? ContactNumber { get; set; }

        [DataType(DataType.Date)]
        public DateTime? DateOfBirth { get; set; }

        [StringLength(30)]
        public string? State { get; set; }

        [StringLength(30)]
        public string? City { get; set; }

        public int? Pincode { get; set; }

        public string? FullAddress { get; set; }

        public UserRole Role { get; set; }

        public bool IsBlocked { get; set; }

        public string LoginToken { get; set; }


    }

    public class ULoginData
    {
        public string Name { get; set; }  
        public string Password { get; set; }
        public string LoginIp { get; set; }
        public bool Status { get; set; }
        public DateTime LoginDateTime { get; set; }
    }

}
