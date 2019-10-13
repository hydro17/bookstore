using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.AspNetCore.Mvc.ModelBinding.Binders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BookStore.ModelBinders
{
  public class DecimalModelBinderProvider : IModelBinderProvider
  {
    public IModelBinder GetBinder(ModelBinderProviderContext context)
    {
      if (context == null) throw new ArgumentNullException (nameof(context));

      if (context.Metadata.ModelType == typeof(decimal))
      {
        return new BinderTypeModelBinder(typeof(DecimalModelBinder));
      }

      return null;
    }
  }
}
