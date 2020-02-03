using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using EAL;

namespace DAL
{
    public class TaskManager
    {
        ITaskContext _context;

        public TaskManager(ITaskContext context)
        {
            _context = context;
        }
        public IEnumerable<Task> GetTasks()
        {            
            return from t in _context.Tasks                   
                   select t;
        }

        public Task GetTask(int taskId)
        {            
            var foundTask = from t in _context.Tasks
                            where t.TaskId == taskId
                            select t;

            return foundTask.FirstOrDefault();
        }

        public void UpdateTask(Task modifiedTask)
        {
            var foundTask = GetTask(modifiedTask.TaskId);
            if(foundTask == null)
            {
                modifiedTask.ParentId = modifiedTask.ParentId == 0 ? null : modifiedTask.ParentId;
                _context.Tasks.Add(modifiedTask);
            }
            else
            {
                foundTask.EndDate = modifiedTask.EndDate;
                foundTask.ParentId = modifiedTask.ParentId == 0? null: modifiedTask.ParentId;
                foundTask.Priority = modifiedTask.Priority;
                foundTask.StartDate = modifiedTask.StartDate;
                foundTask.TaskName = modifiedTask.TaskName;
                _context.MarkAsModified(foundTask);
            }
            
            _context.SaveChanges();
        }
    }
}
