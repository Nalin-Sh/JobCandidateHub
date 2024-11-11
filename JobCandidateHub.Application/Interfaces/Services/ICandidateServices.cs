using JobCandidateHub.Application.DTOs.Candidates;

namespace JobCandidateHub.Application.Interfaces.Services
{
    public interface ICandidateServices
    {
        Task<CandidatesResponseDTO> UpsertCandidate(CandidatesRequestDTO candidatesRequest);
    }
}
