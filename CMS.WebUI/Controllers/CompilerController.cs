using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using System;
using System.Diagnostics;
using CMS.WebUI.ViewModels;
using System.Text;
using System.Collections.Generic;

namespace CMS.WebUI.Controllers
{

    [Authorize]
    public class CompilerController : Controller
    {
       public static CompilerData data = new CompilerData() { Input = "Write code", Output = "Result" };
        public IActionResult Index()
        {
            
            return View(data);
        }

 
    private string GenerateOuput(string Input, string ClassName)
        {
     
            /* Run CMD */
            Process process = new Process();
            ProcessStartInfo startInfo = new ProcessStartInfo();
            startInfo.UseShellExecute = false;
            startInfo.RedirectStandardOutput = true;
            startInfo.WorkingDirectory = @"E:\\LocalServerData";
            startInfo.FileName = "CMD.exe";
            startInfo.Arguments = "/c javac " + ClassName + ".java 2> error.txt";
            process.StartInfo = startInfo;
            process.Start();
            process.WaitForExit();

            string output = System.IO.File.ReadAllText("e:\\LocalServerData\\error.txt", Encoding.UTF8);

            if (output.Length == 0)
            {
                process = new Process();
                startInfo = new ProcessStartInfo();
                startInfo.UseShellExecute = false;
                startInfo.RedirectStandardOutput = true;
                startInfo.WorkingDirectory = @"E:\\LocalServerData";
                startInfo.FileName = "CMD.exe";
                startInfo.Arguments = "/c java " + ClassName;
                process.StartInfo = startInfo;
                process.Start();
                output = process.StandardOutput.ReadToEnd();
                process.WaitForExit();

                Console.WriteLine(output);
            }

            return output;
        }


        [HttpPost]
        public IActionResult ClickButton_saveInputToServer(string InputData)
        {
            string ClassName = null;
            List<string> list = new List<string>();

            string[] words = InputData.Split(new char[] { ' ', '\t', '{' }, StringSplitOptions.RemoveEmptyEntries);
            foreach (string word in words)
            {
                if (word != " ")
                    list.Add(word);

            }

            for (int i = 0; i < list.Count; i++)
            {
                if (list[i] == "class")
                    ClassName = list[i + 1];
            }

            if (ClassName != null)
            {
                ClassName = ClassName.Remove(ClassName.Length - 2);
                System.IO.File.WriteAllText("E:\\LocalServerData\\" + ClassName + ".java", InputData);
                data = new CompilerData() { Input = InputData, Output = GenerateOuput(InputData, ClassName) };
            }
            else
                data = new CompilerData() { Input = InputData, Output = "Corrupted file"};


            return RedirectToAction("Index");

        }
    }
}
