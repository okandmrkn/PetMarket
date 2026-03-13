namespace Product.Application.DTOs
{
    public record BaseResponse<T>
    {
        public bool IsSuccess { get; init; }
        public string Message { get; init; } = string.Empty;
        public T? Data { get; init; }

        public static BaseResponse<T> Success(T data, string message = "İşlem başarılı.")
        {
            return new BaseResponse<T>
            {
                IsSuccess = true,
                Data = data,
                Message = message
            };
        }

        public static BaseResponse<T> Failure(string message)
        {
            return new BaseResponse<T>
            {
                IsSuccess = false,
                Data = default,
                Message = message
            };
        }
    }
}