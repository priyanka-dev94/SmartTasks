import { type TaskStatus } from "../models/task";

interface Props {
  status: TaskStatus;
}

export const StatusBadge = ({ status }: Props) => {
  const base =
    "inline-flex items-center px-2 py-1 rounded text-xs font-medium";

  const styles: Record<TaskStatus, string> = {
    Pending: "bg-gray-100 text-gray-700",
    InProgress: "bg-yellow-100 text-yellow-800",
    Completed: "bg-green-100 text-green-800",
    Snoozed: "bg-blue-100 text-blue-800",
    Archived: "bg-slate-200 text-slate-700",
  };

  const labelMap: Record<TaskStatus, string> = {
    Pending: "Pending",
    InProgress: "In Progress",
    Completed: "Completed",
    Snoozed: "Snoozed",
    Archived: "Archived",
  };

  return (
    <span className={`${base} ${styles[status]}`}>
      {labelMap[status]}
    </span>
  );
};
