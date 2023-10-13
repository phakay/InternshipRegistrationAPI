using AutoMapper;
using InternshipRegistrationAPI.Core.Dtos;
using InternshipRegistrationAPI.Core.Exceptions;
using InternshipRegistrationAPI.Core.Models;
using InternshipRegistrationAPI.Data.Contracts;
using Microsoft.AspNetCore.Mvc;
using System.Net;

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
            var responseData = await _formRepository.GetFormAsync(id, partitionKey);
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
            var responseData = await _formRepository.GetFormsAsync();
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
            var responseData = await _formRepository.UpdateFormAsync(data);
            FormDto response = _mapper.Map<FormDto>(responseData);
            return Ok(response);
        }
        catch (Exception ex)
        {
            return StatusCode((int)HttpStatusCode.InternalServerError,
                new { error = "An error occurred", errorInfo = ex.Message });
        }
    }
}