
namespace Lakeshore.SpecialOrderPickupStatus.Dto.SpecialOrderPickupStatus;

public class SpecialOrderDto
{
    public Header Header { get; set; }
}

public class Header
{
    public string? TypeOfOrder { get; set; }
    public string? OrderNumber { get; set; }
    public string? HeaderStatus { get; set; }
    public List<Item> Items { get; set; }
}

public class Item
{
    public decimal? OrderLine { get; set; }
    public string? ItemStatus { get; set; }
    public string? ItemNumber { get; set; }
    public int Quantity { get; set; }
}


