alter PROCEDURE [TopAgents]
AS
BEGIN

SELECT TOP (3)
    a.FirstName + ' ' + a.LastName as [Name],
    a.DateOfBirth,
    Count(m.MissionId) 'NumberOfMissions'
FROM Agent a
INNER JOIN MissionAgent ma ON a.AgentId = ma.AgentId
INNER Join Mission m ON ma.MissionId = m.MissionId
WHERE m.ActualEndDate IS NOT NULL
GROUP BY a.FirstName, a.LastName, a.DateOfBirth
ORDER BY NumberOfMissions desc;

END