import { api } from "./axios";
import type { PagedResponse, TaskItem, TaskStatus } from "../models/task";

export interface TaskQueryParams {
  pageNumber: number;
  pageSize: number;
  status?: TaskStatus;
}

export const getTasks = async (
  params: TaskQueryParams
): Promise<PagedResponse<TaskItem>> => {
  const response = await api.get("/tasks/paged", { params });
  return response.data;
};
