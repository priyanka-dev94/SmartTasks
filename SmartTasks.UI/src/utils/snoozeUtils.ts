export const getSnoozeDate = (
  type: "today" | "tomorrow" | "nextWeek"
): string => {
  const date = new Date();
  date.setHours(0, 0, 0, 0);

  if (type === "tomorrow") {
    date.setDate(date.getDate() + 1);
  }

  if (type === "nextWeek") {
    date.setDate(date.getDate() + 7);
  }

  return date.toISOString();
};
