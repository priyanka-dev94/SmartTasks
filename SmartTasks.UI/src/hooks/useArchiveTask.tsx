import { useMutation, useQueryClient } from "@tanstack/react-query";
import { updateTask } from "../api/task.api";
import type { TaskStatus } from "../models/task";

export const useArchiveTask = () => {
  const queryClient = useQueryClient();

  return useMutation({
    mutationFn: ({
      id,
      title,
      dueDate,
    }: {
      id: string;
      title: string;
      dueDate?: string | null;
    }) =>
      updateTask(id, {
        title,
        status: "Archived" as TaskStatus,
        dueDate,
      }),

    onSuccess: () => {
      queryClient.invalidateQueries({ queryKey: ["tasks"] });
    },
  });
};
