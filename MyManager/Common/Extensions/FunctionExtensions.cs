using System.Runtime.CompilerServices;

namespace MyManager.Common.Extensions;

public static class FunctionExtensions
{
    extension<T>(T value)
    {
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public TA Pipe<TA>(Func<T, TA> operation) => operation(value);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public T Tap(Action<T> action)
        {
            action(value);
            return value;
        }
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static T3 Pipe<T1, T2, T3>(this (T1, T2) tuple, Func<T1, T2, T3> operation) =>
        operation(tuple.Item1, tuple.Item2);

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static T4 Pipe<T1, T2, T3, T4>(this (T1, T2, T3) tuple, Func<T1, T2, T3, T4> operation) =>
        operation(tuple.Item1, tuple.Item2, tuple.Item3);

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static T5 Pipe<T1, T2, T3, T4, T5>(this (T1, T2, T3, T4) tuple, Func<T1, T2, T3, T4, T5> operation) =>
        operation(tuple.Item1, tuple.Item2, tuple.Item3, tuple.Item4);

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static T6 Pipe<T1, T2, T3, T4, T5, T6>(this (T1, T2, T3, T4, T5) tuple,
                                                  Func<T1, T2, T3, T4, T5, T6> operation) =>
        operation(tuple.Item1, tuple.Item2, tuple.Item3, tuple.Item4, tuple.Item5);

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static T7 Pipe<T1, T2, T3, T4, T5, T6, T7>(this (T1, T2, T3, T4, T5, T6) tuple,
                                                      Func<T1, T2, T3, T4, T5, T6, T7> operation) =>
        operation(tuple.Item1, tuple.Item2, tuple.Item3, tuple.Item4, tuple.Item5, tuple.Item6);

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static T8 Pipe<T1, T2, T3, T4, T5, T6, T7, T8>(this (T1, T2, T3, T4, T5, T6, T7) tuple,
                                                          Func<T1, T2, T3, T4, T5, T6, T7, T8> operation) =>
        operation(tuple.Item1, tuple.Item2, tuple.Item3, tuple.Item4, tuple.Item5, tuple.Item6, tuple.Item7);

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static T9 Pipe<T1, T2, T3, T4, T5, T6, T7, T8, T9>(this (T1, T2, T3, T4, T5, T6, T7, T8) tuple,
                                                              Func<T1, T2, T3, T4, T5, T6, T7, T8, T9> operation) =>
        operation(tuple.Item1, tuple.Item2, tuple.Item3, tuple.Item4, tuple.Item5, tuple.Item6, tuple.Item7,
            tuple.Item8);

    extension<T>(Func<T> fn)
    {
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public Func<TA> Pipe<TA>(Func<T, TA> other) => () =>
        {
            var result = fn();
            return other(result);
        };

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public Func<T> Tap(Action<T> action) => () =>
        {
            var result = fn();
            action(result);
            return result;
        };
    }

