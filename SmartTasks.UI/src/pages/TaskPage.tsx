import { useState } from "react";
import { useTasks } from "../hooks/useTasks";
import { TaskTable } from "../components/TaskTable";
import { StatusFilter } from "../components/StatusFilter";
import { type TaskStatus } from "../models/task";
import { CreateTaskModal } from "../components/CreateTaskModal";

export const TasksPage = () => {
  const [pageNumber, setPageNumber] = useState(1);
  const [status, setStatus] = useState<TaskStatus | undefined>();
  const [showModal, setShowModal] = useState(false);

  const { data, isLoading, error } = useTasks({
    pageNumber,
    pageSize: 10,
    status,
  });

  if (isLoading) return <p>Loading...</p>;
  if (error) return <p>Error loading tasks</p>;

  return (
    <div className="p-6 space-y-4">
      
      <div className="flex justify-between items-center">
        <StatusFilter value={status} onChange={setStatus} />

      <button className="px-3 py-1 bg-blue-600 text-white rounded"
          onClick={() => setShowModal(true)}>
          + New Task
        </button>
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
      {showModal && <CreateTaskModal onClose={() => setShowModal(false)} />}
    </div>
    
  );
};
