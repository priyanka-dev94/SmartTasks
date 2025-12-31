interface Props {
  title: string;
  onConfirm: () => void;
  onCancel: () => void;
  isLoading?: boolean;
}

export const DeleteTaskConfirmModal = ({
  title,
  onConfirm,
  onCancel,
  isLoading,
}: Props) => {
  return (
    <div className="fixed inset-0 bg-black/40 flex items-center justify-center">
      <div className="bg-white rounded p-6 w-full max-w-sm space-y-4">
        <h2 className="text-lg font-semibold text-red-600">
          Delete Task
        </h2>

        <p className="text-sm">
          Are you sure you want to delete{" "}
          <span className="font-semibold">"{title}"</span>?
          <br />
          This action cannot be undone.
        </p>

        <div className="flex justify-end gap-2">
          <button
            className="px-3 py-1 border rounded"
            onClick={onCancel}
            disabled={isLoading}
          >
            Cancel
          </button>

          <button
            className="px-3 py-1 bg-red-600 text-white rounded disabled:opacity-50"
            onClick={onConfirm}
            disabled={isLoading}
          >
            Delete
          </button>
        </div>
      </div>
    </div>
  );
};
