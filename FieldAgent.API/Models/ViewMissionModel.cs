using FieldAgent.Core.Entities;
using System.ComponentModel.DataAnnotations;

namespace FieldAgent.API.Models
{
    public class ViewMissionModel
    {
        public int MissionId { get; set; }

        [Required(ErrorMessage = "Code Name is required")]
        [StringLength(50, ErrorMessage = "Code Name cannot exceed 50 characters")]
        public string CodeName { get; set; }

        public string Notes { get; set; }

        [Required(ErrorMessage = "Start Date is required")]
        public DateTime StartDate { get; set; }

        [Required(ErrorMessage = "Projected End Date is required")]        
        public DateTime ProjectedEndDate { get; set; }
        public DateTime? ActualEndDate { get; set; }
        public decimal? OperationalCost { get; set; }

        [Required(ErrorMessage = "AgencyId is required")]
        public int AgencyId { get; set; }
    }
}
