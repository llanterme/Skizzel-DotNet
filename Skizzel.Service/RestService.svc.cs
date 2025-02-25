﻿
using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.ServiceModel.Activation;
using System.Web.Hosting;
using Skizzel.Domain.Entities;
using Skizzel.Domain.Logic;
using Skizzel.Service.Wrappers;

namespace Skizzel.Service
{

 [AspNetCompatibilityRequirements(RequirementsMode = AspNetCompatibilityRequirementsMode.Allowed)]
 public class RestService : IRestService
 {
  private readonly CoreLogic _manager = new CoreLogic();

  public ReceiptOverviewResponse GetReceiptOverview(string userId, string receiptMonth, string categoryId)
  {
   var receiptOverView = _manager.GetReceiptOverView(int.Parse(userId), receiptMonth, int.Parse(categoryId));

   if (receiptOverView != null)
   {
    return new ReceiptOverviewResponse
    {
     ReceiptList = receiptOverView,
     Message = "success",
     Status = "success"

    };
   }
   return null;
  }

  public MillageOverviewResponse GetMillageOvreview(string userId, string receiptMonth)
  {
   var milageOverview = _manager.GetMillageOverview(int.Parse(userId), receiptMonth);

   if (milageOverview != null)
   {
    return new MillageOverviewResponse
    {
     MillageList = milageOverview,
     Message = "success",
     Status = "success"

    };
   }
   return null;
  }

  public AuthenticateResponse AuthenticateUser(UserEntity user)
  {
   var authenticateUser = _manager.AuthenticateUser(user);

   if (authenticateUser != 0)
   {
    return new AuthenticateResponse
    {
     Message = authenticateUser.ToString(CultureInfo.InvariantCulture),
     Status = "success",
     UserCategories = _manager.GetUserCategories(authenticateUser)
    };
   }

   return new AuthenticateResponse
   {
    Message = "denied",
    Status = "denied"
   };
  }

  public AbstractResponse CreateReceipt(ReceiptEntity receipt)
  {
   var newReceipt = _manager.CreateReceipt(receipt);

   if (newReceipt != 0)
   {
    return new AbstractResponse
    {
     Message = newReceipt.ToString(CultureInfo.InvariantCulture),
     Status = "success"
    };
   }

   return new AbstractResponse
   {
    Message = "failed",
    Status = "failed"
   };

  }

  public List<ReceiptCategoryEntity> GetUsersReceiptCategoryCount(string userId, string receiptMonth)
  {
   return _manager.GetUsersReceiptCategory(int.Parse(userId), receiptMonth);
  }

  public AbstractResponse CreateMillage(MillageEntity millage)
  {
   var newMillage = _manager.CreateMillage(millage);

   if (newMillage != 0)
   {
    return new AbstractResponse
    {
     Message = newMillage.ToString(CultureInfo.InvariantCulture),
     Status = "success"
    };
   }

   return new AbstractResponse
   {
    Message = "failed",
    Status = "failed"
   };

  }

  public CreateCategoryResponse CreateCategory(CategoryEntity category)
  {
   var categoriesList = _manager.CreateCategory(category);

   if (categoriesList != null)
   {
    return new CreateCategoryResponse
    {
     Message = "success",
     Status = "success",
     UserCategories = categoriesList
     
    };
   }

   return new CreateCategoryResponse
   {
    Message = "failed",
    Status = "failed",
    UserCategories = null
   };
  }

  public List<UserMonthsEntity> GetUserMillageDates(string userId)
  {
   return _manager.GetUserMillageMonths(int.Parse(userId));
  }

  public AbstractResponse RegisterUser(UserEntity user)
  {
   var newUser = _manager.CreateUser(user);

   if (newUser != 0)
   {
    return new AbstractResponse
    {
     Message = newUser.ToString(CultureInfo.InvariantCulture),
     Status = "success"
    };
   }

   return new AbstractResponse
   {
    Message = "failed",
    Status = "failed"
   };


  }
 
  public List<UserMonthsEntity> GetUserDates(string userId)
  {
   return _manager.GetUserMonths(int.Parse(userId));
  }

  public AbstractResponse UploadCustomFile(string fileName, Stream stream)
  {

   var imageRaw = fileName.Split('_');
   var receiptId = imageRaw[1];
   var saveFileName = Guid.NewGuid() + imageRaw[0];

   try
   {

    string filePath = Path.Combine(HostingEnvironment.MapPath("~/_upload"), saveFileName);

    using (var writer = new FileStream(filePath, FileMode.Create))
    {
     int readCount;
     var buffer = new byte[8192];
     while ((readCount = stream.Read(buffer, 0, buffer.Length)) != 0)
     {
      writer.Write(buffer, 0, readCount);
     }
    }

    var imageId = _manager.CreateImage(new ImageEntity
    {
     ReceiptId = int.Parse(receiptId),
     ImageUrl = saveFileName
    });

    if (imageId != 0)
    {
     return new AbstractResponse
     {
      Message = imageId.ToString(CultureInfo.InvariantCulture),
      Status = "success"
     };
    }
   }
   catch (IOException ex)
   {

   }
   return new AbstractResponse
   {
    Message = "failed",
    Status = "failed"
   };

  }

 }
}
