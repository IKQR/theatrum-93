using System.Collections.Generic;

namespace Theatrum.Web.Razor.Models
{
    public class PaginationOptionsModel
    {
        public readonly int Count;
        public readonly int Current;
        public readonly string Action;
        public readonly string Controller;

        public bool HasNext => Current < Count;
        public bool HasPrevious => Current > 1;

        public PaginationOptionsModel(
            int count,
            int current,
            string action,
            string controller = "")
        {
            Count = count;
            Current = current;
            Action = action;
            Controller = controller;
        }
    }

    public class PaginationViewModel<TInnerModel>
    {
        public  List<TInnerModel> Models { get; }
        public PaginationOptionsModel Options { get; set; }
    }
}
