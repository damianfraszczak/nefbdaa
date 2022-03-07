using System.Threading.Tasks;

namespace NEFBDAACommons.Shared.Extensions
{
    public static class TaskExtension
    {
        public static T RunSync<T>(this Task<T> task)
        {
            return task.Result;
        }
    }
}
