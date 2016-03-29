using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Search {

    public Graph graph;

    public List<Node> reachable;
    public List<Node> explored;
    public List<Node> path;

    public Node goalNode;
    public int iterations;
    public bool finished;

    /// <summary>
    /// Public constructor of the Search class.
    /// </summary>
    /// <param name="graph">The graph we need to search in.</param>
    public Search(Graph graph)
    {
        this.graph = graph;
    }

    /// <summary>
    /// Function to start the pathfinding.
    /// </summary>
    /// <param name="start">Node where the path starts.</param>
    /// <param name="goal">Node where the path ends.</param>
    public void Start(Node start, Node goal)
    {
        reachable = new List<Node>();
        reachable.Add(start);

        goalNode = goal;

        explored = new List<Node>();
        path = new List<Node>();

        iterations = 0;

        for(var i = 0; i < graph.nodes.Length; ++i)
        {
            graph.nodes[i].Clear();
        }
    }

    public void Step()
    {
        //Checking if we already reached the destination
        if (path.Count > 0)
            return;

        //If we can't reach, we consider that it's finished.
        if (reachable.Count == 0)
        {
            finished = true;
            return;
        }

        ++iterations;

        var node = ChoseNode();
        //Traversing the nodes
        if (node == goalNode)
        {
            while (node != null)
            {
                path.Insert(0, node);
                node = node.previous;
            }
            finished = true;
            return;
        }

        reachable.Remove(node);
        explored.Add(node);

        for(var i = 0; i < node.adjecent.Count; ++i)
            AddAdjacent(node, node.adjecent[i]);
        

    }

    public void AddAdjacent(Node node, Node adjacent)
    {
        if (FindNode(adjacent, explored) || FindNode(adjacent, reachable))
            return;

        adjacent.previous = node;
        reachable.Add(adjacent);
    }

    public bool FindNode(Node node, List<Node> list)
    {
        return (getNodeIndex(node, list) >= 0);
    }

    public int getNodeIndex(Node node, List<Node> list)
    {
        for (var i =0; i < list.Count; ++i)
        {
            if (node == list[i])
                return i;
        }

        return -1;
    }

    /// <summary>
    /// Function which choses a random node with all the paths he can have.
    /// </summary>
    /// <returns>Returns a random node.</returns>
    public Node ChoseNode()
    { return reachable[Random.Range(0, reachable.Count)]; }
}
