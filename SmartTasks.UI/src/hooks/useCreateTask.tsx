import { useMutation, useQueryClient } from "@tanstack/react-query";
import { createTask, type CreateTaskRequest } from "../api/task.api";
import { type TaskItem } from "../models/task";

export const useCreateTask = () => {
  const queryClient = useQueryClient();

  return useMutation<TaskItem, unknown, CreateTaskRequest>({
    mutationFn: createTask,
    onSuccess: () => {
      queryClient.invalidateQueries({ queryKey: ["tasks"] });
    },
  });
};
