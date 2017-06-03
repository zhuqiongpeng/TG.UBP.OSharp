using OSharp.Utility.Data;
using OSharp.Web.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

using OSharp.Web.Mvc.Security;
using TG.UBP.Utility;
using TG.UBP.Utility.Config;
using TG.UBP.Web.Utility;
using System.Threading.Tasks;
using OSharp.Web.Mvc.UI;

namespace TG.UBP.Web.Controllers
{
    public class LoginController : BaseController
    {
        public ISiteConfigProvider SiteConfigProvider { get; set; }

        // GET: Login
        public ActionResult Index()
        {
            SiteConfig siteConfig = SiteConfigProvider.LoadConfig(Utils.GetXmlMapPath("Configpath"));

            //系统名称
            ViewBag.WebName = siteConfig.webname;
            //公司名称
            ViewBag.ComName = siteConfig.webcompany;
            //CopyRight
            ViewBag.CopyRight = siteConfig.webcopyright;

            return View();
        }

        /// <summary>
        /// 生成验证码
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public ActionResult VerifyCode()
        {
            return File(new VerifyCode().GetVerifyCode(), @"image/Gif");
        }

        [HttpPost]
        public async Task<ActionResult> Login(string UserName, string Password, string Code)
        {
            if (Session[OSharp.Core.Constants.VerifyCodeSession] == null)
                return Json(new OperationResult(OperationResultType.ValidError, "请重新刷新验证码").ToAjaxResult(), JsonRequestBehavior.AllowGet);

            if (!SecurityHelper.CheckVerify(Code))
                return Json(new OperationResult(OperationResultType.ValidError, "验证码错误").ToAjaxResult(), JsonRequestBehavior.AllowGet);

            return Json(new OperationResult(OperationResultType.Success, Session[OSharp.Core.Constants.VerifyCodeSession].ToString()).ToAjaxResult(), JsonRequestBehavior.AllowGet);

            //SysUser user = accountBLL.Login(UserName, ValueConvert.MD5(Password));
            //if (user == null)
            //{
            //    LogHandler.WriteServiceLog(UserName, ResultHelper.NowTime + "登录系统,IP:" + ResultHelper.GetUserIP() + "账户或密码错误", "失败", "登录", "系统入口");
            //    return Json(JsonHandler.CreateMessage(0, "用户名或密码错误"), JsonRequestBehavior.AllowGet);
            //}
            //else if (!Convert.ToBoolean(user.State))//被禁用
            //{
            //    return Json(JsonHandler.CreateMessage(0, "账户被系统禁用"), JsonRequestBehavior.AllowGet);
            //}

            //AccountModel account = new AccountModel();
            //account.Id = user.Id;
            //account.TrueName = user.TrueName;
            //account.Photo = string.IsNullOrEmpty(user.Photo) ? "/Images/Photo.jpg" : user.Photo;
            //Session["Account"] = account;
            //GetThemes(user.Id);

            //LoginUserManage.Add(Session.SessionID, account.Id);

            ////在线用户统计
            ////OnlineHttpModule.ProcessRequest();
            //LogHandler.WriteServiceLog(UserName, ResultHelper.NowTime + "登录系统,IP:" + ResultHelper.GetUserIP(), "成功", "登录", "系统入口");
            //return Json(JsonHandler.CreateMessage(1, ""), JsonRequestBehavior.AllowGet);
        }
        /// <summary>
        /// 安全退出
        /// </summary>
        [HttpPost]
        public void LogOut()
        {
            if (Session["Account"] != null)
                Session["Account"] = null;
            Session.Clear();
            Session.Abandon();
        }

        public void GetThemes(string userid)
        {
            //SysUserConfigModel entity = userConfigBLL.GetById("themes", userid);
            //SysUserConfigModel menuEntity = userConfigBLL.GetById("menu", userid);
            //if (entity != null)
            //{
            //    Session["themes"] = entity.Value;
            //}
            //else
            //{
            //    Session["themes"] = "blue";
            //}
            //if (menuEntity != null)
            //{
            //    Session["menu"] = menuEntity.Value;
            //}
            //else
            //{
            //    Session["menu"] = "accordion";
            //}
        }
    }
}