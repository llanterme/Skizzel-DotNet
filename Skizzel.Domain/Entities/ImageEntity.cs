using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Skizzel.Domain.Entities
{
  public class ImageEntity
  {
    public int ReceiptId { get; set; }
    public string ImageUrl { get; set; }
    public int ImageId { get; set; }
  }
}
