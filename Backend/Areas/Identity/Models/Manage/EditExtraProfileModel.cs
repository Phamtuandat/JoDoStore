using System.ComponentModel.DataAnnotations;

namespace App.Areas.Identity.Models.ManageViewModel
{
      public class EditExtraProfileModel
      {
            public string UserName { get; set; }
            public string UserEmail { get; set; }
            [Display(Name = "Phone")]
            public string PhoneNumber { get; set; }
            [Display(Name = "Home Adress")]
            [StringLength(400)]
            public string HomeAdress { get; set; }
            public DateOnly? BirthDate { get; set; }
      }
}