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

    static async Task<IResult> GetAllStocks(IStockService stockService)
    {
        var stocks = await stockService.GetAllStocksAsync();
        return TypedResults.Ok(stocks);
    }

    static async Task<IResult> GetStock(int id, IStockService stockService)
    {
        var stock = await stockService.GetStockByIdAsync(id);
        return stock is not null ? TypedResults.Ok(stock) : TypedResults.NotFound();
    }

    static async Task<IResult> CreateStock(Stock stockActivity, IStockService stockService)
    {
        await stockService.AddStockAsync(stockActivity);
        return TypedResults.Created($"/stocks/{stockActivity.Id}", stockActivity);
    }

    static async Task<IResult> UpdateStock(int id, Stock stockActivity, IStockService stockService)
    {
        var success = await stockService.UpdateStockAsync(id, stockActivity);
        return success ? TypedResults.NoContent() : TypedResults.NotFound();
    }

    static async Task<IResult> DeleteStock(int id, IStockService stockService)
    {
        var success = await stockService.DeleteStockAsync(id);
        return success ? TypedResults.NoContent() : TypedResults.NotFound();
    }
}
