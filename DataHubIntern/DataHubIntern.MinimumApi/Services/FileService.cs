using System.Globalization;
using System.Net;
using System.Text;
using CsvHelper;
using DataHubIntern.MinimumApi.Repository;
using DataHubIntern.Shared.File;
using DataHubIntern.Shared.Identity;

namespace DataHubIntern.MinimumApi.Services;

public class FileService(IIdentityRepository identityRepository, IIdentityService identityService)
    : IFileService
{
    public async Task<UploadResponse> UploadAsync(IAsyncEnumerable<UploadRequest> request, CancellationToken cancellationToken = default)
    {
        await using var ms = new MemoryStream();

        await foreach (var message in request.WithCancellation(cancellationToken))
            await ms.WriteAsync(message.ChunkData, cancellationToken);
        ms.Seek(0, SeekOrigin.Begin);

        using var fs = new StreamReader(ms, Encoding.UTF8);
        using var csv = new CsvReader(fs, CultureInfo.InvariantCulture);

        var line = 0;
        var successes = new List<string>();
        var errors = new Dictionary<int, List<string>>();

        var records = await csv.GetRecordsAsync<RawData>(cancellationToken).ToListAsync(cancellationToken);

        // Start here !

        return new UploadResponse { StatusCode = HttpStatusCode.MultiStatus, Successes = successes, Errors = errors };
    }

    private static bool TryValidate(RawData record, out List<string> errors)
    {
        throw new NotImplementedException("ファイルのバリデーションが実装されていません。");
    }
}
