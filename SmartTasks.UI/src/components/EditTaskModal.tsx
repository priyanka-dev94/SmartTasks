import { useState } from "react";
import axios from "axios";
import type { TaskItem, TaskStatus } from "../models/task";
import { useUpdateTask } from "../hooks/useUpdateTask";
import type { ValidationProblemDetails } from "../models/problemDetails";

interface Props {
  task: TaskItem;
  onClose: () => void;
}

export const EditTaskModal = ({ task, onClose }: Props) => {
  const [title, setTitle] = useState(task.title);
  const [description, setDescription] = useState(task.description ?? "");
  const [status, setStatus] = useState<TaskStatus>(task.status);
  const [dueDate, setDueDate] = useState(
        task.dueDate ? task.dueDate.substring(0, 10) : ""
    );

  const [fieldErrors, setFieldErrors] =
    useState<Record<string, string[]>>({});
  const [submitError, setSubmitError] = useState<string | null>(null);

  const { mutateAsync, isPending } = useUpdateTask();

  const handleSubmit = async () => {
    setFieldErrors({});
    setSubmitError(null);

    try {
      await mutateAsync({
        id: task.id,
        data: {
          title,
          description,
          status,
          dueDate: dueDate ? new Date(dueDate).toISOString() : null,
        },
      });

      onClose();
    } catch (err) {
      if (axios.isAxiosError(err)) {
        const data = err.response?.data as ValidationProblemDetails;
        if (data?.errors) {
          setFieldErrors(data.errors);
          return;
        }
      }

      setSubmitError("Failed to update task");
    }
  };

  return (
    <div className="fixed inset-0 bg-black/40 flex items-center justify-center">
      <div className="bg-white rounded p-6 w-full max-w-md space-y-4">
        <h2 className="text-lg font-semibold">Edit Task</h2>

        <input
          className="w-full border px-3 py-2 rounded"
          value={title}
          onChange={(e) => setTitle(e.target.value)}
        />
        {fieldErrors.Title && (
          <p className="text-sm text-red-600">{fieldErrors.Title[0]}</p>
        )}

        <textarea
          className="w-full border px-3 py-2 rounded"
          value={description}
          onChange={(e) => setDescription(e.target.value)}
        />
        {fieldErrors.Description && (
          <p className="text-sm text-red-600">
            {fieldErrors.Description[0]}
          </p>
        )}

        <select
          className="w-full border px-3 py-2 rounded"
          value={status}
          onChange={(e) => setStatus(e.target.value as TaskStatus)}
        >
          <option value="Pending">Pending</option>
          <option value="InProgress">In Progress</option>
          <option value="Completed">Completed</option>
          <option value="Snoozed">Snoozed</option>
          <option value="Archived">Archived</option>
        </select>

        <input
            type="date"
            className="w-full border px-3 py-2 rounded"
            value={dueDate}
            onChange={(e) => setDueDate(e.target.value)}
            />

            {fieldErrors.DueDate && (
            <p className="text-sm text-red-600">{fieldErrors.DueDate[0]}</p>
            )}


        {submitError && (
          <p className="text-sm text-red-600">{submitError}</p>
        )}

        <div className="flex justify-end gap-2">
          <button
            className="px-3 py-1 border rounded"
            onClick={onClose}
          >
            Cancel
          </button>

          <button
            className="px-3 py-1 bg-blue-600 text-white rounded disabled:opacity-50"
            disabled={isPending || !title.trim()}
            onClick={handleSubmit}
          >
            Save
          </button>
        </div>
      </div>
    </div>
  );
};
