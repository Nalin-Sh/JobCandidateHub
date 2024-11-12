using JobCandidateHub.Application.DTOs.Candidates;
using JobCandidateHub.Application.Interfaces.Services;
using JobCandidateHub.Controllers;
using JobCandidateHub.Domain.Entities.ViewModel;
using Microsoft.AspNetCore.Mvc;
using Moq;
using System.Net;

namespace JobCandidateHub.Tests.API.Controller
{
    public class CandidateControllerTest
    {
        [Fact]
        public async Task Run_UpsertCandidate_When_Success_Returns_Ok()
        {
            var model = new CandidatesRequestDTO()
            {
                FirstName = "Ram",
                LastName = "Pandey",
                Email = "ramey@example.com",
                PhoneNumber = "1234567890",
                BestCallTime = "9:00 AM - 11:00 AM",
                LinkedInUrl = "https://www.linkedin.com/in/ramey",
                GitHubUrl = "https://github.com/ramey",
                Comments = "Experienced developer"
            };

            var expectedResult = new CandidatesResponseDTO()
            {
                FirstName = model.FirstName,
                LastName = model.LastName,
                Email = model.Email,
                PhoneNumber = model.PhoneNumber,
                BestCallTime = model.BestCallTime,
                LinkedInUrl = model.LinkedInUrl,
                GitHubUrl = model.GitHubUrl,
                Comments = model.Comments
            };

            var candidateServiceMock = new Mock<ICandidateServices>();

            candidateServiceMock.Setup(m => m.UpsertCandidate(It.IsAny<CandidatesRequestDTO>())).ReturnsAsync(expectedResult);

            var candidateController = new CandidateController(candidateServiceMock.Object);

            var expectedResponse = new Response<CandidatesResponseDTO>
            {
                StatusMessage = "Candidate Added/Updated Successfully",
                StatusCode = HttpStatusCode.OK,
                ResponseData = expectedResult,
                IsSuccess = true
            };

            var response = await candidateController.UpsertCandidate(model);

            var okResult = Assert.IsType<OkObjectResult>(response);
            var responseObject = Assert.IsType<Response<CandidatesResponseDTO>>(okResult.Value);
            Assert.True(responseObject.IsSuccess);
            Assert.Equal("Candidate Added/Updated Successfully", responseObject.StatusMessage);
            Assert.Equal(HttpStatusCode.OK, responseObject.StatusCode);
            Assert.Equal(expectedResult, responseObject.ResponseData);
        }


        [Fact]
        public async Task Run_UpsertCandidate_When_InvalidData_Returns_BadRequest()
        {
            // Arrange
            var candidateRequest = new CandidatesRequestDTO
            {
                Email = "ramey@example.com",
                FirstName = "Ram",
                LastName = "Pandey",
                PhoneNumber = "123456789",
                BestCallTime = "9:00 AM - 11:00 AM",
                LinkedInUrl = "https://linkedin.com/in/ramey",
                GitHubUrl = "https://github.com/ramey",
                Comments = "Experienced developer"
            };

            var candidateServiceMock = new Mock<ICandidateServices>();

            // Simulate a failure or invalid data response
            candidateServiceMock.Setup(service => service.UpsertCandidate(It.IsAny<CandidatesRequestDTO>()))
                                .ReturnsAsync((CandidatesResponseDTO)null); // Simulate invalid data (null result)

            var candidateController = new CandidateController(candidateServiceMock.Object);

            // Act
            var result = await candidateController.UpsertCandidate(candidateRequest);

            // Assert
            var badRequestResult = Assert.IsType<BadRequestObjectResult>(result); // Check if the result is a BadRequest (HTTP 400)
            var response = Assert.IsType<Response<CandidatesResponseDTO>>(badRequestResult.Value); // Assert the response type

            // Verify response properties
            Assert.False(response.IsSuccess);
            Assert.Equal("Invalid data provided.", response.StatusMessage);
            Assert.Null(response.ResponseData);
        }
    }
}
