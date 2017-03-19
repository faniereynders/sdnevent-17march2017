using System;
using System.IO;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;

namespace WebApplication8
{
    public class FileHttpResponse : HttpResponse
    {
        string path;
        HeaderDictionary headers;
        Stream stream;
        public FileHttpResponse(string path)
        {
            this.path = path;
            this.headers = new HeaderDictionary();
            stream = new MemoryStream();


            // Body = new FileStream(path, FileMode.OpenOrCreate);
        }
        public override HttpContext HttpContext => throw new NotImplementedException();

        public override int StatusCode { get; set; }

        public override IHeaderDictionary Headers => headers;

        public override Stream Body
        {
            get { return stream; }
            set { stream = value; }
        }

        public override long? ContentLength { get; set; }
        public override string ContentType { get; set; }

        public override IResponseCookies Cookies => throw new NotImplementedException();

        public override bool HasStarted => true;

        public override void OnCompleted(Func<object, Task> callback, object state)
        {
            using (var reader = new StreamReader(stream))
            {
                stream.Position = 0;
                var text = reader.ReadToEnd();
                File.AppendAllText(path, $"\r\n\r\n--\r\n{StatusCode}\r\n{text}");
                stream.Flush();
                stream.Dispose();
            }


            //Body.Flush();
            //throw new NotImplementedException();
        }

        public override void OnStarting(Func<object, Task> callback, object state)
        {
            //  throw new NotImplementedException();
        }

        public override void Redirect(string location, bool permanent)
        {
            // throw new NotImplementedException();
        }
    }

}
