using Senparc.Weixin.MP;
using Senparc.Weixin.MP.Entities.Request;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebApp.Handlers;

namespace WebApp.Controllers
{
    using Senparc.Weixin.MP.MvcExtension;
    public class WeixinController : Controller
    {
        public readonly string Token = "edwardyin9";
        public readonly string EncodingAESKey = "KwQVjPhm1Z7h9Csph2idLvU293X8kszTdZyT4aoqXmH";
        public readonly string AppId = "wx280becd860183e43";
        /// <summary>
        /// 微信后台验证地址（使用Get），微信后台的“接口配置信息”的Url填写如：http://weixin.senparc.com/weixin
        /// </summary>
        [HttpGet]
        [ActionName("Index")]
        public ActionResult Get(PostModel postModel, string echostr)
        {
            if (CheckSignature.Check(postModel.Signature, postModel.Timestamp, postModel.Nonce, Token))
            {
                return Content(echostr); //返回随机字符串则表示验证通过
            }
            else
            {
                return Content("failed:" + postModel.Signature + ","
                    + Senparc.Weixin.MP.CheckSignature.GetSignature(postModel.Timestamp, postModel.Nonce, Token) + "。" +
                    "如果你在浏览器中看到这句话，说明此地址可以被作为微信公众账号后台的Url，请注意保持Token一致。");
            }
        }
        /// <summary>
        /// 用户发送消息后，微信平台自动Post一个请求到这里，并等待响应XML
        /// </summary>
        [HttpPost]
        [ActionName("Index")]
        public ActionResult Post(PostModel postModel)
        {
            if (!CheckSignature.Check(postModel.Signature, postModel.Timestamp, postModel.Nonce, Token))
            {
                return Content("参数错误！");
            }
            postModel.Token = Token;
            postModel.EncodingAESKey = EncodingAESKey;//根据自己后台的设置保持一致
            postModel.AppId = AppId;//根据自己后台的设置保持一致

            var messageHandler = new CustomMessageHandler(Request.InputStream, postModel);//接收消息（第一步）

            messageHandler.Execute();//执行微信处理过程（第二步）

            return new WeixinResult(messageHandler);//返回（第三步）
        }
    }
}