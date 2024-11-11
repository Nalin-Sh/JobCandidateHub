using JobCandidateHub.Application.DTOs.Candidates;
using JobCandidateHub.Application.Interfaces.Services;
using JobCandidateHub.Domain.Entities.ViewModel;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace JobCandidateHub.Controllers
{
    [Route("api/Candidate")]
    [ApiController]
    public class CandidateController : ControllerBase
    {
        private readonly ICandidateServices _candidateServices;

        public CandidateController(ICandidateServices candidateServices)
        {
            _candidateServices = candidateServices;
        }

        [HttpPost("upsert-candidate")]
        public async Task<IActionResult> UpsertCandidate([FromForm] CandidatesRequestDTO requestDTO)
        {
            try
            {
                var result = await _candidateServices.UpsertCandidate(requestDTO);

                
                if (result == null)
                {
                    var response = new Response<CandidatesResponseDTO>
                    {
                        IsSuccess = false,
                        ResponseData = null,
                        StatusCode = System.Net.HttpStatusCode.BadRequest,
                        StatusMessage = "Invalid data provided."
                    };
                    return BadRequest(response);
                }

                var successResponse = new Response<CandidatesResponseDTO>
                {
                    IsSuccess = true,
                    ResponseData = result,
                    StatusCode = System.Net.HttpStatusCode.OK,
                    StatusMessage = "Candidate Added/Updated Successfully"
                };
                return Ok(successResponse);
            }
            catch (Exception ex)
            {
                var errorResponse = new Response<CandidatesResponseDTO>
                {
                    IsSuccess = false,
                    ResponseData = null,
                    StatusCode = System.Net.HttpStatusCode.InternalServerError,
                    StatusMessage = $"An error occurred: {ex.Message}"
                };
                return StatusCode((int)System.Net.HttpStatusCode.InternalServerError, errorResponse);
            }
        }

    }
}
