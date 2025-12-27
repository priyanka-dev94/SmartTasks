import { type TaskItem } from "../models/task";
import { StatusBadge } from "./StatusBadge";

interface Props {
  tasks: TaskItem[];
}

export const TaskTable = ({ tasks }: Props) => {
  return (
    <table className="w-full border border-gray-200 rounded overflow-hidden">
      <thead className="bg-gray-50">
        <tr>
          <th className="border px-3 py-2 text-left">Title</th>
          <th className="border px-3 py-2">Status</th>
          <th className="border px-3 py-2">Created</th>
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
          </tr>
        ))}
      </tbody>
    </table>
  );
};
