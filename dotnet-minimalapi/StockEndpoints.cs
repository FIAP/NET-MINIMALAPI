using dotnet_minimalapi.Models;

namespace dotnet_minimalapi;

public static class StockEndpoints
{
    private static List<Stock> stocks = new List<Stock>(); 

    public static void MapStockEndpoints(this WebApplication app)
    {
        var stocksGroup = app.MapGroup("/stocks");

        stocksGroup.MapGet("/", GetAllStocks);
        stocksGroup.MapGet("/{id}", GetStock);
        stocksGroup.MapPost("/", CreateStock);
        stocksGroup.MapPut("/{id}", UpdateStock);
        stocksGroup.MapDelete("/{id}", DeleteStock);
    }

    private static async Task<IResult> GetAllStocks()
    {
        return TypedResults.Ok(stocks);
    }

    private static async Task<IResult> GetStock(int id)
    {
        var stock = stocks.FirstOrDefault(sa => sa.Id == id);
        return stock is not null ? TypedResults.Ok(stock) : TypedResults.NotFound();
    }

    private static async Task<IResult> CreateStock(Stock stockActivity)
    {
        stockActivity.Id = stocks.Count + 1;
        stocks.Add(stockActivity);
        return TypedResults.Created($"/stocks/{stockActivity.Id}", stockActivity);
    }

    private static async Task<IResult> UpdateStock(int id, Stock stockActivity)
    {
        var existingActivity = stocks.FirstOrDefault(sa => sa.Id == id);

        if (existingActivity is null)
            return TypedResults.NotFound();

        existingActivity.Symbol = stockActivity.Symbol;
        existingActivity.Action = stockActivity.Action;
        existingActivity.Quantity = stockActivity.Quantity;

        return TypedResults.NoContent();
    }

    private static async Task<IResult> DeleteStock(int id)
    {
        var existingActivity = stocks.FirstOrDefault(sa => sa.Id == id);

        if (existingActivity is not null)
        {
            stocks.Remove(existingActivity);
            return TypedResults.NoContent();
        }

        return TypedResults.NotFound();
    }
}
