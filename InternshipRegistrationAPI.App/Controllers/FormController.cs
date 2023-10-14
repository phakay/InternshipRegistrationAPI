using AutoMapper;
using InternshipRegistrationAPI.Core.Dtos;
using InternshipRegistrationAPI.Core.Exceptions;
using InternshipRegistrationAPI.Core.Models;
using InternshipRegistrationAPI.Data.Contracts;
using InternshipRegistrationAPI.Data.Repositories;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using System.Net;
using System.Reflection;

namespace InternshipRegistrationAPI.App.Controllers;

[Route("api/{controller}")]
[ApiController]
public class FormController : ControllerBase
{
    private IFormRepository _formRepository;
    private IMapper _mapper;

    public FormController(IFormRepository formRepository, IMapper mapper)
    {
        _formRepository = formRepository;
        _mapper = mapper;
    }

    [HttpGet("{id}/{partitionKey}")]
    public async Task<IActionResult> GetForm(string id, string partitionKey)
    {
        try
        {
            var responseData = await _formRepository.GetDocumentAsync(id, partitionKey);
            var responseDto = _mapper.Map<FormDto>(responseData);

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
    public async Task<IActionResult> GetForms()
    {
        try
        {
            var responseData = await _formRepository.GetDocumentsAsync();
            var responseDtos = new List<FormDto>();
            foreach (var data in responseData)
            {
                responseDtos.Add(_mapper.Map<FormDto>(data));
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
    public async Task<IActionResult> ReplaceForm(string id, [FromBody] FormDto dto)
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

            var data = _mapper.Map<Form>(dto);
            var responseData = await _formRepository.UpdateDocumentAsync(data);
            FormDto response = _mapper.Map<FormDto>(responseData);
            return Ok(response);
        }
        catch (Exception ex)
        {
            return StatusCode((int)HttpStatusCode.InternalServerError,
                new { error = "An error occurred", errorInfo = ex.Message });
        }

    }

    [HttpPost]
    public async Task<IActionResult> PostForm([FromBody] FormDto dto)
    {
        try
        {
            if (ModelState.ValidationState != ModelValidationState.Valid)
                return BadRequest(ModelState);

            var data = _mapper.Map<Form>(dto);
            var responseData = await _formRepository.AddDocumentAsync(data);
            FormDto response = _mapper.Map<FormDto>(responseData);
            return CreatedAtAction(nameof(GetForm), new { id = responseData.Id, partitionKey = responseData.PartitionKey }, response);
        }
        catch (Exception ex)
        {
            return StatusCode((int)HttpStatusCode.InternalServerError,
                new { error = "An error occurred", errorInfo = ex.Message });
        }
    }

}