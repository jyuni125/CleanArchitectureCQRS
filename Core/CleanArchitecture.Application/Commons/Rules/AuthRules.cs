using CleanArchitecture.Application.Bases;
using CleanArchitecture.Application.Commons.Exceptions;
using CleanArchitecture.Domain.Entities;
using Microsoft.AspNetCore.Identity;
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

        public Task RefreshTokenShouldNotBeExpired(DateTime? expiryDate)
        {
            if (expiryDate <= DateTime.Now) throw new RefreshTokenShouldNotBeExpiredException();
            return Task.CompletedTask;
        }

        public Task EmailShouldNotBeInvalid(User? user)
        {
            if (user is null) throw new EmailShouldNotBeInvalidException();
            return Task.CompletedTask;
        }

        public Task EmailShouldBeVerified(bool settingsSetup, bool userEmailConfirmed)
        {
            if (settingsSetup && !userEmailConfirmed) throw new EmailConfirmationException();
            return Task.CompletedTask;
        }
    }
}
