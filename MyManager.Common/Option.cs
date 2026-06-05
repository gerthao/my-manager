using System.Collections;
using System.Runtime.CompilerServices;

namespace MyManager.Common;

public sealed record Some<T>(T Value) : Option<T> where T : notnull;

public sealed record None<T> : Option<T> where T : notnull;

public abstract record Option<T> : IReadOnlyCollection<T> where T : notnull
{
    public bool IsEmpty => this is None<T>;

    public bool IsDefined => this is Some<T>;

    public static Option<T> Empty => new None<T>();

    public static Option<T> Of(T? value) => value is null ? new None<T>() : new Some<T>(value);

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Option<T> Condition(bool test, Func<T> provider) => test ? provider() : new None<T>();

    public static Option<T> Condition(bool test, T value) => test ? value : new None<T>();

    public bool Contains(T element) => this switch
    {
        Some<T>(var value) => value.Equals(element),
        _                  => false,
    };

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public bool Exists(Func<T, bool> predicate) => this is Some<T> some && predicate(some.Value);

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public Option<T> Filter(Func<T, bool> predicate) => this switch
    {
        Some<T>(var value) when predicate(value) => this,
        _                                        => new None<T>(),
    };

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public Option<T> FilterNot(Func<T, bool> predicate) => this switch
    {
        Some<T>(var value) when !predicate(value) => this,
        _                                         => new None<T>(),
    };

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public Option<TA> FlatMap<TA>(Func<T, Option<TA>> binder) where TA : notnull => this switch
    {
        Some<T>(var value) => binder(value),
        _                  => new None<TA>(),
    };

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public TA Fold<TA>(TA ifEmpty, Func<T, TA> operation) where TA : notnull => this switch
    {
        Some<T>(var value) => operation(value),
        _                  => ifEmpty,
    };

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public TA Fold<TA>(Func<TA> ifEmpty, Func<T, TA> operation) where TA : notnull => this switch
    {
        Some<T>(var value) => operation(value),
        _                  => ifEmpty(),
    };

    public Task<TA> FoldAsync<TA>(Func<TA> ifEmpty, Func<T, TA> operation) where TA : notnull => this switch
    {
        Some<T>(var value) => Task.FromResult(operation(value)),
        _                  => Task.FromResult(ifEmpty()),
    };

    public Task<TA> FoldAsync<TA>(Func<Task<TA>> ifEmpty, Func<T, TA> operation) where TA : notnull => this switch
    {
        Some<T>(var value) => Task.FromResult(operation(value)),
        _                  => ifEmpty(),
    };

    public Task<TA> FoldAsync<TA>(Func<TA> ifEmpty, Func<T, Task<TA>> operation) where TA : notnull => this switch
    {
        Some<T>(var value) => operation(value),
        _                  => Task.FromResult(ifEmpty()),
    };

    public Task<TA> FoldAsync<TA>(Func<Task<TA>> ifEmpty, Func<T, Task<TA>> operation) where TA : notnull => this switch
    {
        Some<T>(var value) => operation(value),
        _                  => ifEmpty(),
    };

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public void ForEach(Action<T> action)
    {
        if (this is Some<T>(var value)) action(value);
    }

    public T GetOrElse(T defaultValue) => this switch
    {
        Some<T>(var value) => value,
        _                  => defaultValue,
    };

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public TA GetOrElse<TA>(Func<TA> provider) where TA : T => this switch
    {
        Some<TA>(var value) => value,
        _                   => provider(),
    };

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public async Task<T> GetOrElseAsync(Func<Task<T>> defaultValue) => this switch
    {
        Some<T>(var value) => value,
        _                  => await defaultValue(),
    };

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public Task<T> GetOrElseAsync(Func<T> defaultValue) => this switch
    {
        Some<T>(var value) => Task.FromResult(value),
        _                  => Task.FromResult(defaultValue()),
    };

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public Option<TA> Map<TA>(Func<T, TA> mapper) where TA : notnull => this switch
    {
        Some<T>(var value) => new Some<TA>(mapper(value)),
        _                  => new None<TA>(),
    };

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public Task<Option<TA>> MapAsync<TA>(Func<T, TA> mapper) where TA : notnull => this switch
    {
        Some<T>(var value) => Task.FromResult<Option<TA>>(new Some<TA>(mapper(value))),
        _                  => Task.FromResult<Option<TA>>(new None<TA>()),
    };

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public async Task<Option<TA>> MapAsync<TA>(Func<T, Task<TA>> mapper) where TA : notnull => this switch
    {
        Some<T>(var value) => await mapper(value),
        _                  => new None<TA>(),
    };


    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public void Match(Action ifEmpty, Action<T> ifDefined)
    {
        if (this is Some<T>(var value)) ifDefined(value);
        else ifEmpty();
    }

    public static implicit operator Option<T>(T value) => new Some<T>(value);

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public Option<T> OrElse(Func<Option<T>> provider) => this switch
    {
        Some<T> => this,
        _       => provider(),
    };

