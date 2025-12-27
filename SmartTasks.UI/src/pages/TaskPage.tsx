import { useState } from "react";
import { useTasks } from "../hooks/useTasks";
import { TaskTable } from "../components/TaskTable";
import { StatusFilter } from "../components/StatusFilter";
import { TaskStatus } from "../models/task";

export const TasksPage = () => {
  const [pageNumber, setPageNumber] = useState(1);
  const [status, setStatus] = useState<TaskStatus | undefined>();

  const { data, isLoading, error } = useTasks({
    pageNumber,
    pageSize: 10,
    status,
  });

  if (isLoading) return <p>Loading...</p>;
  if (error) return <p>Error loading tasks</p>;

  return (
    <div className="p-6 space-y-4">
      <div className="flex items-center gap-4">
        <StatusFilter value={status} onChange={setStatus} />
      </div>

      <TaskTable tasks={data!.items} />

      <div className="flex gap-2">
        <button
          className="px-3 py-1 border rounded disabled:opacity-50"
          disabled={pageNumber === 1}
          onClick={() => setPageNumber((p) => p - 1)}
        >
          Previous
        </button>

        <button
          className="px-3 py-1 border rounded disabled:opacity-50"
          disabled={data!.items.length < 10}
          onClick={() => setPageNumber((p) => p + 1)}
        >
          Next
        </button>
      </div>
    </div>
  );
};
