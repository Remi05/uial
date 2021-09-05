using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Uial.Contexts
{
    public interface IContextResolver
    {
        IContext ResolveContext(BaseContextDefinition baseContextDefinition);
    }
}
