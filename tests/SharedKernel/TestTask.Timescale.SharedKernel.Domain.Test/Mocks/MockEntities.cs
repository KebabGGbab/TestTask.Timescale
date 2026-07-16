using TestTask.Timescale.SharedKernel.Domain.BaseModels;

namespace TestTask.Timescale.SharedKernel.Domain.Test.Mocks;

internal class MockEntity : Entity
{
    public string Name { get; }

    public int Age { get; }

    public MockEntity(int id, string name, int age)
    {
        Id = id;
        Name = name;
        Age = age;
    }
}

internal class EntityMock2 : Entity
{
    public EntityMock2(int id)
    {
        Id = id;
    }
}