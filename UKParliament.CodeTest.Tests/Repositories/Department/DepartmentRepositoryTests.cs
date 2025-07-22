using UKParliament.CodeTest.Data;
using UKParliament.CodeTest.Data.Repositories.Department;
using Xunit;

namespace UKParliament.CodeTest.Tests.Repositories.Department;

public class DepartmentRepositoryTests : BaseRepository
{
    private static Data.Department CreateDepartment(int id = 1)
    {
        return new Data.Department() { Id = id, Name = "Department One" };
    }

    [Fact]
    public void DepartmentRepository_GetAll_ReturnsAllDepartments()
    {
        var options = CreateNewContextOptions();

        using var context = new PersonManagerContext(options);

        context.Departments.AddRange(
            CreateDepartment(1),
            CreateDepartment(2),
            CreateDepartment(3)
        );
        context.SaveChanges();

        var repo = new DepartmentRepository(context);
        var result = repo.GetAll();

        Assert.Equal(3, result.Count());
    }
}