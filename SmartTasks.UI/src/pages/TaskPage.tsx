import { useState } from "react";
import { useTasks } from "../hooks/useTasks";
import { TaskTable } from "../components/TaskTable";
import { StatusFilter } from "../components/StatusFilter";
import { type TaskItem, type TaskStatus } from "../models/task";
import { CreateTaskModal } from "../components/CreateTaskModal";
import { EditTaskModal } from "../components/EditTaskModal";
import { DeleteTaskConfirmModal } from "../components/DeleteTaskConfirmModal";
import { useDeleteTask } from "../hooks/useDeleteTask";

export const TasksPage = () => {
  const [pageNumber, setPageNumber] = useState(1);
  const [status, setStatus] = useState<TaskStatus | undefined>();
  const [showModal, setShowModal] = useState(false);
  const [editingTask, setEditingTask] = useState<TaskItem | null>(null);
  const [deletingTask, setDeletingTask] = useState<TaskItem | null>(null);
  const { mutateAsync, isPending } = useDeleteTask();

  const { data, isLoading, error } = useTasks({
    pageNumber,
    pageSize: 10,
    status,
  });
  const hasNextPage = data ? data.pageNumber * data.pageSize < data.totalCount : false;

  if (isLoading) return <p>Loading...</p>;
  if (error) return <p>Error loading tasks</p>;
  if (!data) return <p>No data available</p>;

  return (
    <div className="p-6 space-y-4">
      
      <div className="flex justify-between items-center">
        <StatusFilter value={status} onChange={setStatus} />

      <button className="px-3 py-1 bg-blue-600 text-white rounded"
          onClick={() => setShowModal(true)}>
          + New Task
        </button>
      </div>
      
      <TaskTable tasks={data.items}
        onEdit={(task) => setEditingTask(task)}
        onDelete={(task) => setDeletingTask(task)} />

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
          disabled={!hasNextPage}
          onClick={() => setPageNumber((p) => p + 1)}
        >
          Next
        </button>
      </div>
      {/*--Render Create task modal-->*/}
      {showModal && <CreateTaskModal onClose={() => setShowModal(false)} />}
      {/*--Render Edit task modal-->*/}
      {editingTask && (
        <EditTaskModal
          task={editingTask}
          onClose={() => setEditingTask(null)}
        />
      )}
      {/*--Render Delete task confirm modal-->*/}
      {deletingTask && (
        <DeleteTaskConfirmModal
          title={deletingTask.title}
          isLoading={isLoading}
          onCancel={() => setDeletingTask(null)}
          onConfirm={async () => {
            await mutateAsync(deletingTask.id);
            setDeletingTask(null);
          }}
        />
      )}
    </div>
    
  );
};
