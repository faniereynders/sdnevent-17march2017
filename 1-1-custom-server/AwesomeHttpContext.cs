using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Http.Features;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.Authentication;
using System.Security.Claims;
using System.Threading;

namespace WebApplication8
{
    public class AwesomeHttpContext : HttpContext
    {
        private readonly IFeatureCollection features;
        private readonly HttpRequest request;
        private readonly HttpResponse response;

        public AwesomeHttpContext(IFeatureCollection features, string path)
        {
            this.features = features;
            this.request = new FileHttpRequest(path);
            this.response = new FileHttpResponse(path);

        }
        public override IFeatureCollection Features => features;

        public override HttpRequest Request => request;

        public override HttpResponse Response => response;

        public override ConnectionInfo Connection => throw new NotImplementedException();

        public override WebSocketManager WebSockets => throw new NotImplementedException();

        public override AuthenticationManager Authentication => throw new NotImplementedException();

        public override ClaimsPrincipal User { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
        public override IDictionary<object, object> Items { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
        public override IServiceProvider RequestServices { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
        public override CancellationToken RequestAborted { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
        public override string TraceIdentifier { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
        public override ISession Session { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }

        public override void Abort()
        {
            throw new NotImplementedException();
        }
    }

}
