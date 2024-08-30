using dotnet_minimalapi.Models;

namespace dotnet_minimalapi;

public class StockService : IStockService
{
    private readonly List<Stock> _stocks = new();

    public Task<List<Stock>> GetAllStocksAsync()
    {
        
        return Task.FromResult(_stocks);
    }

    public Task<Stock?> GetStockByIdAsync(int id)
    {
        if (id == 13)
        {
            throw new Exception("teste");
        }
        var stock = _stocks.FirstOrDefault(sa => sa.Id == id);
        return Task.FromResult(stock);
    }

    public Task AddStockAsync(Stock stock)
    {
        stock.Id = _stocks.Count + 1;
        _stocks.Add(stock);
        return Task.CompletedTask;
    }

    public Task<bool> UpdateStockAsync(int id, Stock stock)
    {
        var existingStock = _stocks.FirstOrDefault(sa => sa.Id == id);
        if (existingStock is null)
            return Task.FromResult(false);

        existingStock.Symbol = stock.Symbol;
        existingStock.Action = stock.Action;
        existingStock.Quantity = stock.Quantity;

        return Task.FromResult(true);
    }

    public Task<bool> DeleteStockAsync(int id)
    {
        var existingStock = _stocks.FirstOrDefault(sa => sa.Id == id);
        if (existingStock is null)
            return Task.FromResult(false);

        _stocks.Remove(existingStock);
        return Task.FromResult(true);
    }
}
