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
        InProgress = 1,
        Completed = 2,
        Snoozed = 3,
        Archived = 4
    }
}
