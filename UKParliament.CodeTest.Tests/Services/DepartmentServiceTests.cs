using UKParliament.CodeTest.Data.Repositories.Department;
using NSubstitute;
using UKParliament.CodeTest.Services;
using Xunit;

namespace UKParliament.CodeTest.Tests.Services;

public class DepartmentServiceTests
{
    private readonly IDepartmentRepository _departmentRepository;
    private readonly DepartmentService _departmentService;

    public DepartmentServiceTests()
    {
        _departmentRepository = Substitute.For<IDepartmentRepository>();
        _departmentService = new DepartmentService(_departmentRepository);
    }

    [Fact]
    public void DepartmentService_GetAll_ReturnsAllDepartments()
    {
        // Arrange
        var departmentList = new List<Data.Department>
        {
            new() { Id = 1, Name = "Department One" },
            new() { Id = 2, Name = "Department Two" }
        };

        _departmentRepository.GetAll().Returns(departmentList);

        // Act
        var result = _departmentService.GetAll();

        // Assert
        Assert.Equal(2, result.Count());

        _departmentRepository.Received(1).GetAll();
    }
}