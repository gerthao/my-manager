using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;

namespace MyManager.Common.Extensions;

public static class ValidatedExtensions
{
    extension(IEnumerable<ValidationError> errors)
    {
        public ValidationProblemDetails AsValidationProblem(
            string? detail = null,
            string? title = null,
            string? instance = null,
            int? status = null,
            string? type = null,
            Dictionary<string, object?>? extensions = null
        )
        {
            var ext = extensions ?? new Dictionary<string, object?>();

            return new ValidationProblemDetails
            {
                Detail = detail,
                Instance = instance,
                Status = status,
                Title = title,
                Type = type,
                Extensions = ext,
                Errors = errors.AsDictionary(),
            };
        }

        public ProblemDetails AsProblem(
            string? detail = null,
            string? title = null,
            string? instance = null,
            int? status = null,
            string? type = null,
            Dictionary<string, object?>? extensions = null
        )
        {
            var ext = extensions ?? new Dictionary<string, object?>();
            ext["errors"] = errors.AsDictionary();

            return new ProblemDetails
            {
                Detail = detail,
                Instance = instance,
                Status = status,
                Title = title,
                Type = type,
                Extensions = ext,
            };
        }

        public Dictionary<string, string[]> AsDictionary() =>
            errors.GroupBy(e => e.Field)
                .ToDictionary(g => g.Key, g => g.Select(e => e.Message).ToArray());
    }

    extension<TE, TA>(Validated<TE, TA> validated)
        where TE : notnull
        where TA : notnull
    {
        public Either<NonEmptyList<TE>, TA> ToEither() => validated.Fold<Either<NonEmptyList<TE>, TA>>(
            i => i,
            v => v
        );

        public Option<TA> ToOption() => validated.Fold<Option<TA>>(
            _ => new None(),
            v => new Some<TA>(v)
        );

        public Validated<TE, bool> Discard() => validated.Map(_ => true);

        public static Validated<TE, bool> ValidateAll(params Validated<TE, bool>[] validations)
        {
            Validated<TE, bool> result = true;
            foreach (var v in validations)
            {
                result = result.Combine(v, (_, _) => true);
            }

            return result;
        }
    }

    extension<TE, TA>(Task<Validated<TE, TA>> validated) where TE : notnull where TA : notnull
    {
        public async Task<TB> FoldAsync<TB>(Func<NonEmptyList<TE>, TB> ifInvalid, Func<TA, TB> ifValid)
            where TB : notnull
        {
            var result = await validated.ConfigureAwait(false);
            return result.Fold(ifInvalid, ifValid);
        }

        public async Task<Validated<TE, TB>> MapAsync<TB>(Func<TA, Task<TB>> mapper) where TB : notnull
        {
            var result = await validated.ConfigureAwait(false);

            return result switch
            {
                Valid<TA> valid => await mapper(valid.Value),
                Invalid<TE> invalid => new Invalid<TE>(invalid.Errors),
                _ => throw new UnreachableException(),
            };
        }

        public async Task<Validated<TE, TB>> MapAsync<TB>(Func<TA, TB> mapper) where TB : notnull
        {
            var result = await validated.ConfigureAwait(false);

            return result switch
            {
                Valid<TA> valid => mapper(valid.Value),
                Invalid<TE> invalid => new Invalid<TE>(invalid.Errors),
                _ => throw new UnreachableException(),
            };
        }

        public async Task<TA> GetOrElseAsync(Func<TA> defaultValue)
        {
            var result = await validated.ConfigureAwait(false);

            return result switch
            {
                Valid<TA> valid => valid.Value,
                Invalid<TE> _ => defaultValue(),
                _ => throw new UnreachableException(),
            };
        }
    }

    extension<TE, T1, T2>((Validated<TE, T1>, Validated<TE, T2>) @this)
        where TE : notnull
        where T1 : notnull
        where T2 : notnull
    {
        public Validated<TE, TB> MapN<TB>(Func<T1, T2, TB> map) where TB : notnull
        {
            if (@this is { Item1: Valid<T1> v1, Item2: Valid<T2> v2 })
                return map(v1.Value, v2.Value);

            List<TE> errors = [];

            if (@this.Item1 is Invalid<TE> i1) errors.AddRange(i1.Errors);
            if (@this.Item2 is Invalid<TE> i2) errors.AddRange(i2.Errors);

            return NonEmptyList<TE>.Of(errors[0], [.. errors.Skip(1)]);
        }
    }

