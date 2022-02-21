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


            while (true)
            {
                HttpListenerContext context = listener.GetContext();

                HttpListenerRequest request = context.Request;
                HttpListenerResponse response = context.Response;



              //Console.WriteLine(request.Url);
              // Console.WriteLine(request.Url.PathAndQuery);

                var staticFilesDirectory = "C:/Users/Hp/source/repos/HttpLis/HttpLis/HTMLPage/";
                var file = string.Empty;


                if (request.Url.AbsolutePath == "/")
                {
                    file = "Brovko.html";
                }
                else
                {
                    file = request.Url.AbsolutePath.Trim('/');
                }

                var staticFileToUpload = Path.Combine(staticFilesDirectory, file);

                var ErrorPage = Path.Combine(staticFilesDirectory, "ErrorPage.html");

                var contentToUpload = string.Empty;

                if (File.Exists(staticFileToUpload) && staticFileToUpload.StartsWith(staticFilesDirectory))
                {
                    contentToUpload = File.ReadAllText(staticFileToUpload);
                }
                else
                {
                    response.StatusCode = (int)HttpStatusCode.NotFound;
                    contentToUpload = File.ReadAllText(ErrorPage);
                }


                byte[] bufferhtml = System.Text.Encoding.UTF8.GetBytes(contentToUpload);

                response.ContentLength64 = bufferhtml.Length;
                Stream output = response.OutputStream;
                output.Write(bufferhtml, 0, bufferhtml.Length);
                output.Close();

            }

        }
    }
}
