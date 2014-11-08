using System;
using System.Collections.Generic;
using System.Configuration;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Security.Cryptography;
using Mweb.Foundation.Practices.Logging;
using Skizzel.Domain.Entities;
using Skizzel.Infrastructure.Catalog;
using Skizzel.Infrastructure.Uow;
using Skizzel.Domain.Utils;

namespace Skizzel.Domain.Logic
{
 public class CoreLogic
 {
  private static readonly ILogger Log = LogManager.GetLogger(typeof(CoreLogic));

  public int CreateUser(UserEntity newUser)
  {
   string methodName = MethodBase.GetCurrentMethod().Name;
   try
   {
    using (var uow = new Uow<User>())
    {
     var user = uow.Repository.Insert(new User
     {
      Email = newUser.Email,
      Name = newUser.Name,
      Password = newUser.Password
     });

     uow.Save();

     return user.UserId;
    }
   }
   catch (Exception ex)
   {
    Log.Info(methodName, string.Format("CreateUser Exception Occured: {0}", ex.Message));
   }

   return 0;
  }

  public int CreateReceipt(ReceiptEntity newReciept)
  {
   string methodName = MethodBase.GetCurrentMethod().Name;
   try
   {

    var todaysDate = DateTime.Now;
    
    using (var uow = new Uow<Receipt>())
    {
     var receipt = uow.Repository.Insert(new Receipt
     {
      UserId = newReciept.UserId,
      CategoryId = newReciept.CategoryId,
      Alias = newReciept.Alias,
      DateCreated = newReciept.DateCreated,
      FilterDate = Utils.Utils.GetFriendlyDate(newReciept.DateCreated)
     });

     uow.Save();

     return receipt.ReceiptId;
    }
   }
   catch (Exception ex)
   {
    Log.Info(methodName, string.Format("CreateReseipt Exception Occured: {0}", ex.Message));
   }

   return 0;
  }

  public int CreateMillage(MillageEntity newMillage)
  {

   string methodName = MethodBase.GetCurrentMethod().Name;
   try
   {

    using (var uow = new Uow<Millage>())
    {
     var milliage = uow.Repository.Insert(new Millage
     {
      UserId = newMillage.UserId,
      CategoryId = newMillage.CategoryId,
      StartLat = newMillage.StartLat,
      StartLong = newMillage.StartLong,
      StopLat = newMillage.StopLat,
      StopLong = newMillage.StopLong,
      Alias = newMillage.Alias,
      DateCreated = newMillage.DateCreated,
      FilterDate = Utils.Utils.GetFriendlyDate(newMillage.DateCreated),
      Total = LocationHelper.CalulateDistance(double.Parse(newMillage.StartLat), double.Parse(newMillage.StartLong), double.Parse(newMillage.StopLat),
                                             double.Parse(newMillage.StopLong), 'K').ToString(CultureInfo.InvariantCulture)
     });

     uow.Save();

     return milliage.MillageId;
    }
   }
   catch (Exception ex)
   {
    Log.Info(methodName, string.Format("CreateMillage Exception Occured: {0}", ex.Message));
   }

   return 0;
  }

  public int AuthenticateUser(UserEntity user)
  {
   string methodName = MethodBase.GetCurrentMethod().Name;

   try
   {
    using (var uow = new Uow<User>())
    {
     var userQuery =
       (from allUsers in uow.Repository.Find(a => a.Email == user.Email && a.Password == user.Password)
        select allUsers).SingleOrDefault();

     if (userQuery != null)
     {
      return GetUserId(userQuery.Email);
     }
    }
   }
   catch (Exception ex)
   {

    throw;
   }

   return 0;
  }

  public UserEntity GetUserOverview(int userId, string receiptMonth)
  {

   var dateParts = receiptMonth.Split('_');
   var filterMonth = dateParts[0] + " " + dateParts[1];
   using (var uow = new Uow<User>())
   {
    var userQuery = (from allUsers in uow.Repository.Find(a => a.UserId == userId)
                     select allUsers).Single();

    return new UserEntity
    {
     Email = userQuery.Email,
     Password = userQuery.Password,
     ReceiptList = GetReceiptList(userQuery.Receipts, filterMonth),
     Name = userQuery.Name,
     CategoriesList = GetUserCategories(userId),
     MillageList = GetMillageList(userQuery.Millages, filterMonth)
     

    };
   }
  }