    extension<TE, T1, T2, T3>(
        (Validated<TE, T1>, Validated<TE, T2>, Validated<TE, T3>) @this)
        where TE : notnull
        where T1 : notnull
        where T2 : notnull
        where T3 : notnull
    {
        public Validated<TE, TB> MapN<TB>(Func<T1, T2, T3, TB> map) where TB : notnull
        {
            if (@this is
                {
                    Item1: Valid<T1> v1,
                    Item2: Valid<T2> v2,
                    Item3: Valid<T3> v3,
                })
                return map(v1.Value, v2.Value, v3.Value);


            List<TE> errors = [];

            if (@this.Item1 is Invalid<TE> i1) errors.AddRange(i1.Errors);
            if (@this.Item2 is Invalid<TE> i2) errors.AddRange(i2.Errors);
            if (@this.Item3 is Invalid<TE> i3) errors.AddRange(i3.Errors);

            return NonEmptyList<TE>.Of(errors[0], errors.Skip(1).ToArray());
        }
    }

    extension<TE, T1, T2, T3, T4>((
        Validated<TE, T1>,
        Validated<TE, T2>,
        Validated<TE, T3>,
        Validated<TE, T4>
        ) @this)
        where TE : notnull
        where T1 : notnull
        where T2 : notnull
        where T3 : notnull
        where T4 : notnull
    {
        public Validated<TE, TB> MapN<TB>(Func<T1, T2, T3, T4, TB> map) where TB : notnull
        {
            if (@this is
                {
                    Item1: Valid<T1> v1,
                    Item2: Valid<T2> v2,
                    Item3: Valid<T3> v3,
                    Item4: Valid<T4> v4,
                })
                return map(v1.Value, v2.Value, v3.Value, v4.Value);


            List<TE> errors = [];

            if (@this.Item1 is Invalid<TE> i1) errors.AddRange(i1.Errors);
            if (@this.Item2 is Invalid<TE> i2) errors.AddRange(i2.Errors);
            if (@this.Item3 is Invalid<TE> i3) errors.AddRange(i3.Errors);
            if (@this.Item4 is Invalid<TE> i4) errors.AddRange(i4.Errors);

            return NonEmptyList<TE>.Of(errors[0], [.. errors.Skip(1)]);
        }
    }

    extension<TE, T1, T2, T3, T4, T5>((
        Validated<TE, T1>,
        Validated<TE, T2>,
        Validated<TE, T3>,
        Validated<TE, T4>,
        Validated<TE, T5>
        ) @this)
        where TE : notnull
        where T1 : notnull
        where T2 : notnull
        where T3 : notnull
        where T4 : notnull
        where T5 : notnull
    {
        public Validated<TE, TB> MapN<TB>(Func<T1, T2, T3, T4, T5, TB> map) where TB : notnull
        {
            if (@this is
                {
                    Item1: Valid<T1> v1,
                    Item2: Valid<T2> v2,
                    Item3: Valid<T3> v3,
                    Item4: Valid<T4> v4,
                    Item5: Valid<T5> v5,
                })
                return map(v1.Value, v2.Value, v3.Value, v4.Value, v5.Value);


            List<TE> errors = [];

            if (@this.Item1 is Invalid<TE> i1) errors.AddRange(i1.Errors);
            if (@this.Item2 is Invalid<TE> i2) errors.AddRange(i2.Errors);
            if (@this.Item3 is Invalid<TE> i3) errors.AddRange(i3.Errors);
            if (@this.Item4 is Invalid<TE> i4) errors.AddRange(i4.Errors);
            if (@this.Item5 is Invalid<TE> i5) errors.AddRange(i5.Errors);

            return NonEmptyList<TE>.Of(errors[0], [.. errors.Skip(1)]);
        }
    }

    extension<TE, T1, T2, T3, T4, T5, T6>((
        Validated<TE, T1>,
        Validated<TE, T2>,
        Validated<TE, T3>,
        Validated<TE, T4>,
        Validated<TE, T5>,
        Validated<TE, T6>
        ) @this)
        where TE : notnull
        where T1 : notnull
        where T2 : notnull
        where T3 : notnull
        where T4 : notnull
        where T5 : notnull
        where T6 : notnull
    {
        public Validated<TE, TB> MapN<TB>(Func<T1, T2, T3, T4, T5, T6, TB> map) where TB : notnull
        {
            if (@this is
                {
                    Item1: Valid<T1> v1,
                    Item2: Valid<T2> v2,
                    Item3: Valid<T3> v3,
                    Item4: Valid<T4> v4,
                    Item5: Valid<T5> v5,
                    Item6: Valid<T6> v6,
                })
                return map(v1.Value, v2.Value, v3.Value, v4.Value, v5.Value, v6.Value);


            List<TE> errors = [];

            if (@this.Item1 is Invalid<TE> i1) errors.AddRange(i1.Errors);
            if (@this.Item2 is Invalid<TE> i2) errors.AddRange(i2.Errors);
            if (@this.Item3 is Invalid<TE> i3) errors.AddRange(i3.Errors);
            if (@this.Item4 is Invalid<TE> i4) errors.AddRange(i4.Errors);
            if (@this.Item5 is Invalid<TE> i5) errors.AddRange(i5.Errors);
            if (@this.Item6 is Invalid<TE> i6) errors.AddRange(i6.Errors);

            return NonEmptyList<TE>.Of(errors[0], [.. errors.Skip(1)]);
        }
    }

