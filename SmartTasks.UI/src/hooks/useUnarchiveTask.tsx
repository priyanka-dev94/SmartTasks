import { useMutation, useQueryClient } from "@tanstack/react-query";
import { updateTask } from "../api/task.api";
import { type TaskStatus } from "../models/task";

export const useUnarchiveTask = () => {
  const queryClient = useQueryClient();

  return useMutation({
    mutationFn: ({
      id,
      title,
      description,
      dueDate,
    }: {
      id: string;
      title: string;
      description?: string;
      dueDate?: string | null;
    }) =>
      updateTask(id, {
        title,
        description,
        status: "Pending" as TaskStatus,
        dueDate,
      }),

    onSuccess: () => {
      queryClient.invalidateQueries({ queryKey: ["tasks"] });
    },
  });
};
