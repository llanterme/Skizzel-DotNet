using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Skizzel.Domain.Entities
{
 public class ReceiptCategoryEntity
 {
  public int CategoryId { get; set; }
  public string ReceiptCategory { get; set; }
  public int CategoryCount { get; set; }
 }
}