  public List<UserMonthsEntity> GetUserMonths(int userId)
  {
   var userMonthsOverview = new List<UserMonthsEntity>();
   using (var uow = new Uow<Receipt>())
   {
    var userMonths = (from allMonths in uow.Repository.Find(a => a.UserId == userId)
                      select allMonths.FilterDate).Distinct();

    foreach (var month in userMonths)
    {
     userMonthsOverview.Add(new UserMonthsEntity
     {
      MonthCount = GetUsersMonthCount(userId, month),
      ReceiptMonth = month

     });
    }

    return userMonthsOverview;
   }

  }

  public List<UserMonthsEntity> GetUserMillageMonths(int userId)
  {
   var userMonthsOverview = new List<UserMonthsEntity>();
   using (var uow = new Uow<Millage>())
   {
    var userMonths = (from allMonths in uow.Repository.Find(a => a.UserId == userId)
                      select allMonths.FilterDate).Distinct();

    foreach (var month in userMonths)
    {
     userMonthsOverview.Add(new UserMonthsEntity
     {
      MonthCount = GetUsersMonthMillageCount(userId, month),
      ReceiptMonth = month

     });
    }

    return userMonthsOverview;
   }

  }

  public int CreateImage(ImageEntity newImage)
  {
    string methodName = MethodBase.GetCurrentMethod().Name;
   try
   {
    using (var uow = new Uow<Image>())
    {
     var image = uow.Repository.Insert(new Image
     {
     ReceiptId = newImage.ReceiptId,
     ImageUrl = newImage.ImageUrl
      
     });

     uow.Save();

     return image.ReceiptId;
    }
   }
   catch (Exception ex)
   {
    Log.Info(methodName, string.Format("CreateReseipt Exception Occured: {0}", ex.Message));
   }

   return 0;
  }
  
  private int GetUsersMonthCount(int userId, string filterMonth)
  {
   using (var uow = new Uow<Receipt>())
   {
    return (from allMonths in uow.Repository.Find(a => a.UserId == userId && a.FilterDate == filterMonth)
            select allMonths).Count();
   }
  }

  private int GetUsersMonthMillageCount(int userId, string filterMonth)
  {
   using (var uow = new Uow<Millage>())
   {
    return (from allMonths in uow.Repository.Find(a => a.UserId == userId && a.FilterDate == filterMonth)
            select allMonths).Count();
   }
  }

  private List<CategoryEntity> GetUserCategories(int userId)
  {
   var userCategoryList = new List<CategoryEntity>();
   using (var uow = new Uow<Category>())
   {
    var categoryQuery = (from allCategories in uow.Repository.Find(a => a.UserId == userId)
                         select allCategories);

    foreach (var category in categoryQuery)
    {
     userCategoryList.Add(new CategoryEntity
     {
      Category = category.Type,
      CategoryId = category.CategoryId,
      UserId = userId
     });
    }
   }

   return userCategoryList;
  }

  private int GetUserId(string email)
  {
   try
   {
    using (var uow = new Uow<User>())
    {
     return (from allUsers in uow.Repository.Find(a => a.Email == email)
             select allUsers).Single().UserId;
    }
   }
   catch (Exception)
   {

    throw;
   }
  }

  private static List<MillageEntity> GetMillageList(IEnumerable<Millage> millages, string filterMonth)
  {

   var filterMillagesQuery = (from allMillages in millages.Where(a => a.FilterDate == filterMonth)
                             select allMillages);

   return filterMillagesQuery.Select(item => new MillageEntity
   {
    StartLat = item.StartLat,
    StartLong = item.StartLong,
    StopLat = item.StopLat,
    StopLong = item.StartLong,
    CategoryId = item.CategoryId,
    MillageId = item.MillageId,
    UserId = item.UserId,
    Alias = item.Alias,
    DateCreated = item.DateCreated,
    FilterDate = item.FilterDate,
    Total = double.Parse(item.Total)
    
   }).ToList();
  } 

  private static List<ReceiptEntity> GetReceiptList(IEnumerable<Receipt> receipts, string filterMonth)
  {

   var filterReceiptQuery = (from allReceipts in receipts.Where(a => a.FilterDate == filterMonth)
    select allReceipts);


   return filterReceiptQuery.Select(item => new ReceiptEntity
   {
    Alias = item.Alias, CategoryId = item.CategoryId, Category = item.Category.Type ,DateCreated = item.DateCreated, ReceiptId = item.ReceiptId, UserId = item.UserId, ReceiptImagesList = GetImageList(item.Images)
   }).ToList();
  }

  private static List<ImageEntity> GetImageList(IEnumerable<Image> images)
  {
   return images.Select(image => new ImageEntity
   {
    ImageId = image.ImageId, ReceiptId = image.ReceiptId, ImageUrl = ConfigurationManager.AppSettings["ImageURL"] + image.ImageUrl
   }).ToList();
  }
 }
}



