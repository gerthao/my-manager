using System.Diagnostics;
using System.Runtime.CompilerServices;

namespace MyManager.Common;

public abstract record Result<T> where T : notnull
{
    internal Result()
    {
    }

    public static Result<T> Ok(T value) => new Success<T>(value);
    public static Result<T> Fail(Error error) => new Failure<T>(error);
    public static Result<T> Fail(string code, string description) => new Failure<T>(new Error(code, description));

    public bool IsSuccess => this is Success<T>;

    public bool IsFailure => this is Failure<T>;

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Result<T> Condition(bool test, Func<T> success, Func<Error> failure) => test ? success() : failure();

    public bool Contains(T element) => this switch
    {
        Success<T>(var value) => value.Equals(element),
        _                     => false,
    };

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public Result<T> Filter(Func<T, bool> predicate) => this switch
    {
        Success<T> (var value) when predicate(value) => this,
        Success<T>                                   => new Failure<T>(Error.Empty),
        Failure<T> (var error)                       => new Failure<T>(error),
        _                                            => throw new UnreachableException(),
    };

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public Result<T> FilterNot(Func<T, bool> predicate) => this switch
    {
        Success<T> (var value) when !predicate(value) => this,
        Success<T>                                    => new Failure<T>(Error.Empty),
        Failure<T> (var error)                        => new Failure<T>(error),
        _                                             => throw new UnreachableException(),
    };

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public Result<TA> FlatMap<TA>(Func<T, Result<TA>> flatmap) where TA : notnull => this switch
    {
        Success<T> (var value) => flatmap(value),
        Failure<T> (var error) => new Failure<TA>(error),
        _                      => throw new UnreachableException(),
    };

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public TA Fold<TA>(Func<Error, TA> ifFailure, Func<T, TA> ifSuccess) => this switch
    {
        Success<T> s => ifSuccess(s.Value),
        Failure<T> f => ifFailure(f.Error),
        _            => throw new UnreachableException(),
    };

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public void ForEach(Action<T> action)
    {
        if (this is Success<T> success) action(success.Value);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public void ForFailure(Action<Error> action)
    {
        if (this is Failure<T> failure) action(failure.Error);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public T GetOrElse(Func<T> or) => this switch
    {
        Success<T> s => s.Value,
        Failure<T>   => or(),
        _            => throw new UnreachableException(),
    };

    public T GetOrElse(T or) => this switch
    {
        Success<T> s => s.Value,
        Failure<T>   => or,
        _            => throw new UnreachableException(),
    };

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public T GetOrThrow(Func<Error, Exception> exceptionFactory) => this switch
    {
        Success<T> s => s.Value,
        Failure<T> f => throw exceptionFactory(f.Error),
        _            => throw new UnreachableException(),
    };

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public Result<TA> Map<TA>(Func<T, TA> mapper) where TA : notnull => this switch
    {
        Success<T> s => new Success<TA>(mapper(s.Value)),
        Failure<T> f => new Failure<TA>(f.Error),
        _            => throw new UnreachableException(),
    };

    public static implicit operator Result<T>(Error error) => new Failure<T>(error);
    public static implicit operator Result<T>(T value) => new Success<T>(value);

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public Result<T> OrElse(Func<Result<T>> or) => this switch
    {
        Success<T> s => s,
        Failure<T>   => or(),
        _            => throw new UnreachableException(),
    };

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public Result<T> OrElse(Result<T> or) => this switch
    {
        Success<T> s => s,
        Failure<T>   => or,
        _            => throw new UnreachableException(),
    };

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public Result<T> Tap<TA>(Func<T, TA> fn)
    {
        if (this is Success<T> success) fn(success.Value);

        return this;
    }


    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public Result<T> Tap(Action<T> fn)
    {
        if (this is Success<T> success) fn(success.Value);

        return this;
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public Result<T> TapFailure(Action<Error> fn)
    {
        if (this is Failure<T> failure) fn(failure.Error);

        return this;
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public Result<T> TapFailure<TA>(Func<Error, TA> fn)
    {
        if (this is Failure<T> failure) fn(failure.Error);

        return this;
    }

    public override string ToString() => this switch
    {
        Success<T>(var value) => $"Success({value})",
        Failure<T>(var error) => $"Failure({error})",
        _                     => "Unknown",
    };
}

public sealed record Success<T>(T Value) : Result<T> where T : notnull;

public sealed record Failure<T>(Error Error) : Result<T> where T : notnull;
