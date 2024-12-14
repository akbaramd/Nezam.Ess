using Microsoft.EntityFrameworkCore.ChangeTracking;
using System.Text.Json.Serialization;

namespace Payeh.SharedKernel.Domain.ValueObjects;

/// <summary>
/// Represents a base class for business identifiers, providing strong typing and immutability.
/// </summary>
[JsonConverter(typeof(BusinessIdJsonConverterFactory))]
public abstract class BusinessId<T, TKey> : ValueObject, IEquatable<BusinessId<T, TKey>>
    where T : BusinessId<T, TKey>, new()
{
    /// <summary>
    /// Initializes a new instance of the <see cref="BusinessId{T, TKey}" /> class with a specific value.
    /// </summary>
    protected BusinessId(TKey value)
    {
        if (value == null || value.Equals(default(TKey)))
            throw new ArgumentException($"The value of {nameof(value)} cannot be null or default.", nameof(value));

        Value = value;
    }

    public TKey Value { get; private set; }

    /// <summary>
    /// Determines whether this instance and another specified instance have the same value.
    /// </summary>
    public bool Equals(BusinessId<T, TKey>? other)
    {
        if (other is null) return false;
        if (ReferenceEquals(this, other)) return true;

        return EqualityComparer<TKey>.Default.Equals(Value, other.Value);
    }


    /// <summary>
    /// Helper method to create an instance of the derived class.
    /// </summary>
    public static T NewId(TKey value)
    {
        var instance = new T();
        instance.Value = value;
        return instance;
    }

    public override bool Equals(object? obj)
    {
        if (obj is BusinessId<T, TKey> other)
            return Equals(other);

        return false;
    }

    public override int GetHashCode()
    {
        return Value?.GetHashCode() ?? 0;
    }

    public static bool operator ==(BusinessId<T, TKey>? left, BusinessId<T, TKey>? right)
    {
        if (left is null && right is null) return true;
        if (left is null || right is null) return false;

        return left.Equals(right);
    }

    public static bool operator !=(BusinessId<T, TKey>? left, BusinessId<T, TKey>? right)
    {
        return !(left == right);
    }

    public override string ToString()
    {
        return Value?.ToString() ?? string.Empty;
    }

    protected override IEnumerable<object?> GetEqualityComponents()
    {
        yield return Value;
    }

    public static ValueComparer<T> GetValueComparer()
    {
        return new ValueComparer<T>(
            (l, r) => l == r,
            v => v.GetHashCode(),
            v => (T)v.Clone()
        );
    }
}

/// <summary>
/// Represents a specialized implementation of BusinessId with a GUID as the key type.
/// </summary>
public abstract class GuidBusinessId<T> : BusinessId<T, Guid> where T : GuidBusinessId<T>, new()
{
    public GuidBusinessId() : base(Guid.NewGuid()) { }

    public GuidBusinessId(Guid value) : base(value) { }

    public static T NewId() => NewId(Guid.NewGuid());

    public static T FromString(string value)
    {
        if (string.IsNullOrWhiteSpace(value))
            throw new ArgumentException("Business ID cannot be empty or whitespace.", nameof(value));

        if (!Guid.TryParse(value, out var parsedGuid))
            throw new ArgumentException("Invalid GUID format.", nameof(value));

        return NewId(parsedGuid);
    }
}

/// <summary>
/// Represents a specialized implementation of BusinessId with an integer as the key type.
/// </summary>
public abstract class IntBusinessId<T> : BusinessId<T, int> where T : IntBusinessId<T>, new()
{
    public IntBusinessId() : base(0) { }

    public IntBusinessId(int value) : base(value) { }




    public static T FromString(string value)
    {
        if (string.IsNullOrWhiteSpace(value))
            throw new ArgumentException("Business ID cannot be empty or whitespace.", nameof(value));

        if (!int.TryParse(value, out var parsedInt))
            throw new ArgumentException("Invalid integer format.", nameof(value));

        return NewId(parsedInt);
    }
}

/// <summary>
/// Represents a specialized implementation of BusinessId with a string as the key type.
/// </summary>
public abstract class StringBusinessId<T> : BusinessId<T, string> where T : StringBusinessId<T>, new()
{
    public StringBusinessId() : base(string.Empty) { }

    public StringBusinessId(string value) : base(value) { }



    public static T FromString(string value) => NewId(value);
}
