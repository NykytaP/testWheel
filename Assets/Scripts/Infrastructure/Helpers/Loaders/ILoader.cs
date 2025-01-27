using System;
namespace Infrastructure.Helpers.Loaders
{
    public interface ILoader : IDisposable
    {
        new void Dispose();
    }
}