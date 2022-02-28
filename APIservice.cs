using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Net;
using ClientDB.Model;
using Newtonsoft.Json.Linq;
using Newtonsoft.Json;
using System.Windows;

namespace ClientDB
{
    public class APIservice
    {
        #region Notify
        public delegate void APIHandler(string message);
        public event APIHandler ExeptionNotify;

        #endregion



        static string baseURL = "http://80.87.192.94:8080";
        string username;
        string password;
        string access_token;
        public List<Filter> Filters { get; set; }

        public APIservice(string username, string password)
        {
            this.username = username;
            this.password = password;
            Filters = new List<Filter>();
        }
        
        public string LogIn()
        {
            string loginUrl = "/api/login";
            HttpWebRequest request = (HttpWebRequest)HttpWebRequest.Create(baseURL+loginUrl);
            request.Method = "POST";
            request.ContentType = "application/x-www-form-urlencoded";
            string payload = String.Format("username={0}&password={1}", username, password);
            request.GetRequestStream().Write(Encoding.UTF8.GetBytes(payload), 0, payload.Length);
            try
            {
                HttpWebResponse response = request.GetResponse() as HttpWebResponse;
                access_token = response.Headers["access_token"];
                response.Close();
                return response.StatusCode.ToString();
            }
            catch(Exception ex)
            {
                ExeptionNotify?.Invoke(ex.Message);
                return "NOTOK";
            }
        }

        public Account GetAccount()
        {
            try
            {
                string Url = "/api/user/current";
                HttpWebRequest request = (HttpWebRequest)HttpWebRequest.Create(baseURL + Url);
                request.Method = "GET";
                request.Headers.Add("Authorization", access_token);

                HttpWebResponse response = request.GetResponse() as HttpWebResponse;

                string json;
                using (var stream = response.GetResponseStream())
                {
                    using (var reader = new StreamReader(stream))
                    {
                        json = reader.ReadToEnd();
                    }
                }
                response.Close();

                Account account = JsonConvert.DeserializeObject<Account>(json);
                return account;
            }
            catch(Exception ex)
            {
                ExeptionNotify?.Invoke(ex.Message);
                return default;
            }
            
            
        }

        #region Students List
        public int GetStudentsCount()

        {
            try
            {
                string Url = "/api/student/amount";
                HttpWebRequest request = (HttpWebRequest)HttpWebRequest.Create(baseURL + Url);
                request.Method = "PATCH";
                request.Headers.Add("Authorization", access_token);
                if (Filters.Count() != 0)
                {
                    string payload = JArray.FromObject(Filters).ToString();
                    request.GetRequestStream().Write(Encoding.UTF8.GetBytes(payload), 0, payload.Length);
                }
                HttpWebResponse response = request.GetResponse() as HttpWebResponse;
                string json;
                using (var stream = response.GetResponseStream())
                {
                    using (var reader = new StreamReader(stream))
                    {
                        json = reader.ReadToEnd();
                    }
                }
                JObject jObject = JObject.Parse(json);
                response.Close();


                return Convert.ToInt32(jObject.GetValue("amount").ToString());
            }
            catch(Exception ex)
            {
                ExeptionNotify?.Invoke(ex.Message);
                return default;
            }
        }

        public List<Student> GetPageOfStudents(int startIndex, int endIndex)
        {
            try
            {
                JObject json = new JObject();
                JArray filter = JArray.FromObject(Filters);
                json.Add("filters", filter);
                json.Add("startIndex", startIndex);
                json.Add("endIndex", endIndex);

                string Url = "/api/student/getAll";
                HttpWebRequest request = (HttpWebRequest)HttpWebRequest.Create(baseURL + Url);
                request.Method = "PATCH";
                request.ContentType = "application/json";
                request.Headers.Add("Authorization", access_token);
                string payload = json.ToString();
                request.GetRequestStream().Write(Encoding.UTF8.GetBytes(payload), 0, payload.Length);
                HttpWebResponse response = request.GetResponse() as HttpWebResponse;
                string responseJson;
                using (var stream = response.GetResponseStream())
                {
                    using (var reader = new StreamReader(stream))
                    {
                        responseJson = reader.ReadToEnd();
                    }
                }
                List<Student> students = JsonConvert.DeserializeObject<List<Student>>(responseJson);
                response.Close();
                return students;
            }
            catch (Exception ex)
            {
                ExeptionNotify?.Invoke(ex.Message);
                return default;
            }
            


            
        }

