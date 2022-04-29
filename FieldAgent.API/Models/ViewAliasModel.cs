using System.ComponentModel.DataAnnotations;

namespace FieldAgent.API.Models
{
    public class ViewAliasModel
    {
        public int AliasId { get; set; }
        
        [Required(ErrorMessage = "Alias is required")]
        [StringLength(50, ErrorMessage = "Driver name cannot exceed 75 characters")]
        public string AliasName { get; set; }

        [Required(ErrorMessage = "Interpol ID is required")]
        public Guid? InterpolId { get; set; }
        public string Persona { get; set; }

        [Required(ErrorMessage = "Agent ID is required")]
        public int AgentId { get; set; }
    }
}
