using AutoDependencyRegistration.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UrlShortener.BLL.InterfacesServices;

namespace UrlShortener.BLL.Services
{
    [RegisterClassAsTransient]
    public class GeneratorsShortCodeService: IGeneratorsShortCodeService
    {
        public string GenerateShortCode(int length = 6)
        {
            const string chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz0123456789";
            var random = new Random();
            return new string(Enumerable.Repeat(chars, length)
                .Select(s => s[random.Next(s.Length)]).ToArray());
        }
    }
}
