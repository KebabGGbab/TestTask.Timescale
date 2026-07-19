namespace TestTask.Timescale.SharedKernel.Domain.BaseModels
{
    public class EntityId : ValueObject
    {
        public int Value { get; private set; }

        public EntityId()
        {
            Value = default;
        }

        public EntityId(int id)
        {
            Value = id;
        }

        protected override IEnumerable<object> GetEqualityComponents()
        {
            yield return Value;
        }
    }
}
