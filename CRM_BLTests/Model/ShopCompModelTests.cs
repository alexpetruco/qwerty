using Microsoft.VisualStudio.TestTools.UnitTesting;
using CRM_BL.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CRM_BL.Model.Tests
{
    [TestClass()]
    public class ShopCompModelTests
    {
        [TestMethod()]
        public void StartTest()
        {
            var model = new ShopCompModel();
            model.Start();
        }
    }
}