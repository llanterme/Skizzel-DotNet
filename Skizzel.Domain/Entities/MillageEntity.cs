using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Skizzel.Domain.Entities
{
 public class MillageEntity
 {

  public int UserId { get; set; }
  public int MillageId { get; set; }
  public int CategoryId { get; set; }
  public string Alias { get; set; }
  public string FilterDate { get; set; }
  public string DateCreated { get; set; }
  public string StartLat { get; set; }
  public string StartLong { get; set; }
  public string StopLat { get; set; }
  public string StopLong { get; set; }
  public Double Total { get; set; }
  public string Category { get; set; }
 }
}
