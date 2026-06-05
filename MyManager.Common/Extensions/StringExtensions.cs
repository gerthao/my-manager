using System.Globalization;

namespace MyManager.Common.Extensions;

public static class StringExtensions
{
    extension(string str)
    {
        public Option<bool> ToBoolOption() =>
            bool.TryParse(str, out var result) ? new Some<bool>(result) : Option<bool>.Empty;

        public Option<DateTimeOffset> ToDateTimeOffsetOption() =>
            DateTimeOffset.TryParse(str, out var result)
                ? new Some<DateTimeOffset>(result)
                : Option<DateTimeOffset>.Empty;

        public Option<DateTime> ToDateTimeOption() =>
            DateTime.TryParse(str, out var result) ? new Some<DateTime>(result) : Option<DateTime>.Empty;

        public Option<decimal> ToDecimalOption() =>
            decimal.TryParse(str, out var result) ? new Some<decimal>(result) : Option<decimal>.Empty;

        public Option<decimal> ToDecimalOption(NumberStyles style, IFormatProvider? provider = null) =>
            decimal.TryParse(str, style, provider, out var result) ? new Some<decimal>(result) : Option<decimal>.Empty;

        public Option<double> ToDoubleOption() =>
            double.TryParse(str, out var result) ? new Some<double>(result) : Option<double>.Empty;

        public Option<double> ToDoubleOption(NumberStyles style, IFormatProvider? provider = null) =>
            double.TryParse(str, style, provider, out var result) ? new Some<double>(result) : Option<double>.Empty;

        public Option<float> ToFloatOption() =>
            float.TryParse(str, out var result) ? new Some<float>(result) : Option<float>.Empty;

        public Option<float> ToFloatOption(NumberStyles style, IFormatProvider? provider = null) =>
            float.TryParse(str, style, provider, out var result) ? new Some<float>(result) : Option<float>.Empty;

        public Option<Guid> ToGuidOption() =>
            Guid.TryParse(str, out var result) ? new Some<Guid>(result) : Option<Guid>.Empty;

        public Option<int> ToIntOption() =>
            int.TryParse(str, out var result) ? new Some<int>(result) : Option<int>.Empty;

        public Option<int> ToIntOption(NumberStyles style, IFormatProvider? provider = null) =>
            int.TryParse(str, style, provider, out var result) ? new Some<int>(result) : Option<int>.Empty;

        public Option<long> ToLongOption() =>
            long.TryParse(str, out var result) ? new Some<long>(result) : Option<long>.Empty;

        public Option<long> ToLongOption(NumberStyles style, IFormatProvider? provider = null) =>
            long.TryParse(str, style, provider, out var result) ? new Some<long>(result) : Option<long>.Empty;

        public Option<string> ToNonEmpty(bool includesWhiteSpace = false)
        {
            if (includesWhiteSpace)
            {
                return string.IsNullOrWhiteSpace(str) ? new None<string>() : new Some<string>(str);
            }

            return string.IsNullOrEmpty(str) ? new None<string>() : new Some<string>(str);
        }

        public Option<TimeSpan> ToTimeSpanOption() =>
            TimeSpan.TryParse(str, out var result) ? new Some<TimeSpan>(result) : Option<TimeSpan>.Empty;

        public Option<Uri> ToUriOption() => Uri.TryCreate(str, UriKind.Absolute, out var result)
            ? new Some<Uri>(result)
            : Option<Uri>.Empty;

        public Option<Version> ToVersionOption() =>
            Version.TryParse(str, out var result) ? new Some<Version>(result) : Option<Version>.Empty;
    }
}
