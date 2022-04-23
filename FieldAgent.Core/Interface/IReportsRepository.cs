using FieldAgent.Core.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FieldAgent.Core.Interface
{
    public interface IReportsRepository
    {
        Response<List<TopAgentListItem>> GetTopAgents();
        Response<List<PensionListItem>> GetPensionList(int agencyId);
        Response<List<ClearanceAuditListItem>> AuditClearance(int securityClearanceId, int agencyId);
    }
}
