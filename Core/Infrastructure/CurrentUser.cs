using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain.Interfaces;

namespace Core.Infrastructure
{
    public  class CurrentUser : ICurrentUser
    {
        public long UserId { get; set; }
        public long CurrentClientId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }

        public CurrentUser(long userId, long currentId, string firstName, string lastName)
        {
            UserId = userId; 
            CurrentClientId = currentId;
            FirstName = firstName;
            LastName = lastName;
        }

    }
}
