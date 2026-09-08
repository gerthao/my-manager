using System.Collections;
using System.Diagnostics;
using System.Runtime.CompilerServices;

namespace MyManager.Common;

public sealed record Valid<TA>(TA Value)
    where TA : notnull;

public sealed record Invalid<TE>(NonEmptyList<TE> Errors)
    where TE : notnull ;

[Union]
public readonly struct Validated<TE, TA>: IUnion, IEnumerable<TA>
    where TE : notnull
    where TA : notnull
{
    public object Value { get; }

    public Validated(Valid<TA> value) => Value = value;
    public Validated(Invalid<TE> value) => Value = value;

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public Validated<TE, (TA, TB)> Combine<TB>(Validated<TE, TB> that)
        where TB : notnull => (this, that) switch
    {
        (Valid<TA>(var a), Valid<TB>(var b)) => (a, b),
        (Invalid<TE>(var e1), Invalid<TE>(var e2)) => e1.Concat(e2),
        (Invalid<TE>(var e1), _) => e1,
        (_, Invalid<TE>(var e2)) => e2,
        _ => throw new UnreachableException(),
    };

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public Validated<TE, TC> Combine<TB, TC>(Validated<TE, TB> that, Func<TA, TB, TC> map)
        where TB : notnull
        where TC : notnull => (this, that) switch
    {
        (Valid<TA>(var a), Valid<TB>(var b)) => map(a, b),
        (Invalid<TE>(var e1), Invalid<TE>(var e2)) => e1.Concat(e2),
        (Invalid<TE>(var e1), _) => e1,
        (_, Invalid<TE>(var e2)) => e2,
        _ => throw new UnreachableException(),
    };

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public Validated<TE, TC> Combine<TB, TC>(Validated<TE, TB> that, Func<TA, TB, TC> map,
        Func<NonEmptyList<TE>, NonEmptyList<TE>, NonEmptyList<TE>> combineErrors)
        where TB : notnull
        where TC : notnull => (this, that) switch
    {
        (Valid<TA>(var a), Valid<TB>(var b)) => map(a, b),
        (Invalid<TE>(var e1), Invalid<TE>(var e2)) => combineErrors(e1, e2),
        (Invalid<TE>(var e1), _) => e1,
        (_, Invalid<TE>(var e2)) => e2,
        _ => throw new UnreachableException(),
    };

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Validated<TE, TA> Condition(bool test, Func<TA> valid, Func<TE> invalid) =>
        test ? valid() : NonEmptyList<TE>.One(invalid());

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Validated<TE, TA> Ensure(TA value, Func<TA, bool> predicate, Func<TE> invalid) =>
        predicate(value) ? value : NonEmptyList<TE>.One(invalid());

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public Validated<TE, TB> FlatMap<TB>(Func<TA, Validated<TE, TB>> flatMap) where TB : notnull => this switch
    {
        Valid<TA>(var validValue) => flatMap(validValue),
        Invalid<TE>(var invalidValue) => invalidValue,
        _ => throw new UnreachableException(),
    };

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public T Fold<T>(Func<NonEmptyList<TE>, T> ifInvalid, Func<TA, T> ifValid) => this switch
    {
        Valid<TA>(var validValue) => ifValid(validValue),
        Invalid<TE>(var invalidValue) => ifInvalid(invalidValue),
        _ => throw new UnreachableException(),
    };

    public Task<T> FoldAsync<T>(Func<NonEmptyList<TE>, Task<T>> ifInvalid, Func<TA, Task<T>> ifValid) => this switch
    {
        Valid<TA>(var validValue) => ifValid(validValue),
        Invalid<TE>(var invalidValue) => ifInvalid(invalidValue),
        _ => throw new UnreachableException(),
    };

    public Task<T> FoldAsync<T>(Func<NonEmptyList<TE>, T> ifInvalid, Func<TA, Task<T>> ifValid) => this switch
    {
        Valid<TA>(var validValue) => ifValid(validValue),
        Invalid<TE>(var invalidValue) => Task.FromResult(ifInvalid(invalidValue)),
        _ => throw new UnreachableException(),
    };

    public Task<T> FoldAsync<T>(Func<NonEmptyList<TE>, Task<T>> ifInvalid, Func<TA, T> ifValid) => this switch
    {
        Valid<TA>(var validValue) => Task.FromResult(ifValid(validValue)),
        Invalid<TE>(var invalidValue) => ifInvalid(invalidValue),
        _ => throw new UnreachableException(),
    };

    public Task<T> FoldAsync<T>(Func<NonEmptyList<TE>, T> ifInvalid, Func<TA, T> ifValid) => this switch
    {
        Valid<TA>(var validValue) => Task.FromResult(ifValid(validValue)),
        Invalid<TE>(var invalidValue) => Task.FromResult(ifInvalid(invalidValue)),
        _ => throw new UnreachableException(),
    };

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public void ForEach(Action<TA> action)
    {
        if (this is Valid<TA>(var validValue)) action(validValue);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public Validated<TE, TB> Map<TB>(Func<TA, TB> map) where TB : notnull => this switch
    {
        Valid<TA>(var validValue) => map(validValue),
        Invalid<TE>(var invalidValue) => invalidValue,
        _ => throw new UnreachableException(),
    };

    public static implicit operator Validated<TE, TA>(TA value) => new(new Valid<TA>(value));

    public static implicit operator Validated<TE, TA>(NonEmptyList<TE> errors) => new(new Invalid<TE>(errors));

    public static implicit operator Validated<TE, TA>(TE error) => new(new Invalid<TE>(new NonEmptyList<TE>(error)));

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public Validated<TE, TA> Tap(Action<TA> fn)
    {
        if (this is Valid<TA>(var value)) fn(value);

        return this;
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public Validated<TE, TA> Tap<TB>(Func<TA, TB> fn)
    {
        if (this is Valid<TA>(var value)) fn(value);

        return this;
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public Validated<TE, TA> TapInvalid<TB>(Func<NonEmptyList<TE>, TB> fn)
    {
        if (this is Invalid<TE>(var value)) fn(value);

        return this;
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public Validated<TE, TA> TapInvalid(Action<NonEmptyList<TE>> fn)
    {
        if (this is Invalid<TE>(var value)) fn(value);

        return this;
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public Validated<TE, (TA, TB)> Zip<TB>(Validated<TE, TB> that) where TB : notnull => (this, that) switch
    {
        (Valid<TA>(var a), Valid<TB>(var b)) => (a, b),
        (Invalid<TE>(var e1), Invalid<TE>(var e2)) => e1.Concat(e2),
        (Invalid<TE>(var e1), _) => e1,
        (_, Invalid<TE>(var e2)) => e2,
        _ => throw new UnreachableException(),
    };

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public Validated<TE, TA> ZipLeft<TB>(Validated<TE, TB> that) where TB : notnull => Combine(that, (a, _) => a);

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public Validated<TE, TB> ZipRight<TB>(Validated<TE, TB> that) where TB : notnull => Combine(that, (_, b) => b);

    public IEnumerator<TA> GetEnumerator()
    {
        if (this is Valid<TA>(var value)) yield return value;
    }

    IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();
}
