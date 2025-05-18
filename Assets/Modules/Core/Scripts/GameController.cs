using Zenject;
using System.Collections.Generic;

public class GameController
{
    [Inject] private IGraph _graph;
    [Inject] private IGraphNavigator _routeCalculator;
    [Inject] private IMineralManager _mineralManager;
    [Inject] private ITrain _train;
    
    private IGraphSpawner _graphSpawner;

    public GameController(IGraphSpawner graphSpawner)
    {
        _graphSpawner = graphSpawner;
        graphSpawner.SpawnGraph();

        // ������� ������ �����
        //var nodes = new List<INode>();
        //var nodeA = new Node(1, NodeType.Base, 1.0f);
        //var nodeB = new Node(2, NodeType.Mine, 1.2f);
        //var nodeC = new Node(3, NodeType.Mine, 0.8f);

        //nodes.Add(nodeA);
        //nodes.Add(nodeB);
        //nodes.Add(nodeC);

        //var graph = new Graph();
        //foreach (var node in nodes)
        //{
        //    graph.AddNode(node);
        //}

        //var edgeAB = new Edge(nodeA, nodeB, 10f);
        //var edgeBC = new Edge(nodeB, nodeC, 15f);
        //var edgeAC = new Edge(nodeA, nodeC, 20f);

        //graph.AddEdge(edgeAB);
        //graph.AddEdge(edgeBC);
        //graph.AddEdge(edgeAC);

        //// ��������� ���������� �����������
        //_graph = graph;

        //// �������� ��������
        //var startNode = nodeA;
        //var targetNode = nodeC;

        //var route = _routeCalculator.FindOptimalRoute(startNode, targetNode);
        //_train.AssignRoute(route);
        //_train.Start();

        //// ������ � ���������
        //_mineralManager.AddMinerals(100);
        //_mineralManager.ApplyMultiplier(1.5f);
        //UnityEngine.Debug.Log($"�������: {_mineralManager.Minerals.Quantity}");
    }
}