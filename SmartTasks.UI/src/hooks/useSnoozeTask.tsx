import { useMutation, useQueryClient } from "@tanstack/react-query";
import { updateTask } from "../api/task.api";
import { type TaskStatus } from "../models/task";

export const useSnoozeTask = () => {
  const queryClient = useQueryClient();

  return useMutation({
    mutationFn: ({
      id,
      dueDate,
      title
    }: {
      id: string;
      dueDate: string;
      title: string;
    }) =>
      updateTask(id, {
        status: "Snoozed" as TaskStatus,
        dueDate,
        title
      }),

    onSuccess: () => {
      queryClient.invalidateQueries({ queryKey: ["tasks"] });
    },
  });
};
