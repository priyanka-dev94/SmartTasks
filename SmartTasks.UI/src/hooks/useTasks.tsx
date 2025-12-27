import { keepPreviousData, useQuery } from "@tanstack/react-query";
import { getTasks, type TaskQueryParams } from "../api/task.api";

export const useTasks = (params: TaskQueryParams) => {
  return useQuery({
    queryKey: ["tasks", params],
    queryFn: () => getTasks(params),
    placeholderData: keepPreviousData,
  });
};
