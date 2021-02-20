using System.ComponentModel.DataAnnotations;

namespace WrongMove
{
    public class Url
    {
        [Required]
        public string Destination { get; set; }
    }
}
