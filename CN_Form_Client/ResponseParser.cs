using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Text;
using System.Net.Http;
using System.Net.Sockets;
using System.Text.RegularExpressions;
using Newtonsoft.Json;

namespace CN_Form_Client
{
    class ResponseParser
    {
        public static string[] parseLogin(Socket socket)
        {
            // bool flag = true; // just so we know we are still reading
            string headerString = ""; // to store header information
                                      //int contentLength = 0; // the body length
                                      // byte[] bodyBuff = new byte[0]; // to later hold the body content
                                      // read the header byte by byte, until \r\n\r\n
            byte[] buffer = new byte[1024];
            socket.Receive(buffer, 0, 1024, 0);
            headerString += Encoding.ASCII.GetString(buffer);
            string[] answer = headerString.Split('\n');
            string[] parseAnswer = new string[2];
            for (int i = 0; i < 2; i++)
            {
                parseAnswer[i] = answer[i].Trim('\r', '\n', ' ');

            }
            if (parseAnswer[0].Contains("OK"))
            {
                parseAnswer[1] = parseAnswer[1].Substring(15);
            }
                
            
            socket.Close();
            return parseAnswer;
        }
        public static string[] parseResReg(Socket socket)
        {
            // bool flag = true; // just so we know we are still reading
            string headerString = ""; // to store header information
                                      //int contentLength = 0; // the body length
                                      // byte[] bodyBuff = new byte[0]; // to later hold the body content
                                      // read the header byte by byte, until \r\n\r\n
            byte[] buffer = new byte[1024];
            socket.Receive(buffer, 0, 1024, 0);
            headerString += Encoding.ASCII.GetString(buffer);
            string[] sep = new string[] { "\r\n\r\n" };
            string[] answer = headerString.Split(sep, System.StringSplitOptions.RemoveEmptyEntries);
            string[] parseAnswer = new string[2];
            if (answer.Length >= 2)
            {
                for (int i = 0; i < 2; i++)
                {
                    parseAnswer[i] = answer[i].Trim('\r', '\n', ' ');
                    //Console.WriteLine(i);
                    Console.WriteLine(parseAnswer[i]);

                }
            }
           


            socket.Close();
            return parseAnswer;
        }

        public static string[] parseRes(Socket socket)
        {
            bool flag = true; // just so we know we are still reading
            string headerString = ""; // to store header information
            int contentLength = 0; // the body length
            byte[] bodyBuff = new byte[0]; // to later hold the body content
            while (flag)
            {
                // read the header byte by byte, until \r\n\r\n
                byte[] buffer = new byte[1];
                socket.Receive(buffer, 0, 1, 0);
                headerString += Encoding.ASCII.GetString(buffer);
                if (headerString.Contains("\r\n\r\n"))
                {
                    // header is received, parsing content length
                    // I use regular expressions, but any other method you can think of is ok
                    Regex reg = new Regex("\r\nContent-Length: (.*?)\r\n\r\n");
                    Match m = reg.Match(headerString);
                    contentLength = int.Parse(m.Groups[1].ToString());
                    flag = false;
                    // read the body
                    bodyBuff = new byte[contentLength];
                    socket.Receive(bodyBuff, 0, contentLength, 0);
                }
            }
            // Console.WriteLine("Server Response :");
            // Console.WriteLine(headerString);
            string body = Encoding.ASCII.GetString(bodyBuff);
            //Console.WriteLine(body);
            socket.Close();

            string[] sep = new string[] { "\r\n" };
            string[] answer = headerString.Split(sep, System.StringSplitOptions.RemoveEmptyEntries);
            string[] parseAnswer = new string[2];
            parseAnswer[0] = answer[0];
            parseAnswer[1] = body;
            //Console.WriteLine(parseAnswer[0]);
            // Console.WriteLine("\n");
            // Console.WriteLine(parseAnswer[1]);
            return parseAnswer;
        }
    }
}
