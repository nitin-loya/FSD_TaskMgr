using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using DAL;
using EAL;
using System.Data.Entity;
using System.Linq;
using System.Collections.Generic;

namespace TaskMgr.Tests.DAL
{
    [TestClass]
    public class TaskManagerTest
    {
        Mock<TaskModel> _mockContext;
        Mock<DbSet<Task>> _mockTasks;
        IQueryable<Task> _taskList;

        [TestMethod]
        public void TestMethod1()
        {
        }

        [TestInitialize]
        public void Initialize()
        {
            _taskList = new List<Task>
            {
                new Task
                {
                    TaskId = 1,
                    TaskName = "Task 1",
                    Priority = 1,
                    StartDate = DateTime.Today,
                    EndDate = DateTime.Today.AddDays(7),
                    Status = "I"
                },
                new Task
                {
                    TaskId = 2,
                    TaskName = "Task 2",
                    Priority = 2,
                    StartDate = DateTime.Today,
                    EndDate = DateTime.Today.AddMonths(1),
                    Status = "I"
                },
                new Task
                {
                    TaskId = 3,
                    TaskName = "Task 3",
                    Priority = 2,
                    StartDate = DateTime.Today,
                    EndDate = DateTime.Today.AddMonths(1),
                    Status = "I",
                    ParentId = 2
                },
                new Task
                {
                    TaskId = 4,
                    TaskName = "Task 4",
                    Priority = 3,
                    StartDate = DateTime.Today,
                    EndDate = DateTime.Today.AddMonths(2),
                    Status = "I",
                    ParentId = 2
                }
            }.AsQueryable();

            _mockTasks = new Mock<DbSet<Task>>();
            _mockTasks.As<IQueryable<Task>>().Setup(t => t.Provider).Returns(_taskList.Provider);
            _mockTasks.As<IQueryable<Task>>().Setup(t => t.Expression).Returns(_taskList.Expression);
            _mockTasks.As<IQueryable<Task>>().Setup(t => t.ElementType).Returns(_taskList.ElementType);
            _mockTasks.As<IQueryable<Task>>().Setup(t => t.GetEnumerator()).Returns(_taskList.GetEnumerator());
        }

        private ITaskContext GetTestContext()
        {
            var context = new TestTaskContext();
            context.Tasks.Add(new Task
            {
                TaskId = 1,
                TaskName = "Task 1",
                Priority = 1,
                StartDate = DateTime.Today,
                EndDate = DateTime.Today.AddDays(7),
                Status = "I"
            });

            context.Tasks.Add(new Task
            {
                TaskId = 2,
                TaskName = "Task 2",
                Priority = 2,
                StartDate = DateTime.Today,
                EndDate = DateTime.Today.AddMonths(1),
                Status = "I"
            });

            context.Tasks.Add(new Task
            {
                TaskId = 3,
                TaskName = "Task 3",
                Priority = 2,
                StartDate = DateTime.Today,
                EndDate = DateTime.Today.AddMonths(1),
                Status = "I",
                ParentId = 2
            });

            context.Tasks.Add(new Task
            {
                TaskId = 4,
                TaskName = "Task 4",
                Priority = 3,
                StartDate = DateTime.Today,
                EndDate = DateTime.Today.AddMonths(2),
                Status = "I",
                ParentId = 2
            });

            return context;
        }

        [TestMethod]
        public void GetTasks_ReturnsAllTasks()
        {
            var taskDao = new TaskManager(GetTestContext());
            var actualTaskList = taskDao.GetTasks();
            Assert.IsNotNull(actualTaskList);
            Assert.AreEqual(4, actualTaskList.Count());
        }

        [TestMethod]
        public void GetTask_ExistingTask()
        {
            var taskDao = new TaskManager(GetTestContext());
            var actualTask = taskDao.GetTask(1);
            Assert.IsNotNull(actualTask);
            Assert.AreEqual("Task 1", actualTask.TaskName);
        }

        [TestMethod]
        public void GetTAsk_NonExistingTask()
        {
            _mockContext = new Mock<TaskModel>();
            _mockContext.Setup(t => t.Tasks).Returns(_mockTasks.Object);
            var taskDao = new TaskManager(GetTestContext());
            var actualTask = taskDao.GetTask(10);
            Assert.IsNull(actualTask);
        }

        [TestMethod]
        public void UpdateTask_ExistingTask()
        {
            _mockContext = new Mock<TaskModel>();
            _mockContext.Setup(t => t.Tasks).Returns(_mockTasks.Object);
            var taskDao = new TaskManager(GetTestContext());
            var modifiedTask = new Task
            {
                TaskId = 2,
                TaskName = "Perform Review",
                StartDate = DateTime.Today,
                EndDate = DateTime.Today.AddDays(2),
                Priority = 1,
                Status = "I"
            };

            taskDao.UpdateTask(modifiedTask);
            var updatedTask = taskDao.GetTask(2);
            Assert.IsNotNull(updatedTask);
            Assert.AreEqual("Perform Review", updatedTask.TaskName);
        }

        [TestMethod]
        public void UpdateTask_NonExistingTask()
        {
            var taskDao = new TaskManager(GetTestContext());
            var modifiedTask = new Task
            {
                TaskId = 10,
                TaskName = "Perform Review",
                StartDate = DateTime.Today,
                EndDate = DateTime.Today.AddDays(2),
                Priority = 1,
                Status = "I"
            };

            taskDao.UpdateTask(modifiedTask);
            var updatedTask = taskDao.GetTask(10);
            Assert.AreEqual(5, taskDao.GetTasks().Count());
        }
    }
}
