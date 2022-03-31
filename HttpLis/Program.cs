using System;
using System.IO;
using System.Net;
using System.Threading;
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

            async void Listener()
            {
                    HttpListenerContext context = await listener.GetContextAsync();
                    HttpListenerRequest request = context.Request;
                    HttpListenerResponse response = context.Response;

                        var staticFilesDirectory = "../../../HTMLPage/";
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
            while (true)
            {
              Listener();
            }
           }
    }
}
