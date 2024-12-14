using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Payeh.SharedKernel.Domain.ValueObjects;

namespace Payeh.SharedKernel.EntityFrameworkCore.Domain;

public class BusinessIdConverter<T,TKey> : ValueConverter<T, TKey> where T : BusinessId<T,TKey>, new()
{
    public BusinessIdConverter()
        : base(
            id => id.Value, // Convert GuidBusinessId<T> to string for database storage
            str => (T)BusinessId<T,TKey>.NewId(str)) // Convert string back to GuidBusinessId<T>
    {
    }
}
    
public class BusinessIdConverter<T> :BusinessIdConverter<T,Guid> where T : GuidBusinessId<T>, new()
{} 