    public Option<T> OrElse(Option<T> or) => this switch
    {
        Some<T> => this,
        _       => or,
    };

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public Option<T> Tap(Action<T> fn)
    {
        if (this is Some<T> some) fn(some.Value);

        return this;
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public Option<T> Tap<TA>(Func<T, TA> fn)
    {
        if (this is Some<T> some) fn(some.Value);

        return this;
    }


    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public Option<T> TapEmpty(Action fn)
    {
        if (this is None<T>) fn();

        return this;
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public Option<T> TapEmpty<TA>(Func<TA> fn)
    {
        if (this is None<T>) fn();

        return this;
    }

    public IEnumerator<T> GetEnumerator()
    {
        if (this is Some<T> some) yield return some.Value;
    }

    public override string ToString() => this switch
    {
        Some<T>(var value) => $"Some({value})",
        _                  => "None",
    };

    IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();

    public Option<(T, T1)> Zip<T1>(Option<T1> that) where T1 : notnull => (this, that) switch
    {
        (Some<T> (var t0), Some<T1>(var t1)) => (t0, t1),
        _                                    => new None<(T, T1)>(),
    };

    public Option<(T, T1, T2)> Zip<T1, T2>(Option<T1> that1, Option<T2> that2)
        where T1 : notnull
        where T2 : notnull => (this, that1, that2) switch
    {
        (Some<T> (var t0), Some<T1>(var t1), Some<T2>(var t2)) => (t0, t1, t2),
        _                                                      => new None<(T, T1, T2)>(),
    };

    public Option<(T, T1, T2, T3)> Zip<T1, T2, T3>(Option<T1> that1, Option<T2> that2, Option<T3> that3)
        where T1 : notnull
        where T2 : notnull
        where T3 : notnull => (this, that1, that2, that3) switch
    {
        (Some<T> (var t0), Some<T1>(var t1), Some<T2>(var t2), Some<T3>(var t3)) => (t0, t1, t2, t3),
        _                                                                        => new None<(T, T1, T2, T3)>(),
    };

    public Option<(T, T1, T2, T3, T4)> Zip<T1, T2, T3, T4>(Option<T1> that1, Option<T2> that2, Option<T3> that3,
                                                           Option<T4> that4)
        where T1 : notnull
        where T2 : notnull
        where T3 : notnull
        where T4 : notnull => (this, that1, that2, that3, that4) switch
    {
        (Some<T> (var t0), Some<T1>(var t1), Some<T2>(var t2), Some<T3>(var t3), Some<T4>(var t4)) => (t0, t1, t2, t3,
            t4),
        _ => new None<(T, T1, T2, T3, T4)>(),
    };

    public Option<(T, T1, T2, T3, T4, T5)> Zip<T1, T2, T3, T4, T5>(Option<T1> that1, Option<T2> that2, Option<T3> that3,
                                                                   Option<T4> that4, Option<T5> that5)
        where T1 : notnull
        where T2 : notnull
        where T3 : notnull
        where T4 : notnull
        where T5 : notnull => (this, that1, that2, that3, that4, that5) switch
    {
        (Some<T> (var t0), Some<T1>(var t1), Some<T2>(var t2), Some<T3>(var t3), Some<T4>(var t4), Some<T5>(var t5)) =>
            (t0, t1, t2, t3, t4, t5),
        _ => new None<(T, T1, T2, T3, T4, T5)>(),
    };

    public Option<(T, T1, T2, T3, T4, T5, T6)> Zip<T1, T2, T3, T4, T5, T6>(
        Option<T1> that1, Option<T2> that2, Option<T3> that3, Option<T4> that4, Option<T5> that5, Option<T6> that6)
        where T1 : notnull
        where T2 : notnull
        where T3 : notnull
        where T4 : notnull
        where T5 : notnull
        where T6 : notnull => (this, that1, that2, that3, that4, that5, that6) switch
    {
        (Some<T> (var t0), Some<T1>(var t1), Some<T2>(var t2), Some<T3>(var t3), Some<T4>(var t4), Some<T5>(var t5),
            Some<T6>(var t6)) => (t0, t1, t2, t3, t4, t5, t6),
        _ => new None<(T, T1, T2, T3, T4, T5, T6)>(),
    };

    public Option<(T, T1, T2, T3, T4, T5, T6, T7)> Zip<T1, T2, T3, T4, T5, T6, T7>(
        Option<T1> that1, Option<T2> that2, Option<T3> that3, Option<T4> that4, Option<T5> that5, Option<T6> that6,
        Option<T7> that7)
        where T1 : notnull
        where T2 : notnull
        where T3 : notnull
        where T4 : notnull
        where T5 : notnull
        where T6 : notnull
        where T7 : notnull => (this, that1, that2, that3, that4, that5, that6, that7) switch
    {
        (Some<T> (var t0), Some<T1>(var t1), Some<T2>(var t2), Some<T3>(var t3), Some<T4>(var t4), Some<T5>(var t5),
            Some<T6>(var t6), Some<T7>(var t7)) => (t0, t1, t2, t3, t4, t5, t6, t7),
        _ => new None<(T, T1, T2, T3, T4, T5, T6, T7)>(),
    };

    public int Count { get; }
}
