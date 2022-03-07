using System.Linq;
using AgileObjects.AgileMapper;
namespace NEFBDAACommons.Shared.Extensions
{
    public static class MappingExtensions
    {
      
        public static IQueryable<TDestination> Project<TSource, TDestination>(this IQueryable<TSource> queryable)
        {
   
            return queryable.Project().To<TDestination>();
        }
        public static TDestination Map<TSource, TDestination>(this TSource source)
        {
            return Mapper.Map(source).ToANew<TDestination>();
        }

        public static TDestination Map<TDestination>(this object source)
        {
            return Mapper.Map(source).ToANew<TDestination>();
        }
        public static TDestination Map<TSource, TDestination>(this TSource source, TDestination destination)
        {
            return Mapper.Map(source).Over(destination);
        }

    }
}
