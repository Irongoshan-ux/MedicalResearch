namespace MedicineManaging.API.Utilities.DotNetTaskExecutors
{
    public class TaskExecutor
    {
        public static Task<bool> GetResultAsync(Action action)
        {
            try
            {
                action();
            }
            catch
            {
                return Task.FromResult(false);
            }
            return Task.FromResult(true);
        }
    }
}
