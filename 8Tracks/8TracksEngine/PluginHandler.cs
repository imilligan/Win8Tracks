using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace _8TracksEngine
{
    public class PlugInHandler : MessageProcessingHandler
    {
        private string m_userToken;

        public PlugInHandler(HttpMessageHandler innerHandler, string userToken = null)
            : base(innerHandler)
        {
            m_userToken = userToken;
        }

        // Process the request before sending it
        protected override HttpRequestMessage ProcessRequest(HttpRequestMessage request, CancellationToken cancellationToken)
        {
            if (request.Method == HttpMethod.Get || request.Method == HttpMethod.Post || request.Method == HttpMethod.Put)
            {
                request.Headers.Add("X-Api-Key", Constants.API_KEY);
                request.Headers.Add("X-Api-Version", Constants.API_VERSION);
                if( m_userToken != null ){
                    request.Headers.Add("X-User-Token", m_userToken);
                }

            }
            return request;
        }

        // Process the response before returning it to the user
        protected override HttpResponseMessage ProcessResponse(HttpResponseMessage response, CancellationToken cancellationToken)
        {
            if (response.RequestMessage.Method == HttpMethod.Get)
            {
                response.Headers.Add("Custom-Header", "CustomResponseValue");
            }
            return response;
        }
    }
}
