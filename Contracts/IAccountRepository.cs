using System;
using System.Collections.Generic;
using Entities.Models;

namespace Contracts
{
    public interface IAccountRepository : IRepositoryBase<Account>
    {
        IEnumerable<Account> AccountByOwner(Guid ownerid);
    }
}
