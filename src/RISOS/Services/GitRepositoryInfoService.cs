using Microsoft.Extensions.Options;
using RISOS.Common.Models;
using RISOS.Dto;
using RISOS.Options;

namespace RISOS.Services;

public class GitRepositoryInfoService(IOptions<ApiOptions> options, ApiService apiService)
{
    public async Task<Result<string>> GetLastUpdateDateAsync()
    {
        var result = await apiService.TryRequestAsync<GitRepositoryDto>(client => client.GetAsync(options.Value.GitUrl));

        return result.IsFailure
            ? Result.Failure<string>(result.Error)
            : result.Value.PushedAt;
    }
}