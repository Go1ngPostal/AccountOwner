﻿using System;
using System.Collections.Generic;
using Contracts;
using Entities;
using Entities.Models;

namespace Repository
{
    public class AccountRepository : RepositoryBase<Account>, IAccountRepository
    {
        public AccountRepository(RepositoryContext repositoryContext) 
            :base(repositoryContext)
        {
        }

        public IEnumerable<Account> AccountByOwner(Guid ownerid)
        {
            return FindByCondition(a => a.OwnerId.Equals(ownerid));
        }
    }
}
