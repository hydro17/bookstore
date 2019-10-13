using Microsoft.AspNetCore.Mvc.ModelBinding;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace BookStore.ModelBinders
{
  public class DecimalModelBinder : IModelBinder
  {
    public Task BindModelAsync(ModelBindingContext bindingContext)
    {
      if (bindingContext == null) throw new ArgumentNullException(nameof(bindingContext));

      string modelName = bindingContext.ModelName;
      var valueProviderResult = bindingContext.ValueProvider.GetValue(modelName);

      if (valueProviderResult == ValueProviderResult.None) return Task.CompletedTask;

      bindingContext.ModelState.SetModelValue(modelName, valueProviderResult);

      string stringValue = valueProviderResult.FirstValue;

      if (string.IsNullOrEmpty(stringValue)) return Task.CompletedTask;

      if (!decimal.TryParse(stringValue, out decimal decimalValue))
      {
        string stringValueOtherDecimalPoint = stringValue.Replace('.', ',');

        if (!decimal.TryParse(stringValueOtherDecimalPoint, out decimalValue))
        {
          // Non-decimal arguments result in model state errors
          bindingContext.ModelState.TryAddModelError(modelName, "Price must be decimal. Current value = " + stringValue);
          return Task.CompletedTask;
        }
      }

      bindingContext.Result = ModelBindingResult.Success(decimalValue);
      return Task.CompletedTask;
    }
  }
}
