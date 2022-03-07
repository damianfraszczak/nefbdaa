using NEFBDAACommons.Shared.Extensions;
using System.Collections.Generic;
using System.Linq;
using Z.EntityFramework.Plus;

namespace NEFBDAACommons.Database.Models.PagedList
{
    public class PagedList<T>
    {
        public PagedList()
        {

        }

        public PagedList(long count,
            IEnumerable<T> list,
            int index,
            int size)
        {
            this.Count = count;
            this.List = list;
            this.Index = index;
            this.Size = size;
        }
        public PagedList(IQueryable<T> queryable, PagedListParameters parameters)
        {
            if (queryable == null)
            {
                return;
            }

             Count = queryable.LongCount();

            //parameters?.Orders?.ToList().ForEach(order => queryable = queryable.Order(order.Property, order.IsAscending));
            if (parameters?.Order != null)
            {
                var property = parameters.Order.Property;
                if (string.IsNullOrEmpty(property))
                {
                    property = "Id";
                }

                queryable = queryable.Order(property, parameters.Order.IsAscending);
            }

            if (parameters?.Page != null)
            {
                queryable = queryable?.Page(parameters.Page.Index, parameters.Page.Size);
                Index = parameters.Page.Index;
                Size = parameters.Page.Size;
            }
            List = queryable.Future();

            if (Size == 0) Size = List.Count();


        }

        public long Count { get; set; }
        public IEnumerable<T> List { get; set; }
        public int Index { get; set; }
        public int Size { get; set; }


    }
}
