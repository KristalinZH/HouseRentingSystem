namespace HouseRentingSystem.Web.ViewModels.Agent
{
    using System.ComponentModel.DataAnnotations;
    using static Common.EntityValidationConstants.Agent;
    public class BecomeAgentFormModel
	{
		[Required]
        [StringLength(PhoneNumberMaxLenght,MinimumLength =PhoneNumberMinLenght)]
        [Phone]
        [Display(Name ="Phone")]
		public string PhoneNumber { get; set; } = null!;
    }
}
