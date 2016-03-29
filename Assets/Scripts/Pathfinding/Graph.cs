using UnityEngine;
using System.Collections;

public class Graph {

    public int rows = 0;
    public int cols = 0;
    public Node[] nodes;

    /// <summary>
    /// The constructor of the Graph class.
    /// </summary>
    /// <param name="grid">A 2D Grid to create and add elements</param>
    public Graph(int[,] grid)
    {
        rows = grid.GetLength(0);
        cols = grid.GetLength(1);

        nodes = new Node[grid.Length];
        for (var i = 0; i < nodes.Length; ++i)
        {
            var node = new Node();
            node.label = i.ToString();
            nodes[i] = node;
        }

        for (var r = 0; r < rows; ++r)
        {
            for (var c = 0; c < cols; ++c)
            {
                var node = nodes[cols * r + c];

                if (grid[r, c] == 1)
                    continue;

                //Adding the node on the Upside of this one
                if (r>0)
                    node.adjecent.Add(nodes[cols * (r - 1) + c]);

                //Right
                if (c < cols - 1)
                    node.adjecent.Add(nodes[cols * r + c + 1]);

                //Down
                if (r < rows - 1)
                    node.adjecent.Add(nodes[cols * (r + 1) + c]);

                //Left
                if (c > 0)
                    node.adjecent.Add(nodes[cols * r + c - 1]);
                
            }
        }
    }
}
