import { useMutation, useQueryClient } from "@tanstack/react-query";
import { updateTask, type UpdateTaskRequest } from "../api/task.api";
import { type TaskItem } from "../models/task";

export const useUpdateTask = () => {
  const queryClient = useQueryClient();

  return useMutation<
    TaskItem,
    unknown,
    { id: string; data: UpdateTaskRequest }
  >({
    mutationFn: ({ id, data }) => updateTask(id, data),
    onSuccess: () => {
      queryClient.invalidateQueries({ queryKey: ["tasks"] });
    },
  });
};
