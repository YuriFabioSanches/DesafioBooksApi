using DesafioBooksApi.Communication.Enums;

namespace DesafioBooksApi.Communication.Request;

public class RequestUpdateBookJson
{
    public string Title { get; set; } = string.Empty;
    public string Author { get; set; } = string.Empty;
    public GenreType GenreType { get; set; }
    public double Price { get; set; }
    public int StockCount { get; set; }
}
