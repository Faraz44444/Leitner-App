using System;
using System.Collections.Generic;

namespace Domain.Model.BaseModels
{
    public abstract class PagedModel
    {
        public int TotalNumberOfItems { get; set; }
    }
    public class PagedModel<T>
    {
        public int CurrentPage { get; set; }
        public int ItemsPerPage { get; set; }
        public int TotalNumberOfItems { get; set; }
        public int TotalPages => Convert.ToInt32(Math.Ceiling((double)TotalNumberOfItems / ItemsPerPage));
        public IEnumerable<T> Items { get; set; }

    }
}