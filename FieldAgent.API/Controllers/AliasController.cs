using FieldAgent.API.Models;
using FieldAgent.Core.Entities;
using FieldAgent.Core.Interface;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace FieldAgent.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AliasController : Controller
    {
        private readonly IAliasRepository _aliasRepository;
        public AliasController(IAliasRepository aliasRepository)
        {
            _aliasRepository = aliasRepository;
        }

        [HttpGet]
        [Route("/api/[controller]/{id}", Name = "GetAlias")]
        public IActionResult GetAlias(int id)
        {
            var result = _aliasRepository.Get(id);

            if (result.Success)
            {
                return Ok(new AliasModel()
                {
                    AliasId = result.Data.AliasId,
                    AliasName = result.Data.AliasName,
                    InterpolId = result.Data.InterpolId,
                    Persona = result.Data.Persona,
                    AgentId = result.Data.AgentId
                });
            }
            else
            {
                return BadRequest(result.Message);
            }
        }

        [HttpGet]
        [Route("/api/[controller]/{id}/agent", Name = "GetAliasByAgent")]
        public IActionResult GetAliasByAgent(int id)
        {
            var result = _aliasRepository.GetByAgent(id);
            
            if (result.Success)
            {
                return Ok(result.Data.Select(
                   a => new AliasModel()
                   {
                       AliasId = a.AliasId,
                       AliasName = a.AliasName,
                       InterpolId = a.InterpolId,
                       AgentId = a.AgentId,
                       Persona = a.Persona
                   }
                   )
                   );
            }
            else
            {
                return BadRequest(result.Message);
            }
        }

        [HttpPost]
        public IActionResult AddAlias(ViewAliasModel viewAlisModel)
        {
            if (ModelState.IsValid)
            {
                Alias Alias = new Alias
                {
                    AliasName = viewAlisModel.AliasName,
                    InterpolId = viewAlisModel.InterpolId,
                    Persona = viewAlisModel.Persona,
                    AgentId = viewAlisModel.AgentId
                };

                var result = _aliasRepository.Insert(Alias);

                if (result.Success)
                {
                    return CreatedAtRoute(nameof(GetAlias), new { id = result.Data.AliasId }, result.Data);
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
        public IActionResult EditAlias(ViewAliasModel viewAliasModel)
        {
            if (ModelState.IsValid && viewAliasModel.AliasId > 0)
            {
                Alias alias = new Alias
                {
                    AliasId = viewAliasModel.AliasId,
                    AliasName = viewAliasModel.AliasName,
                    InterpolId = viewAliasModel.InterpolId,
                    Persona = viewAliasModel.Persona,
                    AgentId = viewAliasModel.AgentId
                };

                var findResult = _aliasRepository.Get(alias.AliasId);
                if (!findResult.Success)
                {
                    return NotFound(findResult.Message);
                }

                var result = _aliasRepository.Update(alias);
                if (result.Success)
                {
                    return Ok(result.Message);
                }
                else
                {
                    return BadRequest(result.Message);
                }
            }
            else
            {
                if (viewAliasModel.AliasId < 1)
                    ModelState.AddModelError("AliasId", "Invalid Alias ID");
                return BadRequest(ModelState);
            }
        }

        [HttpDelete("{aliasId}"), Authorize]
        public IActionResult DeleteAlias(int aliasId)
        {
            var findResult = _aliasRepository.Get(aliasId);

            if (!findResult.Success)
            {            
                return NotFound(findResult.Message);
            }            

            var result = _aliasRepository.Delete(findResult.Data.AliasId);
            
            if (!result.Success)
            {
                return BadRequest(result.Message);
            }
            else
            {
                return Ok(findResult.Data);
            }
        }
    }
}