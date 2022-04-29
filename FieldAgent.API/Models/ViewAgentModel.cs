using FieldAgent.Core.Entities;
using System.ComponentModel.DataAnnotations;

namespace FieldAgent.API.Models
{
    public class ViewAgentModel
    {
        public int AgentId { get; set; }

        [Required(ErrorMessage = "First Name is required")]
        [StringLength(50, ErrorMessage = "First Name cannot be longer than 50 characters.")]
        public string FirstName { get; set; }

        [Required(ErrorMessage = "Last Name is required")]
        [StringLength(50, ErrorMessage = "Last Name cannot be longer than 50 characters.")]
        public string LastName { get; set; }

        [Required(ErrorMessage = "Date Of Birth is required")]
        public DateTime DateOfBirth { get; set; }

        [Required(ErrorMessage = "Height is required")]
        public decimal Height { get; set; }
    }
}
