using ShipwayUnitOfWork.Models;
using System;
using System.Web;

namespace Shipway.Cookies
{
    public class UserCookie
    {
        public static string USERCOOKIE_ID = "user_id";
        public static string USERCOOKIE_USERNAME = "user_username";
        public static string USERCOOKIE_TYPEUSER = "user_typeid";
        public static string USERCOOKIE_AGENCY = "user_agencyid";

        public static void SetDataCookie(Users model)
        {
            var time = DateTime.Now.AddDays(1);
            HttpCookie cookieId = new HttpCookie(USERCOOKIE_ID, model.Id.ToString());
            HttpCookie cookieUsername = new HttpCookie(USERCOOKIE_USERNAME, model.UserName);
            HttpCookie cookieTypeUser = new HttpCookie(USERCOOKIE_TYPEUSER, model.TypeId.ToString());

            cookieId.Expires = time;
            cookieUsername.Expires = time;
            cookieTypeUser.Expires = time;

            HttpContext.Current.Response.Cookies.Add(cookieId);
            HttpContext.Current.Response.Cookies.Add(cookieUsername);
            HttpContext.Current.Response.Cookies.Add(cookieTypeUser);

            // agency
            //if (model.Agency != null)
            //{
            //    HttpCookie cookieAgency = new HttpCookie(USERCOOKIE_AGENCY, model.AgencyId.ToString());
            //    cookieAgency.Expires = time;
            //    HttpContext.Current.Response.Cookies.Add(cookieAgency);
            //}

        }

        public static Users GetDataCookie()
        {
            HttpCookie cookieId = HttpContext.Current.Request.Cookies[USERCOOKIE_ID];
            HttpCookie cookieUsername = HttpContext.Current.Request.Cookies[USERCOOKIE_USERNAME];
            HttpCookie cookieTypeUser = HttpContext.Current.Request.Cookies[USERCOOKIE_TYPEUSER];

            if (cookieId != null)
            {
                Users user = new Users()
                {
                    Id = int.Parse(cookieId.Value),
                    UserName = cookieUsername.Value,
                    TypeId = int.Parse(cookieTypeUser.Value)
                };
                return user;
            }
            return null;
        }

        public static void RemoveDataCookie()
        {
            var time = DateTime.Now.AddDays(-1);
            HttpCookie cookieId = new HttpCookie(USERCOOKIE_ID);
            HttpCookie cookieUsername = new HttpCookie(USERCOOKIE_USERNAME);
            HttpCookie cookieTypeUser = new HttpCookie(USERCOOKIE_TYPEUSER);

            cookieId.Expires = time;
            cookieUsername.Expires = time;
            cookieTypeUser.Expires = time;

            HttpContext.Current.Response.Cookies.Add(cookieId);
            HttpContext.Current.Response.Cookies.Add(cookieUsername);
            HttpContext.Current.Response.Cookies.Add(cookieTypeUser);
        }
    }
}