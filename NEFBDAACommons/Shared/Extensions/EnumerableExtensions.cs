using System;
using System.Collections.Generic;
using System.Text;

namespace NEFBDAACommons.Shared.Extensions
{
  public static class EnumerableExtensions
  {
    public static void ForEach<T>(
        this IEnumerable<T> source,
        Action<T> action)
    {
      if (source != null)
      {
        foreach (T element in source)
          action(element);
      }

    }

  }
}
