namespace Application.Parameters
{
    public class GetAllMovementsParameters : BaseRequestParameters
    {
        public string ProductName { get; set; } = string.Empty;
        public string Reason { get; set; } = string.Empty;
    }
}