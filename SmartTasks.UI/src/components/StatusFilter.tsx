import { type TaskStatus } from "../models/task";

interface Props {
  value?: TaskStatus;
  onChange: (value?: TaskStatus) => void;
}

export const StatusFilter = ({ value, onChange }: Props) => {
  return (
    <select
      className="border rounded px-3 py-2 text-sm"
      value={value ?? ""}
      onChange={(e) =>
        onChange(e.target.value === "" ? undefined : (e.target.value) as TaskStatus)
      }
    >
      <option value="">All</option>
      <option value="Pending">Pending</option>
      <option value="InProgress">In Progress</option>
      <option value="Completed">Completed</option>
      <option value="Snoozed">Snoozed</option>
      <option value="Archived">Archived</option>
    </select>
  );
};
