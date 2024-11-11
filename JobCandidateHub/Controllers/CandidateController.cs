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
        public async Task<IActionResult> UpsertCandidate(CandidatesRequestDTO requestDTO)
        {
            var result = await _candidateServices.UpsertCandidate(requestDTO);
            try
            {
                var response = new Response<CandidatesResponseDTO>
                {
                    IsSuccess = true,
                    ResponseData = result,
                    StatusCode = System.Net.HttpStatusCode.OK,
                    StatusMessage = "Candidate Added/Updated Successfully"
                };
                return Ok(response);
            }
            catch (Exception ex)
            {
                var response = new Response<CandidatesResponseDTO>
                {
                    IsSuccess = false,
                    ResponseData = null,
                    StatusCode = System.Net.HttpStatusCode.InternalServerError,
                    StatusMessage = $"An error occurred: {ex.Message}"
                };
                return Ok(response);
            }
        }
    }
}
