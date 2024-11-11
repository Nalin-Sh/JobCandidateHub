using JobCandidateHub.Application.DTOs.Candidates;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JobCandidateHub.Application.Interfaces.Services
{
    public interface ICandidateServices
    {
        Task<CandidatesResponseDTO> UpsertCandidate(CandidatesRequestDTO candidatesRequest);
    }
}
