
using OpenQA.Selenium;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Text;
using Titanium.Web.Proxy;

namespace HCrawler.CrawlSites
{
    public class TitaniumClass
    {
        //private IWebDriver _driver;
        //private ProxyServer _proxyServer;
        //private readonly IDictionary<Guid, Proxy.Request> _requestsHistory =
        //    new ConcurrentDictionary<Guid, Proxy.Request>();
        //private readonly IDictionary<Guid, Proxy.Response> _responsesHistory =
        //    new ConcurrentDictionary<Guid, Proxy.Response>();
        //[OneTimeSetUp]
        //public void ClassInit()
        //{
        //    _proxyServer = new ProxyServer
        //    {
        //        TrustRootCertificate = true
        //    };
        //    var explicitEndPoint = new ExplicitProxyEndPoint(System.Net.IPAddress.Any, 18882, true);
        //    _proxyServer.AddEndPoint(explicitEndPoint);
        //    _proxyServer.Start();
        //    _proxyServer.SetAsSystemHttpProxy(explicitEndPoint);
        //    _proxyServer.SetAsSystemHttpsProxy(explicitEndPoint);
        //    _proxyServer.BeforeRequest += OnRequestCaptureTrafficEventHandler;
        //    _proxyServer.BeforeResponse += OnResponseCaptureTrafficEventHandler;
        //}
        //[OneTimeTearDown]
        //public void ClassCleanup()
        //{
        //    _proxyServer.Stop();
        //}
        //[SetUp]
        //public void TestInit()
        //{
        //    var proxy = new OpenQA.Selenium.Proxy
        //    {
        //        HttpProxy = "http://localhost:18882",
        //        SslProxy = "http://localhost:18882",
        //        FtpProxy = "http://localhost:18882"
        //    };
        //    var options = new ChromeOptions
        //    {
        //        Proxy = proxy
        //    };
        //    _driver = new ChromeDriver(options);
        //}
        //[TearDown]
        //public void TestCleanup()
        //{
        //    _driver.Dispose();
        //    _requestsHistory.Clear();
        //    _responsesHistory.Clear();
        //}
    }
}
