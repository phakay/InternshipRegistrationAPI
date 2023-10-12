using AutoMapper;
using InternshipRegistrationAPI.Core.Dtos;
using InternshipRegistrationAPI.Core.Exceptions;
using InternshipRegistrationAPI.Data.Contracts;
using Models = InternshipRegistrationAPI.Core.Models;
using System.Web.Http;


namespace InternshipRegistrationAPI.App.Controllers
{
    
    [Route("api/{controller}")]
    public class ProgramController : ApiController
    {
        private IProgramRepository _programRepository;
        private IMapper _mapper;

        public ProgramController(IProgramRepository programRepository, IMapper mapper)
        {
            _programRepository = programRepository;
            _mapper = mapper;
        }
        [HttpGet]
        [Route("{id}/{partitionKey}")]
        public async Task<IHttpActionResult> GetProgram(string id, string partitionKey)
        {
            try
            {
                Models.Program response = await _programRepository.GetProgramAsync(id, partitionKey);
                var responseDto = _mapper.Map<ProgramDto>(response);
                
                return Ok<ProgramDto>(responseDto);
            }
            catch (ItemNotFoundException ex)
            {
                return NotFound();
            }
            catch (Exception ex)
            {
                return InternalServerError(ex);
            }
        }
    }
}
