﻿using SportBet.Services.Contracts.Factories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SportBet.Services.ResultTypes
{
    public class AuthServiceFactoryResult : AuthResult
    {
        public ServiceFactory Factory { get; set; }

        public AuthServiceFactoryResult(string message, bool success)
            : this(null, message, success) { }
        public AuthServiceFactoryResult(ServiceFactory factory, string message, bool success)
            : base(message, success)
        {
            this.Factory = factory;
        }
    }
}