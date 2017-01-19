using CMS.BussinesInterfaces.ModelLogic;
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
    public class HomeControllerTest
    {
        private IUserLogic userLogic;
        [TestMethod]
        public void When_Index_IsCalled_Shoul_ReturnView()
        {
            HomeController a = new HomeController();
            a.Index().Should().BeOfType(typeof(ViewResult));
        }
        [TestMethod]
        public void When_Admin_IsCalled_Shoul_ReturnView()
        {
            HomeController a = new HomeController();
            a.Admin().Should().BeOfType(typeof(ViewResult));
        }
        [TestMethod]
        public void When_Teacher_IsCalled_Shoul_ReturnView()
        {
            HomeController a = new HomeController();
            a.Teacher().Should().BeOfType(typeof(ViewResult));
        }
        [TestMethod]
        public void When_Student_IsCalled_Shoul_ReturnView()
        {
            HomeController a = new HomeController();
            a.Student().Should().BeOfType(typeof(ViewResult));
        }
        [TestMethod]
        public void When_Error_IsCalled_Shoul_ReturnView()
        {
            HomeController a = new HomeController();
            a.Error().Should().BeOfType(typeof(ViewResult));
        }


    }
}
