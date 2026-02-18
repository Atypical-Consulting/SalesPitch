using SalesPitch.WebApp.Models;

namespace SalesPitch.WebApp.Services;

public interface ISalesPitchService
{
    Task<string> GenerateSalesPitchAsync(SalesPitchRequest request, CancellationToken cancellationToken = default);
    Task<IAsyncEnumerable<string>> GenerateSalesPitchStreamAsync(SalesPitchRequest request, CancellationToken cancellationToken = default);
}