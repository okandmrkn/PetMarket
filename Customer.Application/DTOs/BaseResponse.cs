namespace Customer.Application.DTOs
{
    public class BaseResponse<T>
    {
        public bool IsSuccess { get; set; }
        public string Message { get; set; } = string.Empty;
        public T? Data { get; set; }

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