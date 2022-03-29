using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web;

namespace GlobusCustomerAPI.Models
{
    public partial class SendInstantSMS
    {
        // SMS Sending function
        public string SendSMS(string MobileNo, string Message)
        {
            string MainUrl = "SMSAPIURL"; //Here need to give SMS API URL
            string UserName = "username"; //Here need to give username
            string Password = "Password"; //Here need to give Password
            string SenderId = "SenderId";
            string strMobileno = MobileNo;
            string URL = "";
            URL = MainUrl + "username=" + UserName + "&msg_token=" + Password + "&sender_id=" + SenderId + "&message=" + HttpUtility.UrlEncode(Message).Trim() + "&mobile=" + strMobileno.Trim() + "";
            var request = new HttpRequestMessage
            {
                Method = HttpMethod.Get,
                RequestUri = new Uri(URL)
            };
            string strResponce = GetResponse(request);
            string msg = "";
            if (strResponce.Equals("Fail"))
            {
                msg = "Fail";
            }
            else
            {
                msg = strResponce;
            }
            return msg;
        }
        // End SMS Sending function
        // Get Response function
        public static string GetResponse(HttpRequestMessage smsURL)
        {
            try
            {
                HttpClient objWebClient = new HttpClient();
                using (var reader = objWebClient.SendAsync(smsURL))
                {
                    return "success";
                }
            }
            catch (Exception)
            {
                return "Fail";
            }
        }
    }
}
