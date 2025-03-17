using Application.FormSubmission;
using Application.FormSubmission.Commands;
using Application.FormSubmission.Queries;
using Application.Helpers;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Presentation.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace Presentation.Controllers
{
    [ApiController]
    [Route("api/form-submissions")]
    public class FormSubmissionController(IMediator mediator) : ControllerBase
    {
        private readonly IMediator _mediator = mediator;

        [HttpPost]
        [ProducesResponseType(typeof(BaseResponseModel), StatusCodes.Status500InternalServerError)]
        [ProducesResponseType(typeof(BaseResponseModel), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(ResponseWrapper<int>), StatusCodes.Status200OK)]
        public async Task<IActionResult> Create([FromBody] Dictionary<string, object> submissionData, CancellationToken cancellationToken = default)
        {
            if (submissionData == null || submissionData.Count == 0)
            {
                return BadRequest(new BaseResponseModel(false, "Invalid request data", ["No submission data provided"], 400));
            }

            try
            {           
                var command = new CreateFormSubmissionCommand { Inputs = submissionData };
                var result = await _mediator.Send(command, cancellationToken);
                if (result.Success)
                {
                    return Ok(result.Result);
                }
                return BadRequest(result.ErrorMessage);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new BaseResponseModel(false, "Internal server error", [ex.Message], 500));
            }
        }

        [HttpGet]
        [ProducesResponseType(typeof(BaseResponseModel), StatusCodes.Status500InternalServerError)]
        [ProducesResponseType(typeof(ResponseWrapper<List<FormSubmissionDTO>>), StatusCodes.Status200OK)]
        public async Task<IActionResult> GetAll(CancellationToken cancellationToken = default)
        {
            try
            {
                var query = new GetAllFormSubmissionsQuery();
                var result = await _mediator.Send(query, cancellationToken);
                if (result.Success)
                {
                    return Ok(result.Result);
                }
                return BadRequest(result.ErrorMessage);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new BaseResponseModel(false, "Internal server error", [ex.Message], 500));
            }
        }

 
        [HttpGet("{id:int}")]
        [ProducesResponseType(typeof(BaseResponseModel), StatusCodes.Status500InternalServerError)]
        [ProducesResponseType(typeof(BaseResponseModel), StatusCodes.Status404NotFound)]
        [ProducesResponseType(typeof(ResponseWrapper<FormSubmissionDTO>), StatusCodes.Status200OK)]
        public async Task<IActionResult> GetById(int id, CancellationToken cancellationToken = default)
        {
            try
            {
                var query = new GetFormSubmissionByIdQuery { Id = id };
                var result = await _mediator.Send(query, cancellationToken);

                if (result.Success)
                {
                    return Ok(result.Result);
                }

                return BadRequest(result.ErrorMessage);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new BaseResponseModel(false, "Internal server error", [ex.Message], 500));
            }
        }

        [HttpPut("{id:int}")]
        [ProducesResponseType(typeof(BaseResponseModel), StatusCodes.Status500InternalServerError)]
        [ProducesResponseType(typeof(BaseResponseModel), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(BaseResponseModel), StatusCodes.Status404NotFound)]
        [ProducesResponseType(typeof(ResponseWrapper<bool>), StatusCodes.Status200OK)]
        public async Task<IActionResult> Update(int id, [FromBody] Dictionary<string, object> updatedData, CancellationToken cancellationToken = default)
        {
            if (updatedData == null || updatedData.Count == 0)
            {
                return BadRequest(new BaseResponseModel(false, "Invalid request data", ["No update data provided"], 400));
            }
            try
            {
                var command = new UpdateFormSubmissionCommand { Id = id, UpdatedData=updatedData };
                var result = await _mediator.Send(command, cancellationToken);

                if (result.Success)
                {
                    return Ok(result.Result);
                }

                return BadRequest(result.ErrorMessage);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new BaseResponseModel(false, "Internal server error", [ex.Message], 500));
            }
        }

        [HttpDelete("{id:int}")]
        [ProducesResponseType(typeof(BaseResponseModel), StatusCodes.Status500InternalServerError)]
        [ProducesResponseType(typeof(BaseResponseModel), StatusCodes.Status404NotFound)]
        [ProducesResponseType(typeof(ResponseWrapper<bool>), StatusCodes.Status200OK)]
        public async Task<IActionResult> Delete(int id, CancellationToken cancellationToken = default)
        {
            try
            {
                var command = new DeleteFormSubmissionCommand { Id = id };
                var result = await _mediator.Send(command, cancellationToken);

                if (result.Success)
                {
                    return Ok(result.Result);
                }

                return BadRequest(result.ErrorMessage);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new BaseResponseModel(false, "Internal server error", [ex.Message], 500));
            }
        }
    }
}
