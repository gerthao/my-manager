namespace MyManager.Common.Extensions;

public static class TaskExtensions
{
    extension<T, TA>(Task<T> task) where T: TA where TA: class
    {
        public async Task<TA> As()
        {
            return await task;
        }
    }

    extension<T>(T value)
    {
        public Task<T> ToTask() => Task.FromResult(value);
    }
}
