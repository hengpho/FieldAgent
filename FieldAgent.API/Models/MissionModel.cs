using FieldAgent.Core.Entities;
namespace FieldAgent.API.Models
{
    public class MissionModel
    {
        public int MissionId { get; set; }
        public string CodeName { get; set; }
        public string Notes { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime ProjectedEndDate { get; set; }
        public DateTime? ActualEndDate { get; set; }
        public decimal? OperationalCost { get; set; }
        public int AgencyId { get; set; }
    }
}
