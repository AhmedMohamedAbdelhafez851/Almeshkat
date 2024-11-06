namespace Almeshkat_Online_Schools.Utilities
{
    public static class ApiResponseFactory
    {
        public static ApiResponse<T> Success<T>(T data = default!, string message = ApiMessages.Success)
        {
            return new ApiResponse<T>("success", message, data);
        }

        public static ApiResponse<string> SuccessMessage(string message)
        {
            return new ApiResponse<string>("success", message, null!);
        }

        public static ApiResponse<T> Error<T>(string message = ApiMessages.ServerError, T data = default!)
        {
            return new ApiResponse<T>("error", message, data);
        }

        public static ApiResponse<string> ValidationError(string message = ApiMessages.ValidationError)
        {
            return new ApiResponse<string>("error", message, null!);
        }
    }

}
