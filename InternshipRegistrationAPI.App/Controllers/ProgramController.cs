using System.Net;
using AutoMapper;
using InternshipRegistrationAPI.Core.Dtos;
using InternshipRegistrationAPI.Core.Exceptions;
using InternshipRegistrationAPI.Data.Contracts;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Models = InternshipRegistrationAPI.Core.Models;


namespace InternshipRegistrationAPI.App.Controllers
{
    [Route("api/{controller}")]
    [ApiController]
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
        public async Task<IActionResult> GetProgram(string id, string partitionKey)
        {
            try
            {
                var responseData = await _programRepository.GetDocumentAsync(id, partitionKey);
                var responseDto = _mapper.Map<ProgramDto>(responseData);

                return Ok(responseDto);
            }
            catch (ItemNotFoundException ex)
            {
                return NotFound();
            }
            catch (Exception ex)
            {
                return StatusCode((int)HttpStatusCode.InternalServerError,
                    new { error = "An error occurred", errorInfo = ex.Message });
            }
        }

        [HttpGet]
        public async Task<IActionResult> GetPrograms()
        {
            try
            {
                var responseData = await _programRepository.GetDocumentsAsync();
                var responseDtos = new List<ProgramDto>();
                foreach (var data in responseData)
                {
                    responseDtos.Add(_mapper.Map<ProgramDto>(data));
                }
                
                return Ok(responseDtos);
            }
            catch (Exception ex)
            {
                return StatusCode((int)HttpStatusCode.InternalServerError,
                    new { error = "An error occurred", errorInfo = ex.Message });
            }
        }
        
        [HttpPost]
        public async Task<IActionResult> PostProgram([FromBody] ProgramDto dto)
        {
            try
            {
                if (ModelState.ValidationState != ModelValidationState.Valid )
                    return BadRequest(ModelState);

                var data = _mapper.Map<Models.Program>(dto);
                var responseData = await _programRepository.AddDocumentAsync(data);
                ProgramDto response =  _mapper.Map<ProgramDto>(responseData);
                return CreatedAtAction(nameof(GetProgram), new {id = responseData.Id , partitionKey = responseData.PartitionKey}, response );
            }
            catch (Exception ex)
            {
                return StatusCode((int)HttpStatusCode.InternalServerError, 
                    new { error = "An error occurred", errorInfo = ex.Message });
            }
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> ReplaceProgram(string id, [FromBody] ProgramDto dto)
        {
            try
            {
                if (string.IsNullOrEmpty(dto.Id))
                {
                    ModelState.AddModelError("", "id is required for this type of request");
                    return BadRequest(ModelState);

                }
                if (dto.Id != id)
                {
                    ModelState.AddModelError("", "id in the url must be equal to id in the request body");
                    return BadRequest(ModelState);
                }
                    
                var data = _mapper.Map<Models.Program>(dto);
                var responseData = await _programRepository.UpdateProgramAsync(data);
                ProgramDto response = _mapper.Map<ProgramDto>(responseData);
                return Ok(response);
            }
            catch (ItemNotFoundException ex)
            {
                return NotFound();
            }
            catch (Exception ex)
            {
                return StatusCode((int)HttpStatusCode.InternalServerError,
                    new { error = "An error occurred", errorInfo = ex.Message });
            }
        }

    }
}
