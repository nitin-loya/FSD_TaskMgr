using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DAL;
using EAL;

namespace TaskMgr.Tests
{
    public class TestTaskContext : ITaskContext
    {
        public TestTaskContext()
        {
            this.Tasks = new TestTaskDbSet();
        }
        public DbSet<EAL.Task> Tasks
        {
            get; set;
        }

        public void Dispose()
        {            
        }

        public void MarkAsModified(EAL.Task t)
        {            
        }

        public int SaveChanges()
        {
            return 0;
        }
    }
}