    extension<TE, T1, T2, T3, T4, T5, T6, T7>((
        Validated<TE, T1>,
        Validated<TE, T2>,
        Validated<TE, T3>,
        Validated<TE, T4>,
        Validated<TE, T5>,
        Validated<TE, T6>,
        Validated<TE, T7>
        ) @this)
        where TE : notnull
        where T1 : notnull
        where T2 : notnull
        where T3 : notnull
        where T4 : notnull
        where T5 : notnull
        where T6 : notnull
        where T7 : notnull
    {
        public Validated<TE, TB> MapN<TB>(Func<T1, T2, T3, T4, T5, T6, T7, TB> map) where TB : notnull
        {
            if (@this is
                {
                    Item1: Valid<T1> v1,
                    Item2: Valid<T2> v2,
                    Item3: Valid<T3> v3,
                    Item4: Valid<T4> v4,
                    Item5: Valid<T5> v5,
                    Item6: Valid<T6> v6,
                    Item7: Valid<T7> v7,
                })
                return map(v1.Value, v2.Value, v3.Value, v4.Value, v5.Value, v6.Value, v7.Value);


            List<TE> errors = [];

            if (@this.Item1 is Invalid<TE> i1) errors.AddRange(i1.Errors);
            if (@this.Item2 is Invalid<TE> i2) errors.AddRange(i2.Errors);
            if (@this.Item3 is Invalid<TE> i3) errors.AddRange(i3.Errors);
            if (@this.Item4 is Invalid<TE> i4) errors.AddRange(i4.Errors);
            if (@this.Item5 is Invalid<TE> i5) errors.AddRange(i5.Errors);
            if (@this.Item6 is Invalid<TE> i6) errors.AddRange(i6.Errors);
            if (@this.Item7 is Invalid<TE> i7) errors.AddRange(i7.Errors);

            return NonEmptyList<TE>.Of(errors[0], [.. errors.Skip(1)]);
        }
    }

    extension<TE, T1, T2, T3, T4, T5, T6, T7, T8>((
        Validated<TE, T1>,
        Validated<TE, T2>,
        Validated<TE, T3>,
        Validated<TE, T4>,
        Validated<TE, T5>,
        Validated<TE, T6>,
        Validated<TE, T7>,
        Validated<TE, T8>
        ) @this)
        where TE : notnull
        where T1 : notnull
        where T2 : notnull
        where T3 : notnull
        where T4 : notnull
        where T5 : notnull
        where T6 : notnull
        where T7 : notnull
        where T8 : notnull
    {
        public Validated<TE, TB> MapN<TB>(Func<T1, T2, T3, T4, T5, T6, T7, T8, TB> map) where TB : notnull
        {
            if (@this is
                {
                    Item1: Valid<T1> v1,
                    Item2: Valid<T2> v2,
                    Item3: Valid<T3> v3,
                    Item4: Valid<T4> v4,
                    Item5: Valid<T5> v5,
                    Item6: Valid<T6> v6,
                    Item7: Valid<T7> v7,
                    Item8: Valid<T8> v8,
                })
                return map(v1.Value, v2.Value, v3.Value, v4.Value, v5.Value, v6.Value, v7.Value, v8.Value);


            List<TE> errors = [];

            if (@this.Item1 is Invalid<TE> i1) errors.AddRange(i1.Errors);
            if (@this.Item2 is Invalid<TE> i2) errors.AddRange(i2.Errors);
            if (@this.Item3 is Invalid<TE> i3) errors.AddRange(i3.Errors);
            if (@this.Item4 is Invalid<TE> i4) errors.AddRange(i4.Errors);
            if (@this.Item5 is Invalid<TE> i5) errors.AddRange(i5.Errors);
            if (@this.Item6 is Invalid<TE> i6) errors.AddRange(i6.Errors);
            if (@this.Item7 is Invalid<TE> i7) errors.AddRange(i7.Errors);
            if (@this.Item8 is Invalid<TE> i8) errors.AddRange(i8.Errors);

            return NonEmptyList<TE>.Of(errors[0], [.. errors.Skip(1)]);
        }
    }
}
