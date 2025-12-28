import { useState } from "react";
const [submitError, setSubmitError] = useState<string | null>(null);
import { useCreateTask } from "../hooks/useCreateTask";

interface Props {
  onClose: () => void;
}

export const CreateTaskModal = ({ onClose }: Props) => {
  const [title, setTitle] = useState("");
  const [description, setDescription] = useState("");

  const { mutateAsync, isPending, error } = useCreateTask();

  const handleSubmit = async () => {
        setSubmitError(null);

        try {
            await mutateAsync({
            title,
            description,
            });

            onClose();
        } catch (err) {
            setSubmitError("Failed to create task");
        }
    };

  return (
    <div className="fixed inset-0 bg-black/40 flex items-center justify-center">
      <div className="bg-white rounded p-6 w-full max-w-md space-y-4">
        <h2 className="text-lg font-semibold">Create Task</h2>

        <input
          className="w-full border px-3 py-2 rounded"
          placeholder="Title"
          value={title}
          onChange={(e) => setTitle(e.target.value)}
        />

        <textarea
          className="w-full border px-3 py-2 rounded"
          placeholder="Description"
          value={description}
          onChange={(e) => setDescription(e.target.value)}
        />

        {submitError && (
          <p className="text-sm text-red-600">
            {submitError}
          </p>
        )}

        <div className="flex justify-end gap-2">
          <button
            className="px-3 py-1 border rounded"
            onClick={onClose}
          >
            Cancel
          </button>

          <button
            className="px-3 py-1 bg-blue-600 text-white rounded disabled:opacity-50"
            disabled={isPending || !title.trim()}
            onClick={handleSubmit}
          >
            Create
          </button>
        </div>
      </div>
    </div>
  );
};
