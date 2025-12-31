import { type TaskItem } from "../models/task";
import { StatusBadge } from "./StatusBadge";
import { isOverdue } from "../utils/dateUtils";
import { getSnoozeDate } from "../utils/snoozeUtils";
import { useSnoozeTask } from "../hooks/useSnoozeTask";
import { useUnarchiveTask } from "../hooks/useUnarchiveTask";
import { useArchiveTask } from "../hooks/useArchiveTask";

interface Props {
  tasks: TaskItem[];
  onEdit: (task: TaskItem) => void;
  onDelete: (taskId: TaskItem) => void;
}

export const TaskTable = ({ tasks, onEdit, onDelete }: Props) => {
  const { mutate: snoozeTask } = useSnoozeTask();
  const { mutate: unarchiveTask } = useUnarchiveTask();
  const { mutate: archiveTask } = useArchiveTask();

  const canEdit = (status: string) =>
    status !== "Completed" && status !== "Archived";

  const canSnooze = (status: string) =>
    status === "Pending" || status === "InProgress" || status === "Snoozed";

  const canArchive = (status: string) =>
    status !== "Archived";

  const canUnarchive = (status: string) =>
    status === "Archived";


  return (
    <table className="w-full border border-gray-200 rounded overflow-hidden">
      <thead className="bg-gray-50">
        <tr>
          <th className="border px-3 py-2 text-left">Title</th>
          <th className="border px-3 py-2">Status</th>
          <th className="border px-3 py-2">Created</th>
          <th className="border px-3 py-2 text-center">Due Date</th>
          <th className="border px-3 py-2 text-center">Actions</th>
        </tr>
      </thead>
      <tbody>
        {tasks.map((t) => (
          <tr key={t.id} className={`hover:bg-gray-50 ${
            isOverdue(t.dueDate, t.status)
              ? "bg-red-50 border-l-4 border-red-500"
              : "odd:bg-white even:bg-gray-50/50"
          }`}>
            <td className="border px-3 py-2">{t.title}</td>
            <td className="border px-3 py-2 text-center">
              <StatusBadge status={t.status} />
              {isOverdue(t.dueDate, t.status) && (
                <span className="ml-2 text-xs text-red-600 bg-red-100 px-2 py-0.5 rounded">
                  Overdue
                </span>
              )}
            </td>
            <td className="border px-3 py-2 text-center">
              {new Date(t.createdOn).toLocaleDateString()}
            </td>
            <td className={`border px-3 py-2 text-center ${
              isOverdue(t.dueDate, t.status)
                ? "text-red-600 font-semibold"
                : ""
            }`}>
              {t.dueDate
                ? new Date(t.dueDate).toLocaleDateString()
                : "—"}
            </td>
            <td className="border px-3 py-2 text-center space-x-2">
            {canEdit(t.status) && (
              <button
                className="text-blue-600 text-sm hover:underline"
                onClick={() => onEdit(t)}
              >
                Edit
              </button>
            )}

            {canSnooze(t.status) && (
              <button
                className="text-orange-600 text-sm hover:underline"
                onClick={() =>
                  snoozeTask({
                    id: t.id,
                    title: t.title,
                    dueDate: getSnoozeDate("tomorrow"),
                  })
                }
              >
                Snooze
              </button>
            )}

            {canArchive(t.status) && !canUnarchive(t.status) && (
              <button
                className="text-gray-600 text-sm hover:underline"
                onClick={() => archiveTask({
                  id: t.id,
                  title: t.title,
                  dueDate: t.dueDate,
                })}
              >
                Archive
              </button>
            )}

            {canUnarchive(t.status) && (
              <button
                className="text-green-600 text-sm hover:underline"
                onClick={() => unarchiveTask({
                  id: t.id,
                  title: t.title,
                  dueDate: t.dueDate,
                })}
              >
                Unarchive
              </button>
            )}

            <button
              className="text-red-600 text-sm hover:underline"
              onClick={() => onDelete(t)}
            >
              Delete
            </button>
          </td>
           
          </tr>
        ))}
      </tbody>
    </table>
  );
};
