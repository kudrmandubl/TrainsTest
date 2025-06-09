using System.Collections.Generic;

namespace Modules.Graph.Interfaces
{
    public interface IRouteShower
    {
        void ShowRoute(IEnumerable<IEdge> route);
    }
}