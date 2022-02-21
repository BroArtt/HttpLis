using System;
using System.IO;
using System.Net;
using System.Threading.Tasks;

namespace HttpListenerBrovko
{
    internal class Program
    {
        static void Main(string[] args)
        {
            HttpListener listener = new HttpListener();
            listener.Prefixes.Add("http://localhost:8888/");
            listener.Start();
            Console.WriteLine("Ожидание подключений...");


            HttpListenerContext context = listener.GetContext();
            HttpListenerRequest request = context.Request;
            HttpListenerResponse response = context.Response;

            string htmlfile = File.ReadAllText("C:/Users/Hp/Source/Repos/Httplistener/HTMLPage/Brovko.html");

            string responseString = htmlfile;
            byte[] bufferhtml = System.Text.Encoding.UTF8.GetBytes(responseString);


            response.ContentLength64 = bufferhtml.Length;
            Stream output = response.OutputStream;
            output.Write(bufferhtml, 0, bufferhtml.Length);
            output.Close();

        }
    }
}