    extension<T1, T2>(Func<T1, T2> fn)
    {
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public Func<T1, T3> Pipe<T3>(Func<T2, T3> other) => input =>
        {
            var result = fn(input);
            return other(result);
        };

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public Func<T1, T2> Tap(Action<T2> action) => input =>
        {
            var result = fn(input);
            action(result);
            return result;
        };
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Func<T1, Func<T2, TR>> Curry<T1, T2, TR>(this Func<T1, T2, TR> fn)
        => t1 => t2 => fn(t1, t2);

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Func<T1, Func<T2, Func<T3, TR>>> Curry<T1, T2, T3, TR>(this Func<T1, T2, T3, TR> fn)
        => t1 => t2 => t3 => fn(t1, t2, t3);


    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Func<T1, Func<T2, Func<T3, Func<T4, TR>>>> Curry<T1, T2, T3, T4, TR>(this Func<T1, T2, T3, T4, TR> fn)
        => t1 => t2 => t3 => t4 => fn(t1, t2, t3, t4);

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Func<T1, Func<T2, Func<T3, Func<T4, Func<T5, TR>>>>> Curry<T1, T2, T3, T4, T5, TR>(
        this Func<T1, T2, T3, T4, T5, TR> fn)
        => t1 => t2 => t3 => t4 => t5 => fn(t1, t2, t3, t4, t5);

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Func<T1, Func<T2, Func<T3, Func<T4, Func<T5, Func<T6, TR>>>>>> Curry<T1, T2, T3, T4, T5, T6, TR>(
    this Func<T1, T2, T3, T4, T5, T6, TR> fn)
    => t1 => t2 => t3 => t4 => t5 => t6 => fn(t1, t2, t3, t4, t5, t6);

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Func<T1, Func<T2, Func<T3, Func<T4, Func<T5, Func<T6, Func<T7, TR>>>>>>> Curry<T1, T2, T3, T4, T5, T6,
    T7, TR>(this Func<T1, T2, T3, T4, T5, T6, T7, TR> fn)
    => t1 => t2 => t3 => t4 => t5 => t6 => t7 => fn(t1, t2, t3, t4, t5, t6, t7);

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Func<T1, Func<T2, Func<T3, Func<T4, Func<T5, Func<T6, Func<T7, Func<T8, TR>>>>>>>> Curry<T1, T2, T3,
            T4, T5, T6, T7, T8, TR>(this Func<T1, T2, T3, T4, T5, T6, T7, T8, TR> fn)
            => t1 => t2 => t3 => t4 => t5 => t6 => t7 => t8 => fn(t1, t2, t3, t4, t5, t6, t7, t8);

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Func<T1, Func<T2, Func<T3, Func<T4, Func<T5, Func<T6, Func<T7, Func<T8, Func<T9, TR>>>>>>>>> Curry<T1,
            T2, T3,
            T4, T5, T6, T7, T8, T9, TR>(this Func<T1, T2, T3, T4, T5, T6, T7, T8, T9, TR> fn)
            => t1 => t2 => t3 => t4 => t5 => t6 => t7 => t8 => t9 => fn(t1, t2, t3, t4, t5, t6, t7, t8, t9);

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static
            Func<T1, Func<T2, Func<T3, Func<T4, Func<T5, Func<T6, Func<T7, Func<T8, Func<T9, Func<T10, TR>>>>>>>>>> Curry<
                T1, T2, T3,
                T4, T5, T6, T7, T8, T9, T10, TR>(this Func<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, TR> fn)
            => t1 => t2 => t3 => t4 => t5 => t6 => t7 => t8 => t9 => t10 => fn(t1, t2, t3, t4, t5, t6, t7, t8, t9, t10);

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static
        Func<T1, Func<T2,
            Func<T3, Func<T4, Func<T5, Func<T6, Func<T7, Func<T8, Func<T9, Func<T10, Func<T11, TR>>>>>>>>>>> Curry<T1,
            T2, T3,
            T4, T5, T6, T7, T8, T9, T10, T11, TR>(this Func<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, TR> fn)
        => t1 => t2 => t3 => t4 =>
            t5 => t6 => t7 => t8 => t9 => t10 => t11 => fn(t1, t2, t3, t4, t5, t6, t7, t8, t9, t10, t11);

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static
        Func<T1, Func<T2, Func<T3,
            Func<T4, Func<T5, Func<T6, Func<T7, Func<T8, Func<T9, Func<T10, Func<T11, Func<T12, TR>>>>>>>>>>>> Curry<T1,
            T2, T3,
            T4, T5, T6, T7, T8, T9, T10, T11, T12, TR>(
            this Func<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, TR> fn)
        => t1 => t2 => t3 => t4 => t5 => t6 =>
            t7 => t8 => t9 => t10 => t11 => t12 => fn(t1, t2, t3, t4, t5, t6, t7, t8, t9, t10, t11, t12);

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static
        Func<T1, Func<T2, Func<T3, Func<T4,
            Func<T5, Func<T6, Func<T7, Func<T8, Func<T9, Func<T10, Func<T11, Func<T12, Func<T13, TR>>>>>>>>>>>>> Curry<
            T1, T2, T3,
            T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, TR>(
            this Func<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, TR> fn)
        => t1 => t2 => t3 => t4 => t5 => t6 => t7 => t8 =>
            t9 => t10 => t11 => t12 => t13 => fn(t1, t2, t3, t4, t5, t6, t7, t8, t9, t10, t11, t12, t13);

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static
        Func<T1, Func<T2, Func<T3, Func<T4, Func<T5,
            Func<T6, Func<T7, Func<T8, Func<T9, Func<T10, Func<T11, Func<T12, Func<T13, Func<T14, TR>>>>>>>>>>>>>>
        Curry<T1, T2, T3,
            T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, TR>(
            this Func<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, TR> fn) => t1 =>
        t2 => t3 => t4 => t5 => t6 => t7 => t8 => t9 => t10 =>
            t11 => t12 => t13 => t14 => fn(t1, t2, t3, t4, t5, t6, t7, t8, t9, t10, t11, t12, t13, t14);

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static
        Func<T1, Func<T2, Func<T3, Func<T4, Func<T5,
            Func<T6, Func<T7,
                Func<T8, Func<T9, Func<T10, Func<T11, Func<T12, Func<T13, Func<T14, Func<T15, TR>>>>>>>>>>>>>>>
        Curry<T1, T2, T3,
            T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15, TR>(
            this Func<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15, TR> fn) => t1 =>
        t2 => t3 => t4 => t5 => t6 => t7 => t8 => t9 => t10 =>
            t11 => t12 => t13 => t14 => t15 => fn(t1, t2, t3, t4, t5, t6, t7, t8, t9, t10, t11, t12, t13, t14, t15);

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static
        Func<T1, Func<T2, Func<T3, Func<T4, Func<T5,
            Func<T6, Func<T7, Func<T8,
                Func<T9, Func<T10, Func<T11, Func<T12, Func<T13, Func<T14, Func<T15, Func<T16, TR>>>>>>>>>>>>>>>>
        Curry<T1, T2, T3,
            T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15, T16, TR>(
            this Func<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15, T16, TR> fn) => t1 =>
        t2 => t3 => t4 => t5 => t6 => t7 => t8 => t9 => t10 =>
            t11 => t12 => t13 => t14 =>
                t15 => t16 => fn(t1, t2, t3, t4, t5, t6, t7, t8, t9, t10, t11, t12, t13, t14, t15, t16);
}
