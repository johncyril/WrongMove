using System.ComponentModel.DataAnnotations;

namespace WrongMove.Shared
{
    public class Url
    {
        [Required]
        public string Destination { get; set; }
    }
}
