using System.ComponentModel.DataAnnotations.Schema;

namespace TipMeBackend.Controllers.DTOs
{
    public class AuthDTO
    {
        public string Email { get; set; }
        public string Password { get; set; }
    }
}