using UnityEngine;
using System.Collections.Generic;

public class Node {

    public List<Node> adjecent = new List<Node>();
    public Node previous = null;
    public string label = "";

    /// <summary>
    /// Function used to clear the previous path.
    /// </summary>
    public void Clear()
    {
        previous = null;
    }
}
