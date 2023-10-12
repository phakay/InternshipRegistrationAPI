using AutoMapper;
using InternshipRegistrationAPI.Core.Dtos;
using InternshipRegistrationAPI.Core.Exceptions;
using InternshipRegistrationAPI.Data.Contracts;
using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace InternshipRegistrationAPI.App.Controllers
{
    [ApiController]
    [Route("api/{controller}")]
    public class ProgramController : ControllerBase
    {
        private IProgramRepository _programRepository;
        private IMapper _mapper;

        public ProgramController(IProgramRepository programRepository, IMapper mapper)
        {
            _programRepository = programRepository;
            _mapper = mapper;
        }

        [HttpGet("{id}/{partitionKey}")]
        public async Task<ActionResult<ProgramDto>> GetProgram(string id, string partitionKey)
        {
            try
            {
                var responseData = await _programRepository.GetProgramAsync(id, partitionKey);
                var responseDto = _mapper.Map<ProgramDto>(responseData);
                return Ok(responseDto);
            }
            catch (ItemNotFoundException ex)
            {
                return NotFound(new { error = ex.Message });
            }
            catch (Exception ex)
            {
                return  StatusCode((int) HttpStatusCode.InternalServerError, new { error = "An error occured while processing the request" });
            }
        }
    }
}
