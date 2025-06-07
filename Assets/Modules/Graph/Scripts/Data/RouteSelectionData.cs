using System.Collections.Generic;
using Modules.Graph.Interfaces;

public class RouteSelectionData
{
    public INode TargetNode;
    public IEnumerable<IEdge> Route;
    public float MoveDuration;
    public float NodeProcessDuration;
    public double NodeProcessResult;
    public float ProfitCoeff;
}