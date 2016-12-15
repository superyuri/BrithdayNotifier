using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using WebApp.Domain;
using System.Collections.Generic;
using WebApp.Handlers;
using Senparc.Weixin.MP.Entities.Request;
using Senparc.Weixin.MP.Entities;

namespace WebappTest
{
    [TestClass]
    public class UnitTest1
    {
        [TestMethod]
        public void LoadDataTest()
        {
            var  obj = BirthdayDoamin.GetBirthdays();
            Assert.IsNotNull(obj);
        }
        [TestMethod]
        public void GetRecentBrithdaysTest()
        {
            var obj = BirthdayDoamin.GetBirthdays();
            IEnumerable<Birthday> lists = BirthdayDoamin.GetRecentBrithdays(30);
            Assert.IsNotNull(lists);
        }
        [TestMethod]
        public void GetBrithdaysByMonthTest()
        {
            var obj = BirthdayDoamin.GetBirthdays();
            IEnumerable<Birthday> lists = BirthdayDoamin.GetBrithdaysByMonth(2016, 12);
            Assert.IsNotNull(lists);
        }
        [TestMethod]
        public void TransformStringTest()
        {
            var obj = BirthdayDoamin.GetBirthdays();
            string s = BirthdayDoamin.TransformString(BirthdayDoamin.GetBirthdays());
            Assert.IsNotNull(s);
        }
        [TestMethod]
        public void OnTextRequestTest()
        {
            BirthdayDoamin.TransformString(BirthdayDoamin.GetBrithdaysByMonth(DateTime.Now.Year, 12));

            //var requestMessageText = new RequestMessageText();
            //requestMessageText.Content = "m12";
            //ResponseMessageText callback = (ResponseMessageText)(customMessageHandler.OnTextOrEventRequest(requestMessageText));
            
            //Assert.IsNotNull(callback.Content);
        }
    }
}
