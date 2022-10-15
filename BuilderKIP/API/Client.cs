using BuilderKIP.Models;
using Microsoft.AspNetCore.Identity;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace BuilderKIP.API
{
    public class Client
    {
        private const string _port = "7003";
        private const string _host = "localhost";

        public enum CRUD
        {
            create,
            get,
            update,
            delete
        }

        public struct Method
        {
            public static string POST = "POST";
            public static string GET = "GET";
            public static string DELETE = "DELETE";
            public static string PUT = "PUT";
        }


        public static string GenerateURI<T>(CRUD CRUD)
        {
            return $"https://{_host}:{_port}/api/easydata/models/__default/sources/{typeof(T).Name}/{CRUD}";
        }

        static string sendRequest(string body, string uri, string method)
        {
            try
            {
                var httpWebRequest = (HttpWebRequest)WebRequest.Create(uri);
                httpWebRequest.ContentType = "application/json; charset=utf-8";
                httpWebRequest.Method = method;

                if (body.Length > 0)
                {
                    using (var streamWriter = new StreamWriter(httpWebRequest.GetRequestStream()))
                    {
                        streamWriter.Write(body);
                        streamWriter.Flush();
                    }
                }


                var httpResponse = (HttpWebResponse)httpWebRequest.GetResponse();
                using (var streamReader = new StreamReader(httpResponse.GetResponseStream()))
                {
                    var result = streamReader.ReadToEnd();
                    return result;
                }
            }
            catch (Exception ee)
            {
                return ee.ToString();
            }
        }


        public static User Authorization(string login, string password)
        {
            string uri = GenerateURI<User>(CRUD.get);
            string json = sendRequest("", uri, Method.POST);
            var listObjs = JsonConvert.DeserializeObject<List<User>>(json);
            var findUser = listObjs.FirstOrDefault(x => x.Login == login);
            if (findUser != null)
            {
                var hasher = new PasswordHasher<User>();
                var s = hasher.VerifyHashedPassword(findUser, findUser.Password, password);
                if (s == PasswordVerificationResult.Success) return findUser;
            }
            
            return null;
        }

        public static List<T> Get<T>()
        {
            try
            {
                string uri = GenerateURI<T>(CRUD.get);
                string json = sendRequest("", uri, Method.POST);
                return JsonConvert.DeserializeObject<List<T>>(json);

            }
            catch(Exception ex)
            {

            } 
            return null;
        }

        public static bool Registration(User user)
        {
            try
            {
                string json = JsonConvert.SerializeObject(user);
                string uri = GenerateURI<User>(CRUD.create);
                var jsonAnswer = sendRequest(json, uri, Method.POST);
                //создание клиента

                dynamic jsonObj = JsonConvert.DeserializeObject(jsonAnswer);
                var newId = jsonObj["record"]["Id"];

                var client = new Models.Client
                {
                    UserId = Convert.ToInt32(newId),
                    Balance = 0
                };
                json = JsonConvert.SerializeObject(client);
                uri = GenerateURI<Client>(CRUD.create);
                sendRequest(json, uri, Method.POST);
                return true;
            }
            catch(Exception ex)
            {

            }
            return false;
        }


    }
}
