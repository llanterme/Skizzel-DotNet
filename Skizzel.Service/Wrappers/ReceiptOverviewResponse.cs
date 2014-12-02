using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Skizzel.Domain.Entities;

namespace Skizzel.Service.Wrappers
{
 public class ReceiptOverviewResponse : AbstractResponse
 {
  public List<ReceiptEntity> ReceiptList { get; set; }
 }
}