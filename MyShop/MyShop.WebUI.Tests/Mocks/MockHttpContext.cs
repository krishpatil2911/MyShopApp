using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Principal;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace MyShop.WebUI.Tests.Mocks
{
    public class MockHttpContext:HttpContextBase
    {
        private MockResponse response;
        private MockRequest request;
        private HttpCookieCollection cookies;
        private IPrincipal FakeUser;

        public MockHttpContext()
        {
            this.cookies = new HttpCookieCollection();
            this.request = new MockRequest(this.cookies);
            this.response = new MockResponse(this.cookies);
        }

        public override IPrincipal User { get => this.FakeUser; set => this.FakeUser = value; }

        public override HttpRequestBase Request
        {
            get
            {
                return request;
            }
        }

        public override HttpResponseBase Response
        {
            get
            {
                return response;
            }
        }

    }

    public class MockResponse : HttpResponseBase
    {
        private readonly HttpCookieCollection cookies;

        public MockResponse(HttpCookieCollection cookies)
        {
            this.cookies = cookies;
        }

        public override HttpCookieCollection Cookies{
            get
            {
                return cookies;
            }
        }
    }

    public class MockRequest : HttpRequestBase
    {
        private readonly HttpCookieCollection cookies;

        public MockRequest(HttpCookieCollection cookies)
        {
            this.cookies = cookies;
        }

        public override HttpCookieCollection Cookies
        {
            get
            {
                return cookies;
            }
        }
    }

}
