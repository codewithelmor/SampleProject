using System.ComponentModel.DataAnnotations;

namespace SampleProject.DataTransferObject.BindingModels
{
    public class PersonBindingModel : BaseBindingModel
    {
        [Required(ErrorMessage = "The field First Name is required.")]
        [Display(Name = "FirstName")]
        public string FirstName { get; set; }

        [Required(ErrorMessage = "The field Last Name is required.")]
        [Display(Name = "LastName")]
        public string LastName { get; set; }

        [Required(ErrorMessage = "The field Gender is required.")]
        [Display(Name = "Gender")]
        public string Gender { get; set; }
    }
}
