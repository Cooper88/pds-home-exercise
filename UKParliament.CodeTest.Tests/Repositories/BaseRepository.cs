using Microsoft.EntityFrameworkCore;
using UKParliament.CodeTest.Data;

namespace UKParliament.CodeTest.Tests.Repositories;

public abstract class BaseRepository
{
    protected DbContextOptions<PersonManagerContext> CreateNewContextOptions()
    {
        return new DbContextOptionsBuilder<PersonManagerContext>()
            .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString()) // unique DB per test
            .Options;
    }
}