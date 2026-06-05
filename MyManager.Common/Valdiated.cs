using System.Diagnostics;
using System.Runtime.CompilerServices;

namespace MyManager.Common;

public abstract record Validated<TE, TA>
    where TE : notnull
    where TA : notnull
{
    public bool IsValid => this is Valid<TE, TA>;
    public bool IsInvalid => this is Invalid<TE, TA>;

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public Validated<TE, TC> Apply<TB, TC>(Validated<TE, TB> that, Func<TA, TB, TC> map)
        where TB : notnull
        where TC : notnull => (this, that) switch
        {
            (Valid<TE, TA>(var a), Valid<TE, TB>(var b)) => map(a, b),
            (Invalid<TE, TA>(var e1), Invalid<TE, TB>(var e2)) => e1.Concat(e2),
            (Invalid<TE, TA>(var e1), _) => e1,
            (_, Invalid<TE, TB>(var e2)) => e2,
            _ => throw new UnreachableException(),
        };

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public Validated<TE, TC> Apply<TB, TC>(Validated<TE, TB> that, Func<TA, TB, TC> map,
                                           Func<NonEmptyList<TE>, NonEmptyList<TE>, NonEmptyList<TE>> combineErrors)
        where TB : notnull
        where TC : notnull => (this, that) switch
        {
            (Valid<TE, TA>(var a), Valid<TE, TB>(var b)) => map(a, b),
            (Invalid<TE, TA>(var e1), Invalid<TE, TB>(var e2)) => combineErrors(e1, e2),
            (Invalid<TE, TA>(var e1), _) => e1,
            (_, Invalid<TE, TB>(var e2)) => e2,
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
        Valid<TE, TA>(var validValue) => flatMap(validValue),
        Invalid<TE, TA>(var invalidValue) => invalidValue,
        _ => throw new UnreachableException(),
    };

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public T Fold<T>(Func<NonEmptyList<TE>, T> ifInvalid, Func<TA, T> ifValid) => this switch
    {
        Valid<TE, TA>(var validValue) => ifValid(validValue),
        Invalid<TE, TA>(var invalidValue) => ifInvalid(invalidValue),
        _ => throw new UnreachableException(),
    };

    public Task<T> FoldAsync<T>(Func<NonEmptyList<TE>, Task<T>> ifInvalid, Func<TA, Task<T>> ifValid) => this switch
    {
        Valid<TE, TA>(var validValue) => ifValid(validValue),
        Invalid<TE, TA>(var invalidValue) => ifInvalid(invalidValue),
        _ => throw new UnreachableException(),
    };

    public Task<T> FoldAsync<T>(Func<NonEmptyList<TE>, T> ifInvalid, Func<TA, Task<T>> ifValid) => this switch
    {
        Valid<TE, TA>(var validValue) => ifValid(validValue),
        Invalid<TE, TA>(var invalidValue) => Task.FromResult(ifInvalid(invalidValue)),
        _ => throw new UnreachableException(),
    };

    public Task<T> FoldAsync<T>(Func<NonEmptyList<TE>, Task<T>> ifInvalid, Func<TA, T> ifValid) => this switch
    {
        Valid<TE, TA>(var validValue) => Task.FromResult(ifValid(validValue)),
        Invalid<TE, TA>(var invalidValue) => ifInvalid(invalidValue),
        _ => throw new UnreachableException(),
    };

    public Task<T> FoldAsync<T>(Func<NonEmptyList<TE>, T> ifInvalid, Func<TA, T> ifValid) => this switch
    {
        Valid<TE, TA>(var validValue) => Task.FromResult(ifValid(validValue)),
        Invalid<TE, TA>(var invalidValue) => Task.FromResult(ifInvalid(invalidValue)),
        _ => throw new UnreachableException(),
    };

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public void ForEach(Action<TA> action)
    {
        if (this is Valid<TE, TA>(var validValue)) action(validValue);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public Validated<TE, TB> Map<TB>(Func<TA, TB> map) where TB : notnull => this switch
    {
        Valid<TE, TA>(var validValue) => map(validValue),
        Invalid<TE, TA>(var invalidValue) => invalidValue,
        _ => throw new UnreachableException(),
    };

    public static implicit operator Validated<TE, TA>(TA value) => new Valid<TE, TA>(value);

    public static implicit operator Validated<TE, TA>(NonEmptyList<TE> errors) => new Invalid<TE, TA>(errors);

    public static implicit operator Validated<TE, TA>(TE error) => new Invalid<TE, TA>(new NonEmptyList<TE>(error));

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public Validated<TE, TA> Tap(Action<TA> fn)
    {
        if (this is Valid<TE, TA>(var value)) fn(value);

        return this;
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public Validated<TE, TA> Tap<TB>(Func<TA, TB> fn)
    {
        if (this is Valid<TE, TA>(var value)) fn(value);

        return this;
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public Validated<TE, TA> TapInvalid<TB>(Func<NonEmptyList<TE>, TB> fn)
    {
        if (this is Invalid<TE, TA>(var value)) fn(value);

        return this;
    }


    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public Validated<TE, TA> TapInvalid(Action<NonEmptyList<TE>> fn)
    {
        if (this is Invalid<TE, TA>(var value)) fn(value);

        return this;
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public Validated<TE, (TA, TB)> Zip<TB>(Validated<TE, TB> that) where TB : notnull => (this, that) switch
    {
        (Valid<TE, TA>(var a), Valid<TE, TB>(var b)) => (a, b),
        (Invalid<TE, TA>(var e1), Invalid<TE, TB>(var e2)) => e1.Concat(e2),
        (Invalid<TE, TA>(var e1), _) => e1,
        (_, Invalid<TE, TB>(var e2)) => e2,
        _ => throw new UnreachableException(),
    };

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public Validated<TE, TA> ZipLeft<TB>(Validated<TE, TB> that) where TB : notnull => Apply(that, (a, _) => a);

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public Validated<TE, TB> ZipRight<TB>(Validated<TE, TB> that) where TB : notnull => Apply(that, (_, b) => b);
}

public record Valid<TE, TA>(TA Value) : Validated<TE, TA>
    where TE : notnull
    where TA : notnull;

public record Invalid<TE, TA>(NonEmptyList<TE> Errors) : Validated<TE, TA>
    where TE : notnull
    where TA : notnull;
