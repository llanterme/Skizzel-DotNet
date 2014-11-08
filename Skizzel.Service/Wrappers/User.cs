using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Skizzel.Domain.Entities;

namespace Skizzel.Service.Wrappers
{
  public class User : AbstractResponse
  {
    public UserEntity UserOverView { get; set; }
  }
}