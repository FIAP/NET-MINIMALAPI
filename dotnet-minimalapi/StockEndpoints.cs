using dotnet_minimalapi.Models;

namespace dotnet_minimalapi;

public static class StockEndpoints
{
    public static void MapStockEndpoints(this WebApplication app)
    {
        var stocks = new List<Stock>();

        app.MapGet("/stocks", () => stocks)
            .CacheOutput(policy =>
            {
                policy.SetVaryByRouteValue("param");
                policy.Expire(TimeSpan.FromMinutes(10));
            });

        app.MapGet("/stocks/{id}", (int id) => stocks.FirstOrDefault(sa => sa.Id == id))
            .CacheOutput(policy =>
            {
                policy.Expire(TimeSpan.FromMinutes(10));
            });

        app.MapPost("/stocks", (Stock stockActivity) =>
        {
            stockActivity.Id = stocks.Count + 1;
            stocks.Add(stockActivity);
            return Results.Created($"/stocks/{stockActivity.Id}", stockActivity);
        });

        app.MapPut("/stocks/{id}", (int id, Stock stockActivity) =>
        {
            var existingActivity = stocks.FirstOrDefault(sa => sa.Id == id);
            if (existingActivity != null)
            {
                existingActivity.Symbol = stockActivity.Symbol;
                existingActivity.Action = stockActivity.Action;
                existingActivity.Quantity = stockActivity.Quantity;
                return Results.NoContent();
            }
            return Results.NotFound();
        });

        app.MapDelete("/stocks/{id}", (int id) =>
        {
            var existingActivity = stocks.FirstOrDefault(sa => sa.Id == id);
            if (existingActivity != null)
            {
                stocks.Remove(existingActivity);
                return Results.NoContent();
            }
            return Results.NotFound();
        });
    }
}