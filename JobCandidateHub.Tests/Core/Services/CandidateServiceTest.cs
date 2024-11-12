using JobCandidateHub.Application.DTOs.Candidates;
using JobCandidateHub.Application.Interfaces.Repository;
using JobCandidateHub.Domain.Entities.API;
using JobCandidateHub.Infrastructure.Implementation.Services;
using Moq;

namespace JobCandidateHub.Tests.Core.Services
{
    public class CandidateServiceTest
    {
        [Fact]
        public async Task UpsertCandidate_When_Candidate_Exists_Should_Update_And_Return_Response()
        {
            var candidateRequest = new CandidatesRequestDTO
            {
                Email = "shyam@example.com",
                FirstName = "Shyam",
                LastName = "Lal",
                PhoneNumber = "123456789",
                BestCallTime = "9:00 AM - 11:00 AM",
                LinkedInUrl = "https://linkedin.com/in/shyamey",
                GitHubUrl = "https://github.com/shyamey",
                Comments = "Experienced developer"
            };

            var existingCandidate = new Candidates
            {
                Email = "shyam@example.com",
                FirstName = "Shyam",
                LastName = "Lal",
                PhoneNumber = "123456789",
                BestCallTime = "8:00 AM - 10:00 AM",
                LinkedInUrl = "https://linkedin.com/in/shyamey",
                GitHubUrl = "https://github.com/shyamey",
                Comments = "Senior developer"
            };

            var genericRepositoryMock = new Mock<IGenericRepository>();

            genericRepositoryMock.Setup(repo => repo.GetFirstOrDefaultAsync<Candidates>(It.IsAny<System.Linq.Expressions.Expression<System.Func<Candidates, bool>>>()))
                .ReturnsAsync(existingCandidate);

            genericRepositoryMock.Setup(repo => repo.Update(It.IsAny<Candidates>())).Returns(Task.CompletedTask);

            var candidateService = new CandidateServices(genericRepositoryMock.Object);

            var result = await candidateService.UpsertCandidate(candidateRequest);

            Assert.NotNull(result);
            Assert.Equal(candidateRequest.FirstName, result.FirstName);
            Assert.Equal(candidateRequest.LastName, result.LastName);
            Assert.Equal(candidateRequest.PhoneNumber, result.PhoneNumber);
            Assert.Equal(candidateRequest.BestCallTime, result.BestCallTime);
            Assert.Equal(candidateRequest.LinkedInUrl, result.LinkedInUrl);
            Assert.Equal(candidateRequest.GitHubUrl, result.GitHubUrl);
            Assert.Equal(candidateRequest.Comments, result.Comments);

            genericRepositoryMock.Verify(repo => repo.Update(It.IsAny<Candidates>()), Times.Once);
        }

        [Fact]
        public async Task UpsertCandidate_When_Exception_Thrown_Should_Return_Null()
        {
            var candidateRequest = new CandidatesRequestDTO
            {
                Email = "shyam@example.com",
                FirstName = "Shyam",
                LastName = "Lal",
                PhoneNumber = "123456789",
                BestCallTime = "9:00 AM - 11:00 AM",
                LinkedInUrl = "https://linkedin.com/in/shyamey",
                GitHubUrl = "https://github.com/shyamey",
                Comments = "Experienced developer"
            };

            var genericRepositoryMock = new Mock<IGenericRepository>();

            genericRepositoryMock.Setup(repo => repo.GetFirstOrDefaultAsync<Candidates>(It.IsAny<System.Linq.Expressions.Expression<System.Func<Candidates, bool>>>()))
                .ThrowsAsync(new Exception("Database error"));

            var candidateService = new CandidateServices(genericRepositoryMock.Object);

            var result = await candidateService.UpsertCandidate(candidateRequest);

            Assert.Null(result);
            genericRepositoryMock.Verify(repo => repo.GetFirstOrDefaultAsync<Candidates>(It.IsAny<System.Linq.Expressions.Expression<System.Func<Candidates, bool>>>()), Times.Once);
        }
    }
}
