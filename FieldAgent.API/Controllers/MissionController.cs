using FieldAgent.API.Models;
using FieldAgent.Core.Entities;
using FieldAgent.Core.Interface;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace FieldAgent.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MissionController : Controller
    {
        private readonly IMissionRepository _missionRepository;
        public MissionController(IMissionRepository missionRepository)
        {
            _missionRepository = missionRepository;
        }
        
        [HttpGet]
        [Route("/api/[controller]/{id}", Name = "GetMission")]
        public IActionResult GetMission(int id)
        {
            var mission = _missionRepository.Get(id);
            if (!mission.Success)
            {
                return BadRequest(mission.Message);
            }
            return Ok(new MissionModel()
            {
                MissionId = mission.Data.MissionId,
                CodeName = mission.Data.CodeName,
                Notes = mission.Data.Notes,
                StartDate = mission.Data.StartDate,
                ProjectedEndDate = mission.Data.ProjectedEndDate,
                ActualEndDate = mission.Data.ActualEndDate,
                OperationalCost = mission.Data.OperationalCost,
                AgencyId = mission.Data.AgencyId
            });
        }

        [HttpGet]
        [Route("/api/[controller]/agents/{id}", Name = "GetMissionsByAgent")]
        public IActionResult GetMissionsByAgent(int id)
        {
            var missions = _missionRepository.GetByAgent(id);
            if (!missions.Success)
            {
                return BadRequest(missions.Message);
            }
            return Ok(missions.Data.Select(m => new MissionModel()
            {
                MissionId = m.MissionId,
                CodeName = m.CodeName,
                Notes = m.Notes,
                StartDate = m.StartDate,
                ProjectedEndDate = m.ProjectedEndDate,
                ActualEndDate = m.ActualEndDate,
                OperationalCost = m.OperationalCost,
                AgencyId = m.AgencyId,
            }));
        }
        [HttpGet]
        [Route("/api/[controller]/agency/{id}", Name = "GetMissionsByAgency")]
        public IActionResult GetMissionsByAgency(int id)
        {
            var missions = _missionRepository.GetByAgency(id);
            if (!missions.Success)
            {
                return BadRequest(missions.Message);
            }
            return Ok(missions.Data.Select(m => new MissionModel()
            {
                MissionId = m.MissionId,
                CodeName = m.CodeName,
                Notes = m.Notes,
                StartDate = m.StartDate,
                ProjectedEndDate = m.ProjectedEndDate,
                ActualEndDate = m.ActualEndDate,
                OperationalCost = m.OperationalCost,
                AgencyId = m.AgencyId,
            }));
        }
        [HttpPost]
        public IActionResult AddMission(ViewMissionModel mission)
        {
            if (ModelState.IsValid)
            {
                Mission newMission = new Mission()
                {
                    CodeName = mission.CodeName,
                    Notes = mission.Notes,
                    StartDate = mission.StartDate,
                    ProjectedEndDate = mission.ProjectedEndDate,
                    ActualEndDate = mission.ActualEndDate,
                    OperationalCost = mission.OperationalCost,
                    AgencyId = mission.AgencyId,
                };

                var result = _missionRepository.Insert(newMission);
                if (result.Success)
                {
                    return CreatedAtRoute(nameof(GetMission), new { id = result.Data.MissionId }, result.Data);
                }
                else
                {
                    return BadRequest(result.Message);
                }
            }
            else
            {
                return BadRequest(ModelState);
            }
        }
        [HttpPut, Authorize]
        public IActionResult UpdateMission(ViewMissionModel mission)
        {
            if (ModelState.IsValid && mission.MissionId > 0)
            {
                Mission updatedMission = new Mission()
                {
                    MissionId = mission.MissionId,
                    CodeName = mission.CodeName,
                    Notes = mission.Notes,
                    StartDate = mission.StartDate,
                    ProjectedEndDate = mission.ProjectedEndDate,
                    ActualEndDate = mission.ActualEndDate,
                    OperationalCost = mission.OperationalCost,
                    AgencyId = mission.AgencyId,
                };

                var findResult = _missionRepository.Get(mission.MissionId);
                if (!findResult.Success)
                {
                    return BadRequest(findResult.Message);
                }

                var result = _missionRepository.Update(updatedMission);
                if (result.Success)
                {
                    return Ok(updatedMission);
                }
                else
                {
                    return BadRequest(result.Message);
                }
            }
            else
            {
                if (mission.MissionId < 1)
                    ModelState.AddModelError("MissionId", "Invalid Mission Id");
                return BadRequest(ModelState);
            }
        }
        [HttpDelete("{missionId}"), Authorize]
        public IActionResult DeleteMission(int missionId)
        {
            var findResult = _missionRepository.Get(missionId);
            if (!findResult.Success)
            {
                return NotFound(findResult.Message);
            }
            var result = _missionRepository.Delete(missionId);
            if (result.Success)
            {
                return Ok(findResult.Data);
            }
            else
            {
                return BadRequest(result.Message);
            }
        }
    }
}