using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.ServiceModel.Web;
using System.Text;
using Skizzel.Domain.Entities;
using Skizzel.Service.Wrappers;

namespace Skizzel.Service
{
  
  [ServiceContract]
  public interface IRestService
  {

        [OperationContract]
        [Description("Authenticate a user")]
        [WebInvoke(BodyStyle = WebMessageBodyStyle.WrappedRequest,
        Method = "POST",
        UriTemplate = "/Authenticate",
        RequestFormat = WebMessageFormat.Json,
        ResponseFormat = WebMessageFormat.Json)]
        AbstractResponse AuthenticateUser(UserEntity user);

        [OperationContract]
        [Description("create a new receipt")]
        [WebInvoke(BodyStyle = WebMessageBodyStyle.WrappedRequest,
        Method = "POST",
        UriTemplate = "/CreateReceipt",
        RequestFormat = WebMessageFormat.Json,
        ResponseFormat = WebMessageFormat.Json)]
        AbstractResponse CreateReceipt(ReceiptEntity receipt);

        [OperationContract]
        [Description("create a new milliage")]
        [WebInvoke(BodyStyle = WebMessageBodyStyle.WrappedRequest,
        Method = "POST",
        UriTemplate = "/CreateMillage",
        RequestFormat = WebMessageFormat.Json,
        ResponseFormat = WebMessageFormat.Json)]
        AbstractResponse CreateMillage(MillageEntity millage);

        [OperationContract]
        [Description("Register a new user")]
        [WebInvoke(BodyStyle = WebMessageBodyStyle.WrappedRequest,
        Method = "POST",
        UriTemplate = "/RegisterUser",
        RequestFormat = WebMessageFormat.Json,
        ResponseFormat = WebMessageFormat.Json)]
        AbstractResponse RegisterUser(UserEntity user);


        [OperationContract]
        [Description("Gets a overview of a users month.")]
        [WebInvoke(BodyStyle = WebMessageBodyStyle.Bare,
        Method = "GET",
        UriTemplate = "/UserDates/{userId}",
        RequestFormat = WebMessageFormat.Json,
        ResponseFormat = WebMessageFormat.Json)]
        List<UserMonthsEntity> GetUserDates(string userId);

        [OperationContract]
        [Description("Gets a overview of a users millage month.")]
        [WebInvoke(BodyStyle = WebMessageBodyStyle.Bare,
        Method = "GET",
        UriTemplate = "/MillageDates/{userId}",
        RequestFormat = WebMessageFormat.Json,
        ResponseFormat = WebMessageFormat.Json)]
        List<UserMonthsEntity> GetUserMillageDates(string userId);

        [OperationContract]
        [Description("Gets a overview of a user.")]
        [WebInvoke(BodyStyle = WebMessageBodyStyle.Bare,
        Method = "GET",
        UriTemplate = "/UserOverView/{userId}/{receiptMonth}",
        RequestFormat = WebMessageFormat.Json,
        ResponseFormat = WebMessageFormat.Json)]
        User GetUserOverview(string userId, string receiptMonth);


        [OperationContract]
        [WebInvoke(Method = "POST", ResponseFormat = WebMessageFormat.Json,
        UriTemplate = "/UploadFile?fileName={fileName}")]
        AbstractResponse UploadCustomFile(string fileName, Stream stream);
  }
}
