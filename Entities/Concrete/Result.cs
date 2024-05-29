namespace Entities.Concrete
{
    public class Result<T>
    {
        public T? ResultObject { get; set; }
        public int? ErrorCode { get; set; }
        public string? Message { get; set; }
        public bool Success { get; set; } = true;
    }
}
