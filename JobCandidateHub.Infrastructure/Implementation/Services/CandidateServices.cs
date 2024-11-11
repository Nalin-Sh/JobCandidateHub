using JobCandidateHub.Application.DTOs.Candidates;
using JobCandidateHub.Application.Interfaces.Repository;
using JobCandidateHub.Application.Interfaces.Services;
using JobCandidateHub.Domain.Entities.API;

namespace JobCandidateHub.Infrastructure.Implementation.Services
{
    public class CandidateServices : ICandidateServices
    {
        private readonly IGenericRepository _genericRepository;

        public CandidateServices(IGenericRepository genericRepository)
        {
            _genericRepository = genericRepository;
        }

        public async Task<CandidatesResponseDTO> UpsertCandidate(CandidatesRequestDTO candidatesRequest)
        {
            try
            {
                var candidate = await _genericRepository.GetFirstOrDefaultAsync<Candidates>(c => c.Email == candidatesRequest.Email);

                if (candidate != null)
                {
                    candidate.FirstName = candidatesRequest.FirstName;
                    candidate.LastName = candidatesRequest.LastName;
                    candidate.PhoneNumber = candidatesRequest.PhoneNumber;
                    candidate.BestCallTime = candidatesRequest.BestCallTime;
                    candidate.LinkedInUrl = candidatesRequest.LinkedInUrl;
                    candidate.GitHubUrl = candidatesRequest.GitHubUrl;
                    candidate.Comments = candidatesRequest.Comments;

                    await _genericRepository.Update(candidate);
                }
                else
                {
                    var newCandidate = new Candidates
                    {
                        Email = candidatesRequest.Email,
                        FirstName = candidatesRequest.FirstName,
                        LastName = candidatesRequest.LastName,
                        PhoneNumber = candidatesRequest.PhoneNumber,
                        BestCallTime = candidatesRequest.BestCallTime,
                        LinkedInUrl = candidatesRequest.LinkedInUrl,
                        GitHubUrl = candidatesRequest.GitHubUrl,
                        Comments = candidatesRequest.Comments
                    };

                    await _genericRepository.Insert(newCandidate);
                }

                var response = new CandidatesResponseDTO
                {
                    Email = candidatesRequest.Email,
                    FirstName = candidatesRequest.FirstName,
                    LastName = candidatesRequest.LastName,
                    PhoneNumber = candidatesRequest.PhoneNumber,
                    BestCallTime = candidatesRequest.BestCallTime,
                    Comments = candidatesRequest.Comments,
                    GitHubUrl = candidatesRequest.GitHubUrl,
                    LinkedInUrl = candidatesRequest.LinkedInUrl
                };

                return response;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"An error occurred while upserting the candidate: {ex.Message}");
                return null;
            }
        }

    }
}
