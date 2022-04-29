using FieldAgent.Core.Entities;
namespace FieldAgent.API.Models
{
    public class AgentModel
    {
        public int AgentId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public DateTime DateOfBirth { get; set; }
        public decimal Height { get; set; }
        public List<Mission>? Missions { get; set; }
    }
}
