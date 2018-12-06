using Nskd;
using System;
using System.Data;

namespace FNet.Mess.Models
{
    public class F0Model
    {
        public DataTable Сообщения;
        public class ItemArray
        {
            public String uid;
            public String id;
            public String обработано;
            public String дата;
            public String отправитель;
            public String получатель;
            public String тема;
            public String содержание;
            public String url;
            public String rqp;
            public static ItemArray Create(DataRow dr)
            {
                ItemArray items = new ItemArray
                {
                    uid = dr["uid"].ToString(),
                    id = dr["id"].ToString(),
                    обработано = (dr["обработано"] == DBNull.Value) ? "False" : ((Boolean)dr["обработано"]).ToString(),
                    дата = (dr["дата"] == DBNull.Value) ? "" : ((DateTime)dr["дата"]).ToString("dd.MM.yy"),
                    отправитель = (dr["отправитель"] == DBNull.Value) ? "" : (String)dr["отправитель"],
                    получатель = (dr["получатель"] == DBNull.Value) ? "" : (String)dr["получатель"],
                    тема = (dr["тема"] == DBNull.Value) ? "" : (String)dr["тема"],
                    содержание = (dr["содержание"] == DBNull.Value) ? "" : (String)dr["содержание"],
                    url = (dr["url"] == DBNull.Value) ? "" : (String)dr["url"],
                    rqp = (dr["rqp"] == DBNull.Value) ? "" : (String)dr["rqp"]
                };
                return items;
            }
        }
        public F0Model() { }
        public void Get(Guid sessionId)
        {
            RequestPackage rqp = new RequestPackage()
            {
                SessionId = sessionId,
                Command = "[Utilities].[dbo].[сообщения_получить]",
                Parameters = new RequestParameter[]
                {
                    new RequestParameter() { Name = "session_id", Value = sessionId },
                    new RequestParameter() { Name = "все", Value = false }
                }
            };
            ResponsePackage rsp = rqp.GetResponse("http://127.0.0.1:11012");
            if (rsp != null && rsp.Data != null && rsp.Data.Tables != null && rsp.Data.Tables.Count > 0)
            {
                Сообщения = rsp.Data.Tables[0];
            }
        }
        public void ApplyFilter(RequestPackage rqp)
        {
            rqp.Command = "[Utilities].[dbo].[сообщения_получить]";
            ResponsePackage rsp = rqp.GetResponse("http://127.0.0.1:11012");
            if (rsp != null && rsp.Data != null && rsp.Data.Tables != null && rsp.Data.Tables.Count > 0)
            {
                Сообщения = rsp.Data.Tables[0];
            }
        }
        public void Save(RequestPackage rqp)
        {
            if (rqp != null)
            {
                foreach (RequestParameter p in rqp.Parameters)
                {
                    String name = p.Name;
                    Boolean value = Convert.ToBoolean(p.Value);
                    Guid uid = Guid.Parse(name.Substring(0, 36));
                    String field = name.Substring(36);
                    RequestPackage rqp1 = new RequestPackage
                    {
                        SessionId = rqp.SessionId,
                        Command = "[Utilities].[dbo].[сообщения_сохранить]",
                        Parameters = new RequestParameter[] {
                            new RequestParameter { Name = "session_id", Value = rqp.SessionId },
                            new RequestParameter { Name = "uid", Value = uid },
                            new RequestParameter { Name = field, Value = value }
                        }
                    };
                    ResponsePackage rsp = rqp1.GetResponse("http://127.0.0.1:11012");
                }
            }
        }
    }
}