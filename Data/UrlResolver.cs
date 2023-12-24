using AutoMapper;
using ELabel.Models;
using ELabel.ViewModels;
using Microsoft.AspNetCore.Hosting.Server;
using Microsoft.AspNetCore.Hosting.Server.Features;

namespace ELabel.Data
{
    public interface IValueResolver<in TSource, in TDestination, TDestMember>
    {
        TDestMember Resolve(TSource source, TDestination destination, TDestMember destMember, ResolutionContext context);
    }

    public class UrlResolver : IValueResolver<Product, ProductExcelDto, string>
    {
        private readonly string? _address;

        public UrlResolver(IServer server)
        {
            _address = server.Features.Get<IServerAddressesFeature>()?.Addresses.FirstOrDefault();
        }

        public string Resolve(Product source, ProductExcelDto target, string destMember, ResolutionContext context)
        {
            return $"{_address}/l/{source.GetCode()}";
        }
    }
}
