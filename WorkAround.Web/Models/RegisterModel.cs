using System.ComponentModel.DataAnnotations;

namespace WorkAround.Web.Models
{
	public class RegisterModel
	{
		[Required]
		[DataType(DataType.EmailAddress)]
		public string Email { get; set; }

		[Required]
		[DataType(DataType.Password)]
		public string Password { get; set; }

		[Required(ErrorMessage = "Country is required")]
		public string Country { get; set; }

		public bool WillingToRelocate { get; set; }
	}
}