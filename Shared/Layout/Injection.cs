﻿using Microsoft.Extensions.DependencyInjection;
namespace OSIC.Shared.Layout;
public static class Injection
{
    public static IServiceCollection OSICSharedLayoutInjection(this IServiceCollection Services) {

        return Services;
    }
}
