
using System;
using System.IO;
using Senparc.Weixin.MP.MessageHandlers;
using Senparc.Weixin.MP.Entities;
using Senparc.Weixin.MP.Entities.Request;
using Senparc.Weixin.Context;
using WebApp.Domain;
using System.Text.RegularExpressions;

namespace WebApp.Handlers
{
    public class CustomMessageHandler : MessageHandler<CustomMessageContext>
    {
        public CustomMessageHandler(Stream inputStream, PostModel postModel, int maxRecordCount = 0)
            : base(inputStream, postModel, maxRecordCount)
        {

        }

        public override IResponseMessageBase DefaultResponseMessage(IRequestMessageBase requestMessage)
        {
            //ResponseMessageText也可以是News等其他类型
            var responseMessage = CreateResponseMessage<ResponseMessageText>();
            responseMessage.Content = "这条消息来自DefaultResponseMessage。";
            return responseMessage;
        }

        public override IResponseMessageBase OnTextRequest(RequestMessageText requestMessage)
        {

            //TODO:这里的逻辑可以交给Service处理具体信息，参考OnLocationRequest方法或/Service/LocationSercice.cs
            var responseMessage = CreateResponseMessage<ResponseMessageText>();
            if (requestMessage.Content.Equals("help"))
            {
                responseMessage.Content = TextConstants.Help;
            }
            else if (requestMessage.Content.Equals("生日"))
            {
                responseMessage.Content = BirthdayDoamin.TransformString(BirthdayDoamin.GetRecentBrithdays(31));
            }
            else if (Regex.IsMatch(requestMessage.Content, @"^生日(\d+)$"))
            {
                int days = int.Parse(Regex.Match(requestMessage.Content, @"^生日(\d+)$").Groups[1].Value);
                responseMessage.Content = BirthdayDoamin.TransformString(BirthdayDoamin.GetRecentBrithdays(days));
            }
            else if (Regex.IsMatch(requestMessage.Content, @"^m(0?[[1-9]|1[0-2])$"))
            {
                int month = int.Parse(Regex.Match(requestMessage.Content, @"^m(0?[[1-9]|1[0-2])$").Groups[1].Value);
                responseMessage.Content = BirthdayDoamin.TransformString(BirthdayDoamin.GetBrithdaysByMonth(DateTime.Now.Year, month));
            }
            else if (Regex.IsMatch(requestMessage.Content, @"^m(\d{4})\.(0?[[1-9]|1[0-2])$"))
            {
                Match match = Regex.Match(requestMessage.Content, @"^m(\d{4})\.(0?[[1-9]|1[0-2])$");
                int year = int.Parse(match.Groups[1].Value);
                int month = int.Parse(match.Groups[2].Value);
                responseMessage.Content = BirthdayDoamin.TransformString(BirthdayDoamin.GetBrithdaysByMonth(year, month));
            }
            else
            {
                responseMessage.Content =
                    string.Format(
                        "您刚才发送了文字信息：{0}",
                        requestMessage.Content);
            }
            return responseMessage;
        }
        /*
        public override IResponseMessageBase OnVoiceRequest(RequestMessageVoice requestMessage)
        {
            return new DefaultResponseMessage<RequestMessageVoice>();
        }*/
    }
}