using HotChocolate.Language;
using HotChocolate.Types;

public abstract class ValueObjectScalarType<TValueObject> : ScalarType<TValueObject, StringValueNode>
    where TValueObject : class
{
    public ValueObjectScalarType(string name) : base(name) { }

    protected abstract TValueObject CreateFromValue(string value);

    protected abstract string GetValue(TValueObject runtimeValue);

    protected override TValueObject ParseLiteral(StringValueNode valueSyntax)
    {
        return CreateFromValue(valueSyntax.Value);
    }

    protected override StringValueNode ParseValue(TValueObject runtimeValue)
    {
        return new StringValueNode(GetValue(runtimeValue));
    }

    public override IValueNode ParseResult(object? resultValue)
    {
        if (resultValue is TValueObject runtimeValue)
        {
            return ParseValue(runtimeValue);
        }

        throw new SerializationException($"Cannot parse result value for type '{typeof(TValueObject).Name}'.", this);
    }

    public bool TrySerialize(TValueObject runtimeValue, out object? resultValue)
    {
        resultValue = GetValue(runtimeValue);
        return true;
    }

    public bool TryDeserialize(object? resultValue, out TValueObject? runtimeValue)
    {
        if (resultValue is string stringValue)
        {
            runtimeValue = CreateFromValue(stringValue);
            return true;
        }

        runtimeValue = null;
        return false;
    }
}