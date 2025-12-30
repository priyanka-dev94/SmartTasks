export const isOverdue = (
  dueDate?: string | null,
  status?: string
): boolean => {
  if (!dueDate) return false;
  if (status === "Completed" || status === "Archived" || status === "Snoozed") return false;

  const today = new Date();
  today.setHours(0, 0, 0, 0);

  const due = new Date(dueDate);
  due.setHours(0, 0, 0, 0);

  return due < today;
};
