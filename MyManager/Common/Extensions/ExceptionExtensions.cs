namespace MyManager.Common.Extensions;

public static class ExceptionExtensions
{
    extension<TException>(TException exception) where TException : Exception
    {
        public bool IsNonFatal() =>
            exception is not OperationCanceledException
                and not OutOfMemoryException
                and not StackOverflowException
                and not AccessViolationException
                and not AppDomainUnloadedException
                and not BadImageFormatException
                and not CannotUnloadAppDomainException;
    }
}
