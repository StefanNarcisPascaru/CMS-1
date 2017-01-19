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
    public class CompilerControllerTest
    {
        [TestMethod]
        public void When_IndexIsCalled_Should_ReturnView()
        {
            CompilerController a = new CompilerController();
            a.Index().Should().BeOfType(typeof(ViewResult));
        }

        [TestMethod]
        public void When_CompillerIsCalled_Should_ReturnView()
        {
            CompilerController a = new CompilerController();
            a.GenerateOuput("public class HelloWorld { public static void main(String[] args){ System.out.println(\"Hello, World\"); }", "HelloWorld").Should().Be("Hello, World\r\n");
        }
    }
}
