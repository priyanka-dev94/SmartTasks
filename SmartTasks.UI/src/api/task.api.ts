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

export interface CreateTaskRequest {
  title: string;
  description?: string;
  dueDate?: string | null;
}

export const createTask = async (
  data: CreateTaskRequest
): Promise<TaskItem> => {
  const response = await api.post("/tasks", data);
  return response.data;
};

export interface UpdateTaskRequest {
  title: string;
  description?: string;
  status: TaskStatus;
  dueDate?: string | null;
}

export const updateTask = async (
  id: string,
  data: UpdateTaskRequest
): Promise<TaskItem> => {
  const response = await api.put(`/tasks/${id}`, data);
  return response.data;
};