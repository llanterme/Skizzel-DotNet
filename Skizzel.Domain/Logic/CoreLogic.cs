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

  public List<CategoryEntity> CreateCategory(CategoryEntity newCategory)
  {
   string methodName = MethodBase.GetCurrentMethod().Name;
   try
   {
    using (var uow = new Uow<Category>())
    {
    
     var categories = (from allCategories in uow.Repository.Find(a => a.Type == newCategory.Category)
      select allCategories);

     if (!categories.Any())
     {

      uow.Repository.Insert(new Category
      {
       Type = newCategory.Category,
       UserId = newCategory.UserId
      });

      uow.Save();

      return GetUserCategories(newCategory.UserId);


     }
     else
     {
      return GetUserCategories(newCategory.UserId);
     }
    
    }
   }
   catch (Exception ex)
   {
    Log.Info(methodName, string.Format("CreateUser Exception Occured: {0}", ex.Message));
   }

   return null;
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

  public List<CategoryEntity> GetUserCategories(int userId)
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

  public  List<MillageEntity> GetMillageOverview(int userId, string millageMonth)
  {

   var dateParts = millageMonth.Split('_');
   var filterMonth = dateParts[0] + " " + dateParts[1];

   using (var uow = new Uow<Millage>())
   {
    var millageQuery = (from receipts in uow.Repository.Find(a => a.UserId == userId && a.FilterDate == filterMonth)
                        select receipts);
  

   return millageQuery.Select(item => new MillageEntity
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
    Total = Math.Round(double.Parse(item.Total),4),
    Category = item.Category.Type
    
   }).ToList();
   }
  }

  public List<ReceiptEntity> GetReceiptOverView(int userId, string receiptMonth, int categoryId)
  {

   var dateParts = receiptMonth.Split('_');
   var filterMonth = dateParts[0] + " " + dateParts[1];

   using (var uow = new Uow<Receipt>())
   {
    var receiptQuery = (from receipts in uow.Repository.Find(a => a.UserId == userId && a.FilterDate == filterMonth && a.CategoryId == categoryId)
     select receipts);


    return receiptQuery.Select(item => new ReceiptEntity
    {
     Alias = item.Alias,
     CategoryId = item.CategoryId,
     Category = item.Category.Type,
     DateCreated = item.DateCreated,
     ReceiptId = item.ReceiptId,
     UserId = item.UserId,
     ReceiptImagesList = GetImageList(item.Images)
    }).ToList();
   }
  }

  public List<ReceiptCategoryEntity> GetUsersReceiptCategory(int userId, string receiptMonth)
  {
   var dateParts = receiptMonth.Split('_');
   var filterMonth = dateParts[0] + " " + dateParts[1];

   using (var uow = new Uow<Receipt>())
   {
    var receiptCategoryQuery = (from categories in
     uow.Repository.Get().Where(a => a.UserId == userId && a.FilterDate == filterMonth)
     select categories).GroupBy(p => p.CategoryId).Select(g => g.First());

    var receiptCategoryList = new List<ReceiptCategoryEntity>();
    foreach (var cat in receiptCategoryQuery)
    {
      receiptCategoryList.Add(new ReceiptCategoryEntity
      {
        ReceiptCategory = cat.Category.Type,
        CategoryCount = GetUserReceiptCategoryCount(userId,cat.CategoryId, filterMonth),
        CategoryId = cat.CategoryId
      });
    }

    return receiptCategoryList;
   }

  }

  private static List<ImageEntity> GetImageList(IEnumerable<Image> images)
  {
   return images.Select(image => new ImageEntity
   {
    ImageId = image.ImageId, ReceiptId = image.ReceiptId, ImageUrl = ConfigurationManager.AppSettings["ImageURL"] + image.ImageUrl
   }).ToList();
  }

  private int GetUsersMonthCount(int userId, string filterMonth)
  {
   using (var uow = new Uow<Receipt>())
   {
    return (from allMonths in uow.Repository.Find(a => a.UserId == userId && a.FilterDate == filterMonth)
            select allMonths).Count();
   }
  }

  private int GetUserReceiptCategoryCount(int userId, int categoryId, string filterMonth)
  {
   using (var uow = new Uow<Receipt>())
   {
    return (from allCats in uow.Repository.Find(a => a.UserId == userId && a.CategoryId == categoryId && a.FilterDate == filterMonth)
            select allCats).Count();
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
 }
}



