using System.Collections.Generic;

public interface ITrain
{
    void AssignRoute(IEnumerable<IEdge> route);
    void Start();
    void Stop();
}