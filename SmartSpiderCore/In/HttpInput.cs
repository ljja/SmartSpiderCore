using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.IO;
using System.Net;
using System.Threading;

namespace SmartSpiderCore.In
{
    public class HttpInput : Input
    {
        private CookieContainer _cookieContainer = new CookieContainer();
        private int _cookieClean = 0;

        public string Encoding { get; set; }

        public string ContentType { get; set; }

        public string Host { get; set; }

        public string Method { get; set; }

        public string Referer { get; set; }

        public string Accept { get; set; }

        public string UserAgent { get; set; }

        public int Sleep { get; set; }

        public int CookieClean { get; set; }

        public List<NameValue> Headers { get; set; }

        public UrlEnumerator UrlEnumerator { get; set; }

        public HttpInput()
        {
            Encoding = "utf-8";
            ContentType = "text/html; charset=utf-8";
            Method = "GET";
            Accept = "text/html,application/xhtml+xml,application/xml;q=0.9,image/webp,*/*;q=0.8";
            UserAgent = "Mozilla/5.0 (Windows NT 6.3; WOW64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/35.0.1916.153 Safari/537.36";
            Host = string.Empty;
            Referer = string.Empty;

            Headers = new List<NameValue>
            {
                new NameValue{ Name = "Accept-Language", Value = "zh-CN,zh;q=0.8,en;q=0.6"},
                new NameValue{ Name = "Cache-Control", Value = "max-age=0"}
            };
        }

        public override void Init()
        {

        }

        public override IEnumerator<string> GetEnumerator()
        {
            return UrlEnumerator;
        }

        public override Content GetContent(string uri)
        {
            try
            {
                Thread.Sleep(Sleep);

                Console.Write("{0},", uri);

                if (_cookieClean == 0 || _cookieClean >= CookieClean)
                {
                    try
                    {
                        _cookieContainer = new CookieContainer();
                        _cookieClean = 0;
                        GetRequest(uri, Encoding);
                    }
                    catch (Exception e)
                    {
                        Console.WriteLine(e.Message);
                    }
                }

                _cookieClean++;

                return GetRequest(uri, Encoding);
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);

                return new Content();
            }
        }

        private Content GetRequest(string uri, string encoding = "utf-8")
        {
            var request = (HttpWebRequest)WebRequest.Create(uri);

            request.CookieContainer = _cookieContainer;

            request.Headers.Clear();
            foreach (var m in Headers)
            {
                request.Headers.Add(m.Name, m.Value);
            }

            if (string.IsNullOrEmpty(Method) == false)
            {
                request.Method = Method;
            }

            if (string.IsNullOrEmpty(ContentType) == false)
            {
                request.ContentType = ContentType;
            }

            if (string.IsNullOrEmpty(Host) == false)
            {
                request.Host = Host;
            }

            if (string.IsNullOrEmpty(Referer) == false)
            {
                request.Referer = Referer;
            }

            if (string.IsNullOrEmpty(Accept) == false)
            {
                request.Accept = Accept;
            }

            if (string.IsNullOrEmpty(UserAgent) == false)
            {
                request.UserAgent = UserAgent;
            }

            var response = (HttpWebResponse)request.GetResponse();
            var content = new Content();

            content.Session.Add(new NameValue { Name = "Request.Url", Value = uri });
            content.Session.AddRange(GetNameValueList(request.Headers, "Request"));

            using (var stream = response.GetResponseStream())
            {
                if (stream == null) return content;

                content.Session.AddRange(GetNameValueList(response.Headers, "Response"));
                for (var i = 0; i < response.Cookies.Count; i++)
                {
                    content.Session.Add(new NameValue
                    {
                        Name = string.Format("Cookie.{0}", response.Cookies[i].Name),
                        Value = response.Cookies[i].Value
                    });
                }

                using (var reader = new StreamReader(stream, System.Text.Encoding.GetEncoding(encoding)))
                {
                    Console.WriteLine("{0}", response.StatusCode);

                    content.ContentText = reader.ReadToEnd();

                    return content;
                }
            }
        }

        private IEnumerable<NameValue> GetNameValueList(NameValueCollection header, string format)
        {
            var result = new List<NameValue>();

            for (var i = 0; i < header.Count; i++)
            {
                result.Add(new NameValue
                {
                    Name = string.Format("{0}.{1}", format, header.GetKey(i)),
                    Value = header.Get(i)
                });
            }

            return result;
        }
    }
}
