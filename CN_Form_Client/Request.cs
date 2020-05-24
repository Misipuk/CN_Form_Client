using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net;
using System.Text;
using System.Net.Http;
using System.Net.Sockets;
using System.Text.RegularExpressions;
using Newtonsoft.Json;

namespace CN_Form_Client
{
    class Request
    {
        static Socket connection()
        {
            string hostName = "127.0.0.1";
            int hostPort = 9090;
            //int response = 0;

            IPAddress host = IPAddress.Parse(hostName);
            IPEndPoint hostep = new IPEndPoint(host, hostPort);
            Socket socket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
            socket.Connect(hostep);
            return socket;
        }

        public string[] loginUser(String login, String password)
        {
            Socket socket = connection();
            Dictionary<string, string> userLogin = new Dictionary<string, string>();
            userLogin.Add("login", login);
            userLogin.Add("password", password);
            string json = JsonConvert.SerializeObject(userLogin);
            int cont_length = json.Length;


            string request_url = String.Format("POST /login HTTP/1.1 \n" +
            "Host: MyServer\n" +
            "Content-Length: {0}\n" +
            "\n", cont_length);
            request_url = request_url + json + "\n";


            //Console.WriteLine(json);
            Console.WriteLine(request_url);
            int response = socket.Send(Encoding.UTF8.GetBytes(request_url));
            return ResponseParser.parseLogin(socket);
        }



        public string[] registerUser(String login, String password)
        {
            Socket socket = connection();
            Dictionary<string, string> userLogin = new Dictionary<string, string>();
            userLogin.Add("login", login);
            userLogin.Add("password", password);
            string json = JsonConvert.SerializeObject(userLogin);
            int cont_length = json.Length;


            string request_url = String.Format("POST /users HTTP/1.1 \n" +
            "Host: MyServer\n" +
            "Content-Length: {0}\n" +
            "\n", cont_length);
            request_url = request_url + json + "\n";


            //Console.WriteLine(json);
            Console.WriteLine(request_url);
            int response = socket.Send(Encoding.UTF8.GetBytes(request_url));
            return ResponseParser.parseResReg(socket);
        }

        public string[] getCafes(String token)
        {
            Socket socket = connection();


            string request_url = String.Format("GET /cafes HTTP/1.1 \n" +
            "Host: MyServer\n" +
            "Accept: application/json\n" +
            "Authorization: {0}\n" +
            "\n", token);
            //Console.WriteLine(json);
            Console.WriteLine(request_url);
            int response = socket.Send(Encoding.UTF8.GetBytes(request_url));
            return ResponseParser.parseRes(socket);
        }

        public string[] getRevCafe(String token, int id)
        {
            Socket socket = connection();


            string request_url = String.Format("GET /cafe/review?cafe_id={0} HTTP/1.1 \n" +
            "Host: MyServer\n" +
            "Accept: application/json\n" +
            "Authorization: {1}\n" +
            "\n", id, token);
            //Console.WriteLine(json);
            Console.WriteLine(request_url);
            int response = socket.Send(Encoding.UTF8.GetBytes(request_url));
            return ResponseParser.parseRes(socket);
        }

        public string[] addCafe(String token, String name, String des, String city)
        {
            Socket socket = connection();
            Dictionary<string, string> newCafe = new Dictionary<string, string>();
            newCafe.Add("owner", "test");
            newCafe.Add("name", name);
            newCafe.Add("des", des);
            newCafe.Add("city", city);
            string json = JsonConvert.SerializeObject(newCafe);
            int cont_length = json.Length;


            string request_url = String.Format("POST /cafe HTTP/1.1 \n" +
            "Host: MyServer\n" +
            "Accept: application/json\n" +
            "Content-Length: {0}\n" +
            "Authorization: {1}\n" +
            "\n", cont_length, token);
            request_url = request_url + json + "\n";


            //Console.WriteLine(json);
            Console.WriteLine(request_url);
            int response = socket.Send(Encoding.UTF8.GetBytes(request_url));
            return ResponseParser.parseResReg(socket);
        }

        public string[] addRevCafe(String token, Review r)
        {
            Socket socket = connection();
            Dictionary<string, string> newReview = new Dictionary<string, string>();
            newReview.Add("owner", "test");
            newReview.Add("cafe_id", r.cafe_id.ToString());
            newReview.Add("stars", r.stars.ToString());
            newReview.Add("description", r.description);
            string json = JsonConvert.SerializeObject(newReview);
            int cont_length = json.Length;


            string request_url = String.Format("POST /cafe/review HTTP/1.1 \n" +
            "Host: MyServer\n" +
            "Accept: application/json\n" +
            "Content-Length: {0}\n" +
            "Authorization: {1}\n" +
            "\n", cont_length, token);
            request_url = request_url + json + "\n";


            //Console.WriteLine(json);
            Console.WriteLine(request_url);
            int response = socket.Send(Encoding.UTF8.GetBytes(request_url));
            return ResponseParser.parseResReg(socket);
        }

    }
}

