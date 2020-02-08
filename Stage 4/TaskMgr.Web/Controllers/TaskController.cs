using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using TaskMgr.Web.Util;
using DAL;
using EAL;
using System.Web.Http.Cors;
using log4net;
using log4net.Appender;
using log4net.Core;

namespace TaskMgr.Web.Controllers
{
    [ApiActionFilter, ApiExceptionFilter, EnableCors("*", "*", "*")]
    [RoutePrefix("api")]
    public class TaskController : ApiController
    {
        private TaskManager _taskMgr;

        public TaskController()
        {
            try
            {
                _taskMgr = new TaskManager(new TaskModel());
            }
            catch (Exception ex)
            {
                throw ex;
            }            
        }

        public TaskController(ITaskContext context)
        {
            _taskMgr = new TaskManager(context);
        }

        [HttpGet]
        public IEnumerable<Object> ParentList(int taskId)
        {
            return from task in _taskMgr.GetTasks()
                   where task.TaskId != taskId
                   select new
                   {
                       ParentId = task.TaskId,
                       ParentTask = task.TaskName
                   };
        }

        [HttpGet]
        public IEnumerable<Object> All()
        {
            try
            {
                return from task in _taskMgr.GetTasks()
                       select new
                       {
                           TaskId = task.TaskId,
                           TaskName = task.TaskName,
                           StartDate = task.StartDate.ToString("yyyy-MM-dd"),
                           EndDate = task.EndDate.ToString("yyyy-MM-dd"),
                           Status = task.Status,
                           Priority = task.Priority,
                           ParentTask = task.Task2 != null ? task.Task2.TaskName : string.Empty,
                           ParentId = task.ParentId
                       };
            }
            catch (Exception ex)
            {
                throw ex;
            }
}

        [HttpGet]
        public Task Get(int taskId)
        {
            return _taskMgr.GetTask(taskId);
        }

        [HttpPost, HttpGet]
        public void Add([FromBody] Task newTask)
        {
            _taskMgr.UpdateTask(newTask);
        }

        public void Update(Task modifiedTask)
        {
            _taskMgr.UpdateTask(modifiedTask);
        }

        [HttpPost, HttpGet]
        public void End(int taskId)
        {
            var taskToComplete = _taskMgr.GetTask(taskId);
            taskToComplete.Status = "C";
            _taskMgr.UpdateTask(taskToComplete);
        }
    }
}