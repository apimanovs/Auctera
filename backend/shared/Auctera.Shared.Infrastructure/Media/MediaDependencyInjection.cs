using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Auctera.Shared.Infrastructure.Media;

public static class MediaDependencyInjection
{
    public static IServiceCollection AddMedia(this IServiceCollection services, IConfiguration configuration)
    {
        services.Configure<MediaOptions>(configuration.GetSection(MediaOptions.SectionName));
        services.AddScoped<IMediaUploader, MediaUploader>();
        return services;
    }
}
