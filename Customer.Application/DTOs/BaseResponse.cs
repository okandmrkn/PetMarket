namespace Customer.Application.DTOs
{
    // <T> ifadesi bu sınıfın her türlü veriyi (Data) taşıyabileceği anlamına gelir.
    public class BaseResponse<T>
    {
        public bool IsSuccess { get; set; }
        public string Message { get; set; } = string.Empty;
        public T? Data { get; set; }

        // Başarılı durumlar için yardımcı metod
        public static BaseResponse<T> Success(T data, string message = "İşlem başarılı.")
        {
            return new BaseResponse<T> 
            { 
                IsSuccess = true, 
                Data = data, 
                Message = message 
            };
        }

        // Hatalı durumlar için yardımcı metod
        public static BaseResponse<T> Failure(string message)
        {
            return new BaseResponse<T> 
            { 
                IsSuccess = false, 
                Data = default, // Veri yok (null)
                Message = message 
            };
        }
    }
}