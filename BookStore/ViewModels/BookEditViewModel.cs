using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BookStore.ViewModels
{
  public class BookEditViewModel : BookCreateViewModel
  {
    public int Id { get; set; }
    public string ExistingPhotoUniqueName { get; set; }
  }
}
