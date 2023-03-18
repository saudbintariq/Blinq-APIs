using Newtonsoft.Json;
using RestSharp;
using RestSharp.Authenticators;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Authentication;
using System.Text;
using System.Threading.Tasks;

namespace Blinq_APIs
{
    public class Program
    {
        static string Token = "";
        static void Main(string[] args)
        {
            Token = Program.GetBearerToken();
            //CreateInvoice();
            CreateConsumer();
        }

        public static string GetBearerToken()
        {
            var client = new RestClient("https://staging-api.blinq.pk/");
            var Authenticator = OAuth1Authenticator.ForRequestToken("08NSX3JgfGaSZvw", "woohqKWrOKskujD");

            AuthRequest authRequest = new AuthRequest();

            var json = JsonConvert.SerializeObject(authRequest);

            var request = new RestRequest("https://staging-api.blinq.pk/api/Auth", Method.Post);
            request.AddHeader("content-type", "application/json");
            request.AddParameter("application/json", json, ParameterType.RequestBody);
            var responseJson = client.Execute(request);
            var token = responseJson.Headers.Where(a => a.Name == "Token").FirstOrDefault();
            if (token != null)
                return Convert.ToString(token.Value);
            else
                throw new AuthenticationException("API authentication failed.");
        }

        public static void CreateInvoice()
        {
            InvoiceRequest invoice = new InvoiceRequest
            {
                ConsumerId = "01581",
                InvoiceNumber = "TestApi15",
                InvoiceAmount = 4400,
                InvoiceDueDate = DateTime.Now.ToString("yyyy-MM-dd"),
                ValidityDate = DateTime.Now.ToString("yyyy-MM-dd"),
                InvoiceType = "Service",
                IssueDate = DateTime.Now.ToString("yyyy-MM-dd"),
                CustomerName = "Saim Ali",
                CustomerMobile1 = "03331234567",
                CustomerMobile2 = "03331234567",
                CustomerMobile3 = "03331234567",
                CustomerEmail1 = "test@gmail.com",
                CustomerEmail2 = "test@gmail.com",
                CustomerEmail3 = "test@gmail.com",
                CustomerAddress = null
            };

            List<InvoiceRequest> lst = new List<InvoiceRequest>();
            lst.Add(invoice);

            var json = JsonConvert.SerializeObject(lst);


            var client = new RestClient("https://staging-api.blinq.pk/");
            var request = new RestRequest("https://staging-api.blinq.pk/invoice/create", Method.Post);
            request.RequestFormat = DataFormat.Json;
            request.AddHeader("token", Token);
            request.AddHeader("content-type", "application/json");
            request.AddParameter("application/json", json, ParameterType.RequestBody);
            var responseJson = client.Execute(request);
            InvoiceResponse invoiceResponse = JsonConvert.DeserializeObject<InvoiceResponse>(responseJson.Content);
        }

        public static void CreateConsumer()
        {
            ConsumerRequest consumer = new ConsumerRequest
            {
                ConsumerCode = "01582",
                Name = "Wayne Rooney",
                Mobile1 = "03331234567",
                Mobile2 = "03331234567",
                Mobile3 = "03331234567",
                Email1 = "test@gmail.com",
                Email2 = "test@gmail.com",
                Email3 = "test@gmail.com",
                Address = "Address 101"
            };

            List<ConsumerRequest> lst = new List<ConsumerRequest>();
            lst.Add(consumer);

            var json = JsonConvert.SerializeObject(lst);


            var client = new RestClient("https://staging-api.blinq.pk/");
            var request = new RestRequest("https://staging-api.blinq.pk/consumer/create", Method.Post);
            request.RequestFormat = DataFormat.Json;
            request.AddHeader("token", Token);
            request.AddHeader("content-type", "application/json");
            request.AddParameter("application/json", json, ParameterType.RequestBody);
            var responseJson = client.Execute(request);
            ConsumerResponse myDeserializedClass = JsonConvert.DeserializeObject<ConsumerResponse>(responseJson.Content);
        }
    }

}

public class AuthRequest
{
    public string ClientID { get; set; } = "08NSX3JgfGaSZvw";
    public string ClientSecret { get; set; } = "woohqKWrOKskujD";
}

public class InvoiceRequest
{
    public string ConsumerId { get; set; }
    public string InvoiceNumber { get; set; }
    public decimal InvoiceAmount { get; set; }
    public string InvoiceDueDate { get; set; }
    public string ValidityDate { get; set; }
    public string InvoiceType { get; set; }
    public string IssueDate { get; set; }
    public string CustomerName { get; set; }
    public string CustomerMobile1 { get; set; }
    public string CustomerMobile2 { get; set; }
    public string CustomerMobile3 { get; set; }
    public string CustomerEmail1 { get; set; }
    public string CustomerEmail2 { get; set; }
    public string CustomerEmail3 { get; set; }
    public string CustomerAddress { get; set; }
    public int InvoiceExpireAfterSeconds { get; set; }
}

public class InvoiceResponseDetail
{
    public string FullConsumerCode { get; set; }
    public string TranFee { get; set; }
    public string Description { get; set; }

    [JsonProperty("1BillID")]
    public string _1BillID { get; set; }
    public string InvoiceNumber { get; set; }
    public string PaymentCode { get; set; }
    public string IsFeeApplied { get; set; }
    public string ClickToPayUrl { get; set; }
    public string InvoiceAmount { get; set; }
}

public class InvoiceResponse
{
    public string Message { get; set; }
    public string Status { get; set; }
    public List<InvoiceResponseDetail> ResponseDetail { get; set; }
}

public class ConsumerRequest
{
    public string ConsumerCode { get; set; }
    public string Name { get; set; }
    public string Mobile1 { get; set; }
    public string Mobile2 { get; set; }
    public string Mobile3 { get; set; }
    public string Email1 { get; set; }
    public string Email2 { get; set; }
    public string Email3 { get; set; }
    public string Address { get; set; }

}

public class ConsumerResponseDetail
{
    public string ConsumerCode { get; set; }
    public string FullConsumerCode { get; set; }
    public string Description { get; set; }
}

public class ConsumerResponse
{
    public string Message { get; set; }
    public List<ConsumerResponseDetail> ResponseDetail { get; set; }
    public string Status { get; set; }
}