        #endregion

        #region Add

        public void AddStudent(Student student)
        {
            string url = "/api/student/add";
            string payload = JObject.FromObject(student).ToString();
            
            HttpWebRequest request = (HttpWebRequest)HttpWebRequest.Create(baseURL + url);
            request.Method = "POST";
            request.ContentType = "application/json";
            request.Headers.Add("Authorization", access_token);
            request.GetRequestStream().Write(Encoding.UTF8.GetBytes(payload), 0, payload.Length);
            HttpWebResponse response = null;
            try 
            {
                 response= request.GetResponse() as HttpWebResponse;
                 response.Close();
            }
            catch(WebException e)
            {
                using (WebResponse badresponse = e.Response)
                {
                    HttpWebResponse httpResponse = (HttpWebResponse)badresponse;
                    using (Stream data = badresponse.GetResponseStream())
                    using (var reader = new StreamReader(data))
                    {
                        string text = reader.ReadToEnd();
                        ExeptionNotify?.Invoke(String.Format("Error code: {0}\nBODY: {1}", httpResponse.StatusCode, text));
                    }
                }
            }
        }

        public void AddPayment (Student[] students)
        {
            try
            {
                string url = "/api/student/addPayment";
                List<PaymentSemester> payments = new List<PaymentSemester>();
                foreach (Student student in students)
                {
                    foreach (Ticketextension ticketextension in student.ticketExtensions)
                    {
                        PaymentSemester payment = new PaymentSemester();
                        payment = PaymentSemester.FromSemester(ticketextension.semester);
                        payment.studentId = student.id;
                        payments.Add(payment);
                    }
                    
                }
                string payload = JArray.FromObject(payments).ToString();

                HttpWebRequest request = (HttpWebRequest)HttpWebRequest.Create(baseURL + url);
                request.Method = "POST";
                request.ContentType = "application/json";
                request.Headers.Add("Authorization", access_token);
                request.GetRequestStream().Write(Encoding.UTF8.GetBytes(payload), 0, payload.Length);
                HttpWebResponse response = request.GetResponse() as HttpWebResponse;
                response.Close();
            }
            catch (Exception ex)
            {
                ExeptionNotify?.Invoke(ex.Message);
            }
        }

        public void AddUser(Account newUser)
        {
            try
            {
                string url = "/api/user/add";
                string payload = JObject.FromObject(newUser).ToString();

                HttpWebRequest request = (HttpWebRequest)HttpWebRequest.Create(baseURL + url);
                request.Method = "POST";
                request.ContentType = "application/json";
                request.Headers.Add("Authorization", access_token);
                request.GetRequestStream().Write(Encoding.UTF8.GetBytes(payload), 0, payload.Length);
                HttpWebResponse response = request.GetResponse() as HttpWebResponse;
                response.Close();
            }
            catch(Exception ex)
            {
                ExeptionNotify?.Invoke(ex.Message);
            }
        }
        #endregion

        #region Student update
        public void UpdateStudent(Student student)
        {
            string url = "/api/student/update";
            string payload = JObject.FromObject(student).ToString();

            HttpWebRequest request = (HttpWebRequest)HttpWebRequest.Create(baseURL + url);
            request.Method = "POST";
            request.ContentType = "application/json";
            request.Headers.Add("Authorization", access_token);
            request.GetRequestStream().Write(Encoding.UTF8.GetBytes(payload), 0, payload.Length);
            HttpWebResponse response = null;
            try
            {
                response = request.GetResponse() as HttpWebResponse;
                response.Close();
            }
            catch (WebException e)
            {
                using (WebResponse badresponse = e.Response)
                {
                    HttpWebResponse httpResponse = (HttpWebResponse)badresponse;
                    using (Stream data = badresponse.GetResponseStream())
                    using (var reader = new StreamReader(data))
                    {
                        string text = reader.ReadToEnd();
                        ExeptionNotify?.Invoke(String.Format("Error code: {0}\nBODY: {1}", httpResponse.StatusCode, text));
                    }
                }
            }
        }
        #endregion

    }
}
