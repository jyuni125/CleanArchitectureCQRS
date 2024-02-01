using CleanArchitecture.Application.Bases;
using CleanArchitecture.Application.Commons.Exceptions;
using CleanArchitecture.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CleanArchitecture.Application.Commons.Rules
{
    public class AuthRules : BaseRules
    {
        public Task UserShouldNotExist(User? user)
        {
            if (user is not null) throw new UserAlreadyExistException();
            return Task.CompletedTask;
        }

        public Task EmailorPasswordShouldNotBeInvalid(User? user,bool checkpassword)
        {
            if(user is null || !checkpassword) throw new EmailOrPasswordShouldNotBeInvalidException();
            return Task.CompletedTask;
        }
    }
}
