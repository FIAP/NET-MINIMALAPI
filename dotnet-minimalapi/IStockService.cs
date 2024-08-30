using dotnet_minimalapi.Models;

namespace dotnet_minimalapi
{
    public interface IStockService
    {
        Task<List<Stock>> GetAllStocksAsync();
        Task<Stock?> GetStockByIdAsync(int id);
        Task AddStockAsync(Stock stock);
        Task<bool> UpdateStockAsync(int id, Stock stock);
        Task<bool> DeleteStockAsync(int id);
    }
}
