using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Skizzel.Domain.Entities
{
  public class CategoryEntity
  {
    public int CategoryId { get; set; }
    public int UserId { get; set; }
    public string Category { get; set; }
  }
}
