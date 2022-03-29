using GlobusCustomerAPI.Data.Contracts;
using GlobusCustomerAPI.Data.Models;
using GlobusCustomerAPI.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;

namespace GlobusCustomerAPI.Controllers
{
    [Route("api/v2/[controller]")]
    [ApiController]
    public class CustomerController : Controller
    {
        private ICustomerUnitOfWork customerUnitOfWork { get; set; }
        public SendInstantSMS sendInstantSMS = new SendInstantSMS();
   
        public CustomerController(ICustomerUnitOfWork _customerUnitOfWork)
        {
            customerUnitOfWork = _customerUnitOfWork;
        }
        [HttpGet]
        [Route("Index")]
        public IActionResult Index()
        {
            return View();
        }
        [HttpGet]
        [Route("LoadExistingCustomers")]
        public List<TblCustomerDetails> GetAllCustomers()
        {
            List<TblCustomerDetails> lstCustomers = new List<TblCustomerDetails>();
            try
            {
                var allcustomers = customerUnitOfWork.Customer.GetAll();
                foreach(var item in allcustomers)
                {
                    var addCustomers = new TblCustomerDetails()
                    {
                        Email = item.Email,
                        Password = item.Password,
                        PhoneNumber = item.PhoneNumber,
                        StateOfResidence = item.StateOfResidence,
                        LGA = item.LGA
                    };
                    lstCustomers.Add(addCustomers);
                }
            }
            catch (Exception ex)
            {

                throw ex;
            }
            return lstCustomers;
        }
        [HttpPost]
        [Route("PostCustomerDetails")]
        public HttpResponseMessage PostCustomerDetails([FromForm] CustomerInformation model)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    var sessionValueForOTP = HttpContext.Session.GetString("SessionName");
                    //var checkIfSession = VerifyOTP(sessionValueForOTP);
                    if (sessionValueForOTP != null)
                    {
                        var addCustomer = new TblCustomerDetails()
                        {
                            Email = model.Email,
                            Password = sessionValueForOTP,
                            PhoneNumber = model.PhoneNumber,
                            StateOfResidence = model.StateOfResidence,
                            LGA = model.LGA
                        };
                        customerUnitOfWork.Customer.Add(addCustomer);
                        customerUnitOfWork.Commit();
                        string message = "Your OTP is " + sessionValueForOTP + "(Sent by Globus Bank, Remember it will expire after 10minutes).\n\n. Thank you for choosing Globus Bank";
                        sendInstantSMS.SendSMS(model.PhoneNumber, message);
                    }
                    }
                    else
                {
                    return new HttpResponseMessage(System.Net.HttpStatusCode.BadRequest);
                }
            }
            catch (Exception ex)
            {

                return new HttpResponseMessage(System.Net.HttpStatusCode.BadRequest);
            }
            return new HttpResponseMessage(System.Net.HttpStatusCode.OK);
        }
        
        [HttpGet]
        [Route("GetStateList")]
        public async Task<StateDetails> GetStateList()
        {
            StateDetails stateDetails = new StateDetails();
            var body = string.Empty;
            try
            {
                var client = new HttpClient();
                using (var response = await client.GetAsync("https://locus.fkkas.com/api/states"))
                {
                    response.EnsureSuccessStatusCode();
                    body = await response.Content.ReadAsStringAsync();
                    stateDetails = JsonConvert.DeserializeObject<StateDetails>(body);
                }
            }
            catch (Exception ex)
            {

                throw ex;
            }
           
            return stateDetails;
        }

        [HttpGet]
        [Route("GetLGAList")]
        public async Task<LGADetails> GetLGAList(string stateName)
        {
            LGADetails lgaDetails = new LGADetails();
            var body = string.Empty;
            try
            {
                var client = new HttpClient();
                using (var response = await client.GetAsync("https://locus.fkkas.com/api/regions/" + stateName))
                {
                    response.EnsureSuccessStatusCode();
                    body = await response.Content.ReadAsStringAsync();
                    lgaDetails = JsonConvert.DeserializeObject<LGADetails>(body);
                }
            }
            catch (Exception ex)
            {

                throw ex;
            }

            return lgaDetails;
        }
        //Verify OTP for assigned mobile number
        [HttpGet]
        [Route("VerifyOTP")]
        public string VerifyOTP(string OTP)
        {
            string responseMessage = string.Empty;
            try
            {
                var OTPExist = GenerateOTP(6);
                if (OTPExist == OTP)
                {
                    HttpContext.Session.SetString("SessionName", OTP);
                    responseMessage = "Valid OTP";
                }
                else
                {
                    responseMessage = "Invalid OTP";
                }
            }
            catch (Exception ex)
            {

                throw ex;
            }
            return responseMessage;
        }
        // Start OTP Generation function
        protected string GenerateOTP(int length)
        {
            char[] charArr = "0123456789".ToCharArray();
            string strrandom = string.Empty;
            Random objran = new Random();
            for (int i = 0; i < length; i++)
            {
                //It will not allow Repetation of Characters
                int pos = objran.Next(1, charArr.Length);
                if (!strrandom.Contains(charArr.GetValue(pos).ToString())) strrandom += charArr.GetValue(pos);
                else i--;
            }
            return strrandom;
        }
        [HttpGet]
        [Route("GetCurrentGoldPrice")]
        public async Task<decimal> GetCurrentGoldPrice()
        {
            var client = new HttpClient();
            var request = new HttpRequestMessage
            {
                Method = HttpMethod.Get,
                RequestUri = new Uri("https://gold-price-live.p.rapidapi.com/get_metal_prices"),
                Headers =
    {
        { "X-RapidAPI-Host", "gold-price-live.p.rapidapi.com" },
         { "X-RapidAPI-Key", "a98a2556a0msh8b0bd4267b4a0dep14147djsndc2a3e589831" },
    },
            };
            using (var response = await client.SendAsync(request))
            {
                response.EnsureSuccessStatusCode();
                var result = await response.Content.ReadAsStringAsync();
                var priceDetails = JsonConvert.DeserializeObject<GoldPriceModel>(result);
                return priceDetails.Gold;
            }
        }
    }
}
