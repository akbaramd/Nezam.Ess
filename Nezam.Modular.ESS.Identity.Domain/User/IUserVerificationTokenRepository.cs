﻿using Bonyan.Layer.Domain.Abstractions;
using Bonyan.UserManagement.Domain.ValueObjects;

namespace Nezam.Modular.ESS.Identity.Domain.User;

public interface IUserVerificationTokenRepository : IRepository<UserVerificationTokenEntity>
{
    
}