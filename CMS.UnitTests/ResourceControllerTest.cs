using CMS.WebUI.Controllers;
using FluentAssertions;
using Microsoft.AspNetCore.Mvc;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CMS.UnitTests
{   [TestClass]
    public class ResourceControllerTest
    {
        [TestMethod]
        public void When_ResourceIsCalled_Shoul_ReturnView()
        {
            ResourceController a = new ResourceController();
            a.Resources().Should().BeOfType(typeof(ViewResult));
        }
    }
}
