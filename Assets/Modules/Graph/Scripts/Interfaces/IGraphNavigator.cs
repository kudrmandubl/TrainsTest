using System.Collections.Generic;

namespace Modules.Graph.Interfaces
{
    public interface IGraphNavigator
    {
        IEnumerable<IEdge> FindOptimalRoute(INode start, INode target);
    }
}