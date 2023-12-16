using System;
using System.Threading;
using System.Web.Mvc;

namespace LoDeOnline.Applications.ModelBinders
{
    public class DateTimeModelBinder : IModelBinder
    {
        public DateTimeModelBinder()
        {
        }

        public object BindModel(ControllerContext controllerContext, ModelBindingContext bindingContext)
        {
            var value = bindingContext.ValueProvider.GetValue(bindingContext.ModelName);

            if (value == null || string.IsNullOrEmpty(value.AttemptedValue))
            {
                return null;
            }

            return DateTime.Parse(value.AttemptedValue, Thread.CurrentThread.CurrentCulture);
        }
    }
}