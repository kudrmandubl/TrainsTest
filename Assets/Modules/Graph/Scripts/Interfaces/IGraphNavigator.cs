using System.Collections.Generic;

public interface IGraphNavigator
{
    IEnumerable<IEdge> FindOptimalRoute(INode start, INode target);
}