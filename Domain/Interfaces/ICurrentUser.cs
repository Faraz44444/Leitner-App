namespace Domain.Interfaces
{
    public interface ICurrentUser
    {
        public long UserId { get;  }
        public string FirstName { get; }
        public string LastName { get; }
    }
}
