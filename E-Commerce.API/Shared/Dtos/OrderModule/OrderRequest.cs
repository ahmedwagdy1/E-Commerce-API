namespace Shared.Dtos.OrderModule
{
    public record OrderRequest
    {
        public string BasketId { get; init; } = string.Empty;
        public AddressDto Address { get; set; }
        public int DeliveryMethodId { get; set; }
    }
}
