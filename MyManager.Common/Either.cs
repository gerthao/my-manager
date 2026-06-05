using System.Diagnostics;
using System.Runtime.CompilerServices;

namespace MyManager.Common;

public abstract record Either<TL, TR>
    where TL : notnull
    where TR : notnull
{
    public Either<TR, TL> Swap => this switch
    {
        Left<TL, TR>(var leftValue)   => new Right<TR, TL>(leftValue),
        Right<TL, TR>(var rightValue) => new Left<TR, TL>(rightValue),
        _                             => throw new UnreachableException(),
    };

    public bool IsRight => this is Right<TL, TR>;

    public bool IsLeft => this is Left<TL, TR>;

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public Either<TL1, TR1> BiMap<TL1, TR1>(Func<TL, TL1> mapLeft, Func<TR, TR1> mapRight)
        where TL1 : notnull
        where TR1 : notnull => this switch
    {
        Left<TL, TR>(var leftValue)   => new Left<TL1, TR1>(mapLeft(leftValue)),
        Right<TL, TR>(var rightValue) => new Right<TL1, TR1>(mapRight(rightValue)),
        _                             => throw new UnreachableException(),
    };

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Either<TL, TR> Condition(bool test, Func<TR> right, Func<TL> left) =>
        test ? right() : left();

    public static Either<TL, TR> Condition(bool test, TR right, TL left) =>
        test ? right : left;

    public bool Contains(TR element) => this switch
    {
        Right<TL, TR>(var value) => value.Equals(element),
        _                        => false,
    };

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public Either<TL, TR> FilterOrElse(Func<TR, bool> predicate, Func<TL> zero) => this switch
    {
        Left<TL, TR>(var leftValue)                              => new Left<TL, TR>(leftValue),
        Right<TL, TR>(var rightValue) when predicate(rightValue) => new Right<TL, TR>(rightValue),
        Right<TL, TR>                                            => new Left<TL, TR>(zero()),
        _                                                        => throw new UnreachableException(),
    };

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public Either<TL, TR> FilterOrElse(Func<TR, bool> predicate, TL zero) => this switch
    {
        Left<TL, TR>(var leftValue)                              => new Left<TL, TR>(leftValue),
        Right<TL, TR>(var rightValue) when predicate(rightValue) => new Right<TL, TR>(rightValue),
        Right<TL, TR>                                            => new Left<TL, TR>(zero),
        _                                                        => throw new UnreachableException(),
    };

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public Either<TL, TR1> FlatMap<TR1>(Func<TR, Either<TL, TR1>> binder) where TR1 : notnull => this switch
    {
        Left<TL, TR>(var error)  => new Left<TL, TR1>(error),
        Right<TL, TR>(var value) => binder(value),
        _                        => throw new UnreachableException(),
    };

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public TA Fold<TA>(
        Func<TL, TA> ifLeft,
        Func<TR, TA> ifRight
    ) => this switch
    {
        Left<TL, TR>(var leftValue)   => ifLeft(leftValue),
        Right<TL, TR>(var rightValue) => ifRight(rightValue),
        _                             => throw new UnreachableException(),
    };

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public void ForEach(Action<TR> action)
    {
        if (this is Right<TL, TR> right) action(right.Value);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public void ForLeft(Action<TL> action)
    {
        if (this is Left<TL, TR> left) action(left.Value);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public TR GetOrElse(Func<TR> or) => this switch
    {
        Left<TL, TR>             => or(),
        Right<TL, TR>(var value) => value,
        _                        => throw new UnreachableException(),
    };

    public TR GetOrElse(TR or) => this switch
    {
        Left<TL, TR>             => or,
        Right<TL, TR>(var value) => value,
        _                        => throw new UnreachableException(),
    };

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public Either<TL, TR1> Map<TR1>(Func<TR, TR1> mapper) where TR1 : notnull => this switch
    {
        Left<TL, TR>(var leftValue)   => new Left<TL, TR1>(leftValue),
        Right<TL, TR>(var rightValue) => new Right<TL, TR1>(mapper(rightValue)),
        _                             => throw new UnreachableException(),
    };

    public static implicit operator Either<TL, TR>(TL left) => new Left<TL, TR>(left);
    public static implicit operator Either<TL, TR>(TR right) => new Right<TL, TR>(right);

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public Either<TL, TR> OrElse(Func<Either<TL, TR>> or) => this switch
    {
        Left<TL, TR>  => or(),
        Right<TL, TR> => this,
        _             => throw new UnreachableException(),
    };

    public Either<TL, TR> OrElse(Either<TL, TR> or) => this switch
    {
        Left<TL, TR>  => or,
        Right<TL, TR> => this,
        _             => throw new UnreachableException(),
    };

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public Either<TL, TR> Tap(Action<TR> fn)
    {
        if (this is Right<TL, TR>(var value)) fn(value);

        return this;
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public Either<TL, TR> Tap<TA>(Func<TR, TA> fn)
    {
        if (this is Right<TL, TR>(var value)) fn(value);

        return this;
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public Either<TL, TR> TapLeft(Action<TL> fn)
    {
        if (this is Left<TL, TR>(var error)) fn(error);

        return this;
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public Either<TL, TR> TapLeft<TA>(Func<TL, TA> fn)
    {
        if (this is Left<TL, TR>(var error)) fn(error);

        return this;
    }

    public override string ToString() => Fold(
        left => $"Left({left})",
        right => $"Right({right})"
    );
}

public sealed record Left<TL, TR>(TL Value) : Either<TL, TR>
    where TL : notnull
    where TR : notnull;

public sealed record Right<TL, TR>(TR Value) : Either<TL, TR>
    where TL : notnull
    where TR : notnull;
