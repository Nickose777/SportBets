﻿using SportBet.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SportBet.Data.Contracts.Repositories
{
    public interface IUserRepository : IRepository<UserEntity>
    {
    }
}