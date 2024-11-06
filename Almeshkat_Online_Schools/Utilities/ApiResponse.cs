namespace Almeshkat_Online_Schools.Utilities
{
    public class ApiResponse<T>
    {
        public string Status { get; set; } // "success" or "error"
        public string Message { get; set; }
        public T Data { get; set; }

        // Constructor to initialize all properties
        public ApiResponse(string status, string message, T data)
        {
            Status = status;
            Message = message;
            Data = data;
        }

        // Second constructor to handle different initialization (use default values if needed)
        public ApiResponse(string v, string notFound)
        {
            Status = "error"; // Default status (can be changed based on logic)
            Message = v;      // Assuming 'v' is the message you want to set
            Data = default!;  // Default value for generic type 'T' (if needed)
        }
    }
}
