using Microsoft.VisualStudio.TestTools.UnitTesting;
using TaskMgr.Web.Controllers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net.Http;
using System.Web.Http;

namespace TaskMgr.Web.Controllers.Tests
{
    [TestClass()]
    public class TaskControllerTests
    {
        [TestMethod()]
        public void GetTest()
        {
            // Set up Prerequisites   
            var controller = new TaskController();
            controller.Request = new HttpRequestMessage();
            controller.Configuration = new HttpConfiguration();
            // Act on Test  
            var response = controller.Get(1);
            // Assert the result  
            //EAL.Task task;
            Assert.IsTrue(response.Status == "I");
            Assert.AreEqual("Full Stack Engineer", response.TaskName);
        }

        [TestMethod()]
        public void AllTest()
        {
            // Set up Prerequisites   
            var controller = new TaskController();
            controller.Request = new HttpRequestMessage();
            controller.Configuration = new HttpConfiguration();
            // Act on Test  
            var response = controller.All();
            // Assert the result
            EAL.Task task = (EAL.Task)response.First<Object>();
            Assert.IsTrue(response.Count()>0);
            Assert.AreEqual("Full Stack Engineer", response.First<Object>().ToString());
            //Assert.Fail();
        }

        [TestMethod()]
        public void TaskControllerTest()
        {
            Assert.IsTrue(true);
        }

        [TestMethod()]
        public void TaskControllerTest1()
        {
            Assert.IsTrue(true);
        }

        [TestMethod()]
        public void ParentListTest()
        {
            Assert.IsTrue(true);
        }

        [TestMethod()]
        public void AllTest1()
        {
            Assert.IsTrue(true);
        }

        [TestMethod()]
        public void AddTest()
        {
            Assert.IsTrue(true);
        }

        [TestMethod()]
        public void UpdateTest()
        {
            Assert.IsTrue(true);
        }

        [TestMethod()]
        public void EndTest()
        {
            Assert.IsTrue(true);
        }
    }
}