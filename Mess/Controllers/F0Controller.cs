using FNet.Mess.Models;
using Nskd;
using System;
using System.Web.Mvc;

namespace FNet.Mess.Controllers
{
    public class F0Controller : Controller
    {
        public Object Index(Guid SessionId)
        {
            Object v = "FNet.Mess.Controllers.F0Controller.Index()";
            F0Model m = new F0Model();
            m.Get(SessionId);
            v = PartialView("~/Views/F0/Index.cshtml", m);
            return v;
        }
        public Object ApplyFilter()
        {
            Object v = "FNet.Mess.Controllers.F0Controller.ApplyFilter()";
            RequestPackage rqp = RequestPackage.ParseRequest(Request.InputStream, Request.ContentEncoding);
            F0Model m = new F0Model();
            m.ApplyFilter(rqp);
            v = PartialView("~/Views/F0/Table.cshtml", m);
            return v;
        }
        public Object Save()
        {
            Object v = "FNet.Mess.Controllers.F0Controller.Save()";
            RequestPackage rqp = RequestPackage.ParseRequest(Request.InputStream, Request.ContentEncoding);
            F0Model m = new F0Model();
            m.Save(rqp);
            return v;
        }
    }
}