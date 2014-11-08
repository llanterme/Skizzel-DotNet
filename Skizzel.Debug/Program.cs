using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Skizzel.Infrastructure.Catalog;
using Skizzel.Infrastructure.Uow;

namespace Skizzel.Debug
{
 class Program
 {
  static void Main(string[] args)
  {
   using (var uow = new Uow<User>())
   {
    var userQuery = (from allUsers in uow.Repository.Find(a => a.UserId == 1)
                     select allUsers).Single();


    List<Receipt> managementList = userQuery.Receipts.ToList();

    foreach (var item in managementList)
    {
      Console.WriteLine(item.ReceiptId);
     Console.ReadLine();
    }
   }
  }
 }
}
