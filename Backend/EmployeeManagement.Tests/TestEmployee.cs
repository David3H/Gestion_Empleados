using Xunit;
using AutoMapper;
using EmployeeManagement.Api.Controllers;
using EmployeeManagement.Domain;
using EmployeeManagement.Domain.Entities;
using EmployeeManagement.Domain.Repositories;
using Microsoft.AspNetCore.Mvc;
using Moq;

namespace EmployeeManagement.Tests
{
    public class EmployeesControllerTests
    {
        [Fact]
        public async Task GetAll_ReturnsOkResult()
        {
            var repoMock = new Mock<IEmployeeRepository>();
            repoMock.Setup(r => r.GetAllEmployeeAsync())
                    .ReturnsAsync(new List<Employee>());

            var storeRepoMock = new Mock<IStoreRepository>();

            var mapperMock = new Mock<IMapper>();

            var controller = new EmployeesController(repoMock.Object, storeRepoMock.Object, mapperMock.Object);

            var result = await controller.GetAll();

            Assert.IsType<OkObjectResult>(result);
        }
    }
}
