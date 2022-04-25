alter PROCEDURE [ClearanceAudit](
    @securityClearanceId as int,
    @agencyId as int
)
AS
BEGIN

select 
sc.SecurityClearanceId,
ag.AgencyId,
aa.BadgeId,
a.LastName + ' ' + a.FirstName as NameLastFirst,
a.DateOfBirth,
aa.ActivationDate,
aa.DeactivationDate
from AgencyAgent aa
    join Agency ag on ag.AgencyId = aa.AgencyId
    join Agent a on aa.AgentId = a.AgentId
    join SecurityClearance sc on aa.SecurityClearanceId = sc.SecurityClearanceId
where sc.SecurityClearanceId = @securityClearanceId
    and ag.AgencyId = @agencyId

END