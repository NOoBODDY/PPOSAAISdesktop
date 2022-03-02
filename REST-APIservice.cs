using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json;
using System.Threading.Tasks;
using RestSharp;
using RestSharp.Authenticators;

using ClientDB.Model;
using RestSharp.Authenticators.OAuth2;

namespace ClientDB
{
    public class REST_APIservice
    {

        public event EventHandler<string> BadRequest;

        //192.168.43.118
        //80.87.192.94:8080
        static string baseURL = "http://192.168.43.118:8080";
        string username;
        string password;
        string access_token;
        public List<Filter> Filters { get; set; }
        RestClient client;

        public REST_APIservice(string username, string password)
        {
            this.username = username;
            this.password = password;
            Filters = new List<Filter>();
            this.client = new RestClient(baseURL);
        }

        public async Task LogInAsync()
        {
            string loginUrl = "api/login";
            RestRequest request = new RestRequest(loginUrl, Method.Post);
            request.AddParameter("username", username);
            request.AddParameter("password", password);
            var response = await client.PostAsync(request);
            if (response ==null || response.StatusCode != System.Net.HttpStatusCode.OK)
            {
                BadRequest?.Invoke(this, response?.StatusCode + " " + response?.ErrorMessage + " " + response?.Content);
            }
            else
            {
                access_token = response.Headers.First(u => u.Name == "access_token").Value.ToString();
                client.Authenticator = new OAuth2AuthorizationRequestHeaderAuthenticator(access_token,"Bearer");
            }
        }

        public async Task<Account> GetAccountAsync()
        {
            string Url = "api/user/current";
            RestRequest request = new RestRequest(Url);
            //request.AddHeader("Authorization", access_token);
            var response = await client.GetAsync(request);
            if (response == null || response.StatusCode != System.Net.HttpStatusCode.OK)
            {
                BadRequest?.Invoke(this, response?.StatusCode + " " + response?.ErrorMessage + " " + response?.Content);
                return default;
            }
            else
            {
                return JsonSerializer.Deserialize<Account>(response.Content);
            }

        }

        #region Students List
        public async Task<int> GetStudentsCountAsync()
        {
            string Url = "api/student/amount";
            RestRequest request = new RestRequest(Url, Method.Patch);
            request.AddJsonBody<List<Filter>>(Filters);
            var response = await client.PatchAsync(request);
            if (response == null || response.StatusCode != System.Net.HttpStatusCode.OK)
            {
                BadRequest?.Invoke(this, response?.StatusCode + " " + response?.ErrorMessage + " " + response?.Content);
                return 0;
            }
            else
            {
                var dict = JsonSerializer.Deserialize<Dictionary<string,int>>(response.Content);
                return Convert.ToInt32(dict["amount"]);
            }
        }

        public async Task<Student[]> GetPageOfStudentsAsync(int startIndex, int endIndex)
        {
            string Url = "/api/student/getAll";
            RestRequest request = new RestRequest(Url, Method.Patch);
            StudentPage page = new StudentPage();
            page.startIndex = startIndex;
            page.endIndex = endIndex;
            page.Filters = this.Filters;
            request.AddJsonBody<StudentPage>(page);
            var response = await client.PatchAsync(request);
            if (response == null || response.StatusCode != System.Net.HttpStatusCode.OK)
            {
                BadRequest?.Invoke(this, response?.StatusCode + " " + response?.ErrorMessage + " " + response?.Content);
                return default;
            }
            else
            {
                Student[] students = JsonSerializer.Deserialize<Student[]>(response.Content);
                return students;
            }
        }

        #endregion

        #region Add

        public async Task<string> AddStudentAsync(Student student)
        {
            string url = "/api/student/add";
            RestRequest request = new RestRequest(url, Method.Post);
            request.AddJsonBody<Student>(student);
            var response = await client.PostAsync(request);
            if (response == null || response.StatusCode != System.Net.HttpStatusCode.OK)
            {
                BadRequest?.Invoke(this, response?.StatusCode + " " + response?.ErrorMessage + " " + response?.Content);
            }
            return response?.StatusCode.ToString();
        }
        //for many
        public async Task<string> AddPaymentAsync(Student[] students)
        {
            string url = "api/student/addPayment";
            RestRequest request = new RestRequest(url, Method.Post);
            List<PaymentSemester> payments = new List<PaymentSemester>();
            foreach (Student student in students)
            {
                foreach(Ticketextension ticketextension in student.ticketExtensions)
                {
                    PaymentSemester payment = new PaymentSemester();
                    payment = PaymentSemester.FromSemester(ticketextension.semester);
                    payment.studentId = student.id;
                    payments.Add(payment);
                }
            }
            request.AddJsonBody<PaymentSemester[]>(payments.ToArray());
            var response = await client.PostAsync(request);
            if (response == null || response.StatusCode != System.Net.HttpStatusCode.OK)
            {
                BadRequest?.Invoke(this, response?.StatusCode + " " + response?.ErrorMessage + " " + response?.Content);
            }
            return response?.StatusCode.ToString();

        }
        //for one
        public async Task<string> AddPaymentAsync(Student student)
        {
            string url = "api/student/addPayment";
            RestRequest request = new RestRequest(url, Method.Post);
            List<PaymentSemester> payments = new List<PaymentSemester>();

            foreach (Ticketextension ticketextension in student.ticketExtensions)
            {
                PaymentSemester payment = new PaymentSemester();
                payment = PaymentSemester.FromSemester(ticketextension.semester);
                payment.studentId = student.id;
                payments.Add(payment);
            }
            request.AddJsonBody<PaymentSemester[]>(payments.ToArray());
            var response = await client.PatchAsync(request);
            if (response == null || response.StatusCode != System.Net.HttpStatusCode.OK)
            {
                BadRequest?.Invoke(this, response?.StatusCode + " " + response?.ErrorMessage + " " + response?.Content);
            }
            return response?.StatusCode.ToString();
        }





        public async Task<string> AddUserAsync(Account newUser)
        {
            string url = "api/user/add";
            RestRequest request = new RestRequest(url, Method.Post);
            request.AddJsonBody<Account>(newUser);
            var response = await client.PostAsync(request);
            if (response == null || response.StatusCode != System.Net.HttpStatusCode.OK)
            {
                BadRequest?.Invoke(this, response?.StatusCode + " " + response?.ErrorMessage + " " + response?.Content);
            }
            return response?.StatusCode.ToString();
        }
        #endregion

        #region Student update
        public async Task<string> UpdateStudentAsync(Student student)
        {
            string url = "api/student/update";
            RestRequest request = new RestRequest(url, Method.Post);
            request.AddJsonBody<Student>(student);
            var response = await client.PostAsync(request);
            if (response == null || response.StatusCode != System.Net.HttpStatusCode.OK)
            {
                BadRequest?.Invoke(this, response?.StatusCode + " " + response?.ErrorMessage + " " + response?.Content);
            }
            return response?.StatusCode.ToString();
        }





        #endregion
    }

    public class StudentPage
    {
        public int startIndex { get; set; }
        public int endIndex { get; set; }
        public List<Filter> Filters { get; set;}
    }

}
