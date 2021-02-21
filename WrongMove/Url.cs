using System.ComponentModel.DataAnnotations;

namespace WrongMove
{
    public class Url
    {
        [Required]
        [UrlValidation(ErrorMessage = "Please enter a full and valid url, e.g. with \"http://\" or \"https://\" at the start")]
        public string Destination { get; set; }
    }
}
