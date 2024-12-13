using HotChocolate;
using HotChocolate.Types;
using Microsoft.EntityFrameworkCore;
using Nezam.Modular.ESS.Identity.Domain.User;
using Nezam.Modular.ESS.Infrastructure.Data;
using Payeh.SharedKernel.Domain.Repositories;

public class UserQueries
{
    private readonly AppDbContext _appDbContext;

    public UserQueries(AppDbContext appDbContext)
    {
        _appDbContext = appDbContext;
    }


    [UsePaging]
    [UseProjection]
    [UseFiltering]
    [UseSorting]
    public IQueryable<UserEntity> GetUsers() => _appDbContext.Users;
}