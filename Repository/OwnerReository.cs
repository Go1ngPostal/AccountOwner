﻿using System;
using System.Collections.Generic;
using System.Linq;
using Contracts;
using Entities;
using Entities.ExtendedModels;
using Entities.Extensions;
using Entities.Models;

namespace Repository
{
    public class OwnerRepository : RepositoryBase<Owner>, IOwnerRepository
    {
        public OwnerRepository(RepositoryContext repositoryContext)
            :base(repositoryContext)
        {
        }

        public void CreateOwner(Owner owner)
        {
            owner.Id = Guid.NewGuid();
            Create(owner);
            Save();
        }

        public IEnumerable<Owner> GetAllOwners(){
            return FindAll()
                .OrderBy(ow=>ow.Name);
        }

        public Owner GetOwnerById(Guid id)
        {
            return FindByCondition(owner => owner.Id.Equals(id))
                .DefaultIfEmpty(new Owner())
                .FirstOrDefault();
        }

        public OwnerExtended GetOwnerWithDetails(Guid ownerId)
        {
            return new OwnerExtended(GetOwnerById(ownerId))
            {
                Accounts = RepositoryContext.Accounts.Where(a=>a.OwnerId ==ownerId)
            };
        }

        public void UpdateOwner(Owner dbOwner, Owner owner)
        {
            dbOwner.Map(owner);
            Update(dbOwner);
            Save();
        }

        public void DeleteOwner(Owner owner)
        {
            Delete(owner);
            Save();
        }
    }
}
