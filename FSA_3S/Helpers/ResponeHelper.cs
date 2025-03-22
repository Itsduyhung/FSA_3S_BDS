namespace FSA_3S.Helpers
{
    public class ResponeHelper<T>
    {
        public T? Data { get; set; }
        public string Message { get; set; } = string.Empty;
        public bool IsSuccess { get; set; }
    }
}
