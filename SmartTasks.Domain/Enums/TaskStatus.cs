using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SmartTasks.Domain.Enums
{
    public enum TaskStatus
    {
        Pending = 0,
        Completed = 1,
        Snoozed = 2,
        Archived = 3
    }
}
