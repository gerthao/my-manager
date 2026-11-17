using System.Collections;
using System.Diagnostics;
using System.Runtime.CompilerServices;

namespace MyManager.Common;

public sealed record Left<TL>(TL Value)
    where TL : notnull;

public sealed record Right<TR>(TR Value) where TR : notnull;

[Union]
public readonly struct Either<TL, TR> : IUnion
    where TL : notnull
    where TR : notnull
{
    public object Value { get; }

    public Either(Left<TL> value) => Value = value;
    public Either(Right<TR> value) => Value = value;

    public static Either<TL, TR> Left(TL value) => new(new Left<TL>(value));
    public static Either<TL, TR> Right(TR value) => new(new Right<TR>(value));

    public Either<TR, TL> Swap => this switch
    {
        Left<TL>(var leftValue) => new Right<TL>(leftValue),
        Right<TR>(var rightValue) => new Left<TR>(rightValue),
    };

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public Either<TL1, TR1> BiMap<TL1, TR1>(Func<TL, TL1> mapLeft, Func<TR, TR1> mapRight)
        where TL1 : notnull
        where TR1 : notnull => this switch
    {
        Left<TL>(var leftValue) => new Left<TL1>(mapLeft(leftValue)),
        Right<TR>(var rightValue) => new Right<TR1>(mapRight(rightValue)),
    };

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Either<TL, TR> Condition(bool test, Func<TR> right, Func<TL> left) =>
        test ? right() : left();

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Either<TL, TR> Condition(bool test, TR right, TL left) =>
        test ? right : left;

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public bool ContainsLeft(TL element) => this switch
    {
        Left<TL>(var value) => value.Equals(element),
        _ => false,
    };

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public bool Contains(TR element) => this switch
    {
        Right<TR>(var value) => value.Equals(element),
        _ => false,
    };

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public bool Exists(Func<TR, bool> predicate) => this switch
    {
        Right<TR>(var value) => predicate(value),
        _ => false,
    };

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public bool ExistsLeft(Func<TL, bool> predicate) => this switch
    {
        Left<TL>(var value) => predicate(value),
        _ => false,
    };

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public Either<TL, TR> FilterOrElse(Func<TR, bool> predicate, Func<TL> zero) => this switch
    {
        Left<TL>(var leftValue) => new Left<TL>(leftValue),
        Right<TR>(var rightValue) when predicate(rightValue) => new Right<TR>(rightValue),
        Right<TR> => new Left<TL>(zero()),
    };

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public Either<TL, TR> FilterOrElse(Func<TR, bool> predicate, TL zero) => this switch
    {
        Left<TL>(var leftValue) => new Left<TL>(leftValue),
        Right<TR>(var rightValue) when predicate(rightValue) => new Right<TR>(rightValue),
        Right<TR> => new Left<TL>(zero),
    };

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public Either<TL, TR1> FlatMap<TR1>(Func<TR, Either<TL, TR1>> binder) where TR1 : notnull => this switch
    {
        Left<TL>(var error) => new Left<TL>(error),
        Right<TR>(var value) => binder(value),
    };

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public TA Fold<TA>(
        Func<TL, TA> ifLeft,
        Func<TR, TA> ifRight
    ) => this switch
    {
        Left<TL>(var leftValue) => ifLeft(leftValue),
        Right<TR>(var rightValue) => ifRight(rightValue),
    };

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public void ForEach(Action<TR> action)
    {
        if (this is Right<TR>(var value)) action(value);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public void ForLeft(Action<TL> action)
    {
        if (this is Left<TL>(var value)) action(value);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public TR GetOrElse(Func<TR> or) => this switch
    {
        Left<TL> => or(),
        Right<TR>(var value) => value,
    };

    public TR GetOrElse(TR or) => this switch
    {
        Left<TL> => or,
        Right<TR>(var value) => value,
    };

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public Either<TL, TR1> Map<TR1>(Func<TR, TR1> mapper) where TR1 : notnull => this switch
    {
        Left<TL>(var leftValue) => new Left<TL>(leftValue),
        Right<TR>(var rightValue) => new Right<TR1>(mapper(rightValue)),
    };

    public static implicit operator Either<TL, TR>(TL left) => new Left<TL>(left);
    public static implicit operator Either<TL, TR>(TR right) => new Right<TR>(right);

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public Either<TL, TR> OrElse(Func<Either<TL, TR>> or) => this switch
    {
        Left<TL> => or(),
        Right<TR> => this,
    };

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public Either<TL, TR> OrElse(Either<TL, TR> or) => this switch
    {
        Left<TL> => or,
        Right<TR> => this,
    };

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public Either<TL, TR> Tap(Action<TR> fn)
    {
        if (this is Right<TR>(var value)) fn(value);

        return this;
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public Either<TL, TR> Tap<TA>(Func<TR, TA> fn)
    {
        if (this is Right<TR>(var value)) fn(value);

        return this;
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public Either<TL, TR> TapLeft(Action<TL> fn)
    {
        if (this is Left<TL>(var error)) fn(error);

        return this;
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public Either<TL, TR> TapLeft<TA>(Func<TL, TA> fn)
    {
        if (this is Left<TL>(var error)) fn(error);

        return this;
    }

    public IEnumerator<TR> GetEnumerator()
    {
        if (this is Right<TR>(var value)) yield return value;
    }

    public override string ToString() => Fold(
        left => $"Left({left})",
        right => $"Right({right})"
    );
}
