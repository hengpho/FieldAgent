create PROCEDURE [PensionList](
    @agencyId as int
)
AS
BEGIN

select 
ag.ShortName,
aa.BadgeId,
a.LastName + ' ' + a.FirstName as NameLastFirst,
a.DateOfBirth,
aa.DeactivationDate
from AgencyAgent aa
    join Agency ag on ag.AgencyId = aa.AgencyId
    join Agent a on aa.AgentId = a.AgentId
    join SecurityClearance sc on aa.SecurityClearanceId = sc.SecurityClearanceId
where sc.SecurityClearanceName = 'retired'
    and ag.AgencyId = @agencyId

END

