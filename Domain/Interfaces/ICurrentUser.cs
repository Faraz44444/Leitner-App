namespace Domain.Interfaces
{
    public interface ICurrentUser
    {
        public long UserId { get;  }
        public long CurrentClientId { get; }
        public string FirstName { get; }
        public string LastName { get; }
    }
}
