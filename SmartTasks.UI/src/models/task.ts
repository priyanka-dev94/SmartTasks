export type TaskStatus = "Pending" | "InProgress" | "Completed" | "Snoozed" | "Archived";

export interface TaskItem {
  id: string;
  title: string;
  description?: string;
  status: TaskStatus;
  createdOn: string;
  dueDate?: string | null;
}


export interface PagedResponse<T> {
  items: T[];
  totalCount: number;
  pageNumber: number;
  pageSize: number;
}
