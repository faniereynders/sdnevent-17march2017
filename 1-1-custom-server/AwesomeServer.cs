using System.IO;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Hosting.Server;
using Microsoft.AspNetCore.Http.Features;
using Microsoft.AspNetCore.Hosting.Server.Features;
using Microsoft.AspNetCore.Hosting.Internal;

namespace WebApplication8
{
    public class AwesomeServer : IServer
    {
        string path;
        public AwesomeServer(string folderToWatch)
        {
            this.path = folderToWatch;
        }
        private FileSystemWatcher watcher = new FileSystemWatcher();
        public IFeatureCollection Features { get; } = new FeatureCollection();

        public void Dispose() { }
        //   private object context;
        public void Start<TContext>(IHttpApplication<TContext> application)
        {
            Features.Set<IHttpRequestFeature>(new HttpRequestFeature());
            Features.Set<IHttpResponseFeature>(new HttpResponseFeature());
            Features.Set<IServerAddressesFeature>(new ServerAddressesFeature());

            var addresses = Features.Get<IServerAddressesFeature>().Addresses;

            watcher.Path = path;
            watcher.EnableRaisingEvents = true;
            watcher.Created += async (sender, e) =>
            {
                var context = (HostingApplication.Context)((dynamic)application.CreateContext(Features));

                context.HttpContext = new AwesomeHttpContext(Features, e.FullPath);

                await application.ProcessRequestAsync((TContext)(dynamic)context);

                context.HttpContext.Response.OnCompleted(null, null);

            };

            Task.Run(() =>
            {
                watcher.WaitForChanged(WatcherChangeTypes.All);

            });

            addresses.Add(watcher.Path);

        }

    }

}
