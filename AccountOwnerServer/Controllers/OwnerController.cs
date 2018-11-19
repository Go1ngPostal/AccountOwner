using System;
using Microsoft.AspNetCore.Mvc;
using Contracts;
using Entities.Models;
using Entities.Extensions;
using System.Linq;


// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace AccountOwnerServer.Controllers
{
    [Route("api/owner")]
    public class OwnerController : Controller
    {
        private ILoggerManger _logger;
        private IRepositoryWrapper _repository;

        public OwnerController(ILoggerManger logger, IRepositoryWrapper repository)
        {
            _logger = logger;
            _repository = repository;
        }

        [HttpGet]
        public IActionResult GetAllOwners()
        {
            try
            {
                var owner = _repository.Owner.GetAllOwners();
                // var owner = new string[] { "value1", "value2" };

                _logger.LogInfo($"Returned all owners from databse.");

                return Ok(owner);
            }
            catch (Exception ex)
            {
                _logger.LogError($"Something went wrong inside GetAllOwners action: {ex.Message}");
                return StatusCode(500, "Internal server error");
            }

        }

        [HttpGet("{id}", Name = "OwnerById")]
        public IActionResult GetOwnerById(Guid id)
        {
            try
            {
                var owner = _repository.Owner.GetOwnerById(id);

                if (owner.IsEmptyObject())
                {
                    _logger.LogError($"Owner with id: {id}, hasn't been found in db.");
                    return NotFound();
                }
                else
                {
                    _logger.LogInfo($"Returned owner with id: {id}");
                    return Ok(owner);
                }
            }
            catch (Exception ex)
            {
                _logger.LogError($"Something went wrong inside GetOnwerById action {ex.Message}");
                return StatusCode(500, "Internal server error");
            }
        }

        [HttpGet("{id}/{account}")]
        public IActionResult GetOwnerWithDetails(Guid id)
        {
            try
            {
                var owner = _repository.Owner.GetOwnerWithDetails(id);

                if (owner.IsEmptyObject())
                {
                    _logger.LogError($"Owner with id: {id}, hasn't been found in db.");
                    return NotFound();
                }
                else
                {
                    _logger.LogInfo($"Returned owner with details for id: {id}");
                    return Ok(owner);
                }
            }
            catch (Exception ex)
            {
                _logger.LogError($"Something went wrong inside GetOwnerWithDetails action {ex.Message}");
                return StatusCode(500, "Internal server error");
            }
        }

        [HttpPost]
        public IActionResult CreateOwner([FromBody]Owner owner)
        {
            try
            {
                if (owner.IsObjectNull())
                {
                    _logger.LogError("Owner object sent from client is null.");
                    return BadRequest("Owner object is null");
                }

                if (!ModelState.IsValid)
                {
                    _logger.LogError("Invalid owner object sent from client.");
                    return BadRequest("Invalid model object");
                }

                _repository.Owner.CreateOwner(owner);

                return CreatedAtRoute("OwnerById", new { id = owner.Id }, owner);

            }
            catch (Exception ex)
            {
                _logger.LogError($"Something went wrong inside CreateOwner action: {ex.Message}");
                return StatusCode(500, "Internal server error");
            }
        }

        [HttpPut("{id}")]
        public IActionResult UpdateOwner(Guid id, [FromBody]Owner owner)
        {
            try
            {
                if (owner.IsObjectNull())
                {
                    _logger.LogError("Owner object sent from client is null.");
                    return BadRequest("Owner object is null");
                }

                if (!ModelState.IsValid)
                {
                    _logger.LogError("Invalid owner object sent from client.");
                    return BadRequest("Invalid model object");
                }

                var dbOwner = _repository.Owner.GetOwnerById(id);
                if (dbOwner.IsEmptyObject())
                {
                    _logger.LogError($"Owner with id: {id}, hasn't been found in db.");
                    return NotFound();
                }

                _repository.Owner.UpdateOwner(dbOwner, owner);

                return NoContent();
            }
            catch (Exception ex)
            {
                _logger.LogError($"Something went wrong inside UpdateOwner action: {ex.Message}");
                return StatusCode(500, "Internal server error");
            }
        }

        [HttpDelete("{id}")]
        public IActionResult DeleteOwner(Guid id)
        {
            try
            {
                var owner = _repository.Owner.GetOwnerById(id);

                if(owner.IsEmptyObject())
                {
                    _logger.LogError($"Owner with id: {id}, hasn't been found in db.");
                    return NotFound();
                }

                if(_repository.Account.AccountByOwner(id).Any())
                {
                    _logger.LogError($"Cannot delete owner with id: {id}. It has related accounts. Delete those accounts first");
                    return BadRequest("Cannot delete owner. It has related accounts. Delete those accounts first");
                }

                _repository.Owner.DeleteOwner(owner);

                return NoContent();
            }
            catch(Exception ex)
            {
                _logger.LogError($"Something went wrong inside DeleteOwner action: {ex.Message}");
                return StatusCode(500, "Internal server error");
            }
        }
    }
}
