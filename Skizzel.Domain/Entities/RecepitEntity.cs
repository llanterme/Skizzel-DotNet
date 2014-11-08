using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Skizzel.Domain.Entities
{
  public class ReceiptEntity
  {
    public int ReceiptId { get; set; }
    public int UserId { get; set; }
    public int CategoryId { get; set; }
    public string Category { get; set; }
    public string Alias { get; set; }
    public string DateCreated { get; set; }
    public List<ImageEntity> ReceiptImagesList { get; set; }

  }
}
