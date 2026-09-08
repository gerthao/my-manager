using System.Globalization;

namespace MyManager.Common.Extensions;

public static class StringExtensions
{
    extension(string str)
    {
        public Option<bool> ToBoolOption() =>
            bool.TryParse(str, out var result) ? new Some<bool>(result) : new None();

        public Option<DateTimeOffset> ToDateTimeOffsetOption() =>
            DateTimeOffset.TryParse(str, out var result)
                ? new Some<DateTimeOffset>(result)
                : new None();

        public Option<DateTime> ToDateTimeOption() =>
            DateTime.TryParse(str, out var result) ? new Some<DateTime>(result) : new None();

        public Option<decimal> ToDecimalOption() =>
            decimal.TryParse(str, out var result) ? new Some<decimal>(result) : new None();

        public Option<decimal> ToDecimalOption(NumberStyles style, IFormatProvider? provider = null) =>
            decimal.TryParse(str, style, provider, out var result) ? new Some<decimal>(result) : new None();

        public Option<double> ToDoubleOption() =>
            double.TryParse(str, out var result) ? new Some<double>(result) : new None();

        public Option<double> ToDoubleOption(NumberStyles style, IFormatProvider? provider = null) =>
            double.TryParse(str, style, provider, out var result) ? new Some<double>(result) : new None();

        public Option<float> ToFloatOption() =>
            float.TryParse(str, out var result) ? new Some<float>(result) : new None();

        public Option<float> ToFloatOption(NumberStyles style, IFormatProvider? provider = null) =>
            float.TryParse(str, style, provider, out var result) ? new Some<float>(result) : new None();

        public Option<Guid> ToGuidOption() =>
            Guid.TryParse(str, out var result) ? new Some<Guid>(result) : new None();

        public Option<int> ToIntOption() =>
            int.TryParse(str, out var result) ? new Some<int>(result) : new None();

        public Option<int> ToIntOption(NumberStyles style, IFormatProvider? provider = null) =>
            int.TryParse(str, style, provider, out var result) ? new Some<int>(result) : new None();

        public Option<long> ToLongOption() =>
            long.TryParse(str, out var result) ? new Some<long>(result) : new None();

        public Option<long> ToLongOption(NumberStyles style, IFormatProvider? provider = null) =>
            long.TryParse(str, style, provider, out var result) ? new Some<long>(result) : new None();

        public Option<string> ToNonEmpty(bool includesWhiteSpace = false)
        {
            if (includesWhiteSpace)
            {
                return string.IsNullOrWhiteSpace(str) ? new None() : new Some<string>(str);
            }

            return string.IsNullOrEmpty(str) ? new None() : new Some<string>(str);
        }

        public Option<TimeSpan> ToTimeSpanOption() =>
            TimeSpan.TryParse(str, out var result) ? new Some<TimeSpan>(result) : new None();

        public Option<Uri> ToUriOption() => Uri.TryCreate(str, UriKind.Absolute, out var result)
            ? new Some<Uri>(result)
            : new None();

        public Option<Version> ToVersionOption() =>
            Version.TryParse(str, out var result) ? new Some<Version>(result) : new None();
    }
}
