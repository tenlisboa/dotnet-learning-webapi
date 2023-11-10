using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace LearnApi.Domain.Models
{
    public class UserDTO
    {
        public int Id { get; set; }

        public string Username { get; set; }
    }
}