import { type TaskItem } from "../models/task";
import { StatusBadge } from "./StatusBadge";

interface Props {
  tasks: TaskItem[];
  onEdit: (task: TaskItem) => void;
}

export const TaskTable = ({ tasks, onEdit }: Props) => {
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
          <tr key={t.id} className="hover:bg-gray-50 odd:bg-white even:bg-gray-50/50">
            <td className="border px-3 py-2">{t.title}</td>
            <td className="border px-3 py-2 text-center">
              <StatusBadge status={t.status} />
            </td>
            <td className="border px-3 py-2 text-center">
              {new Date(t.createdOn).toLocaleDateString()}
            </td>
            <td className="border px-3 py-2 text-center">
              {t.dueDate
                ? new Date(t.dueDate).toLocaleDateString()
                : "—"}
            </td>
            <td className="border px-3 py-2 text-center">
              <button
                className="text-blue-600 text-sm hover:underline"
                onClick={() => onEdit(t)}
              >Edit</button>
            </td>
            
          </tr>
        ))}
      </tbody>
    </table>
  );
};
