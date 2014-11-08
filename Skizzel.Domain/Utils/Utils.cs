using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Skizzel.Domain.Utils
{
  public static class Utils
  {
    public static string GetFriendlyDate(string dateCreated)
    {

      var dateRaw = dateCreated.Split('-');
      var returnDate = string.Empty;

      switch (dateRaw[1])
      {
        case "01":
          returnDate = "January";
          break;
        case "02":
          returnDate = "Febuary";
          break;
        case "03":
          returnDate = "March";
          break;
        case "04":
          returnDate = "April";
          break;
        case "05":
          returnDate = "May";
          break;
        case "06":
          returnDate = "June";
          break;
        case "07":
          returnDate = "July";
          break;
        case "08":
          returnDate = "August";
          break;
        case "09":
          returnDate = "September";
          break;
        case "10":
          returnDate = "October";
          break;
        case "11":
          returnDate = "November";
          break;
        case "12":
          returnDate = "December";
          break; 
      }

      return returnDate + " " + dateRaw[0] ;
    }
  }
}
