# smsboxDemo
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
  
