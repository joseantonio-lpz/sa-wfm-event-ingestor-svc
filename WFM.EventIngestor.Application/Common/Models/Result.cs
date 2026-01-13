namespace WFM.EventIngestor.Application.Common.Models
{
     /// <summary>
    /// Objeto genérico de transferencia para casos de uso.
    /// </summary>
    /// <typeparam name="T">Tipo de dato que se transfiere.</typeparam>
    public class Result<T>
    {
        public bool IsSuccess  { get; set; }
        public string Message { get; set; } = string.Empty;
        public T? Data { get; set; }
        public List<string> Errors { get; set; } = new();

        public static Result<T> Ok(T data, string message = "")
            => new() { IsSuccess  = true, Data = data, Message = message };

        public static Result<T> Fail(string message, List<string>? errors = null)
            => new() { IsSuccess  = false, Message = message, Errors = errors ?? new() };
    }
}