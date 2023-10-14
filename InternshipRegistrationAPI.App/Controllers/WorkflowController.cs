using AutoMapper;
using InternshipRegistrationAPI.Core.Dtos;
using InternshipRegistrationAPI.Core.Exceptions;
using InternshipRegistrationAPI.Core.Models;
using InternshipRegistrationAPI.Data.Contracts;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using System.Net;

namespace InternshipRegistrationAPI.App.Controllers;

[Route("api/{controller}")]
[ApiController]
public class WorkflowController : ControllerBase
{
    private IWorkflowRepository _workflowRepository;
    private IMapper _mapper;

    public WorkflowController(IWorkflowRepository WorkflowRepository, IMapper mapper)
    {
        _workflowRepository = WorkflowRepository;
        _mapper = mapper;
    }

    [HttpGet("{id}/{partitionKey}")]
    public async Task<IActionResult> GetWorkflow(string id, string partitionKey)
    {
        try
        {
            var responseData = await _workflowRepository.GetDocumentAsync(id, partitionKey);
            var responseDto = _mapper.Map<WorkflowDto>(responseData);

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

    [HttpPost]
    public async Task<IActionResult> PostWorkflow(WorkflowDto dto)
    {
        try
        {
            if (ModelState.ValidationState != ModelValidationState.Valid)
                return BadRequest(ModelState);


            if (!(dto.Stages.DistinctBy(b => b.StageOrder).Count() == dto.Stages.Count))
                return BadRequest("The 'Stages' must contain a distinct order");
            

            var data = _mapper.Map<Workflow>(dto);
            var responseData = await _workflowRepository.AddDocumentAsync(data);
            WorkflowDto response = _mapper.Map<WorkflowDto>(responseData);
            return CreatedAtAction(nameof(GetWorkflow), new { id = responseData.Id, partitionKey = responseData.PartitionKey }, response);
        }
        catch (Exception ex)
        {
            return StatusCode((int)HttpStatusCode.InternalServerError,
                new { error = "An error occurred", errorInfo = ex.Message });
        }
    }

    [HttpGet]
    public async Task<IActionResult> GetWorkflows()
    {
        try
        {
            var responseData = await _workflowRepository.GetDocumentsAsync();
            var responseDtos = new List<WorkflowDto>();
            foreach (var data in responseData)
            {
                responseDtos.Add(_mapper.Map<WorkflowDto>(data));
            }

            return Ok(responseDtos);
        }
        catch (Exception ex)
        {
            return StatusCode((int)HttpStatusCode.InternalServerError,
                new { error = "An error occurred", errorInfo = ex.Message });
        }
    }


    [HttpPut("{id}")]
    public async Task<IActionResult> ReplaceWorkflow(string id, [FromBody] WorkflowDto dto)
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

            var data = _mapper.Map<Workflow>(dto);
            var responseData = await _workflowRepository.UpdateDocumentAsync(data);
            WorkflowDto response = _mapper.Map<WorkflowDto>(responseData);
            return Ok(response);
        }
        catch (Exception ex)
        {
            return StatusCode((int)HttpStatusCode.InternalServerError,
                new { error = "An error occurred", errorInfo = ex.Message });
        }
    }
}