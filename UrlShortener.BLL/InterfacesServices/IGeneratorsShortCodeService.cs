using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UrlShortener.BLL.InterfacesServices
{
    public interface IGeneratorsShortCodeService
    {
        string GenerateShortCode(int length = 6);
    }
}
