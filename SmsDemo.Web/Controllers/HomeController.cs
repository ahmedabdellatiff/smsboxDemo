using SmsDemo.Web.Models;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;

namespace SmsDemo.Web.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            var model = new IndexViewModel();
            return View(model);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Index(IndexViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }
            SendSms(model.Mobile, model.Message);
            TempData["Message"] = "تم ارسال الرسالة بنجاح";
            return View(new IndexViewModel());
        }
        [NonAction]
        void SendSms(string number, string message)
        {
            var username = ConfigurationManager.AppSettings["SmsUsername"];
            var password = ConfigurationManager.AppSettings["SmsPassword"];
            var custId = ConfigurationManager.AppSettings["SmsCustomerId"];
            var senderName = ConfigurationManager.AppSettings["SmsSenderText"];

            HttpWebRequest req = (HttpWebRequest)WebRequest.Create("https://www.smsbox.com/SMSGateway/Services/Messaging.asmx/Http_SendSMS");
            req.Method = "POST";
            req.ContentType = "application/x-www-form-urlencoded";
            string postData = string.Format("username={0}&password={1}&customerId={2}&senderText={3}&messageBody={4}&recipientNumbers={5}&defDate=&isBlink=false&isFlash=false",
               username, password, custId, senderName, HttpUtility.UrlEncode( message), number);
            req.ContentLength = postData.Length;

            StreamWriter stOut = new
            StreamWriter(req.GetRequestStream(),
            System.Text.Encoding.ASCII);
            stOut.Write(postData);
            stOut.Close();
            string strResponse;
            StreamReader stIn = new StreamReader(req.GetResponse().GetResponseStream());
            strResponse = stIn.ReadToEnd();
            stIn.Close();

        }
    }
}