using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using System;

public class PathFindingTest : MonoBehaviour {

    public GameObject mapGroup;
    Vector3[] pathFound;

	// Use this for initialization before it starts
	void Awake () {
        int[,] map = new int[10, 10]
        {
            {0,1,0,1,0,0,0,0,0,0},
            {0,1,0,1,0,0,1,0,0,0},
            {0,1,0,1,0,0,1,0,0,0},
            {0,1,0,1,0,0,1,1,1,0},
            {0,1,0,0,0,0,1,0,0,0},
            {0,1,0,0,0,0,1,0,0,0},
            {0,1,1,1,1,1,1,0,0,0},
            {0,0,0,0,0,0,0,0,0,0},
            {0,0,0,1,1,1,1,1,1,0},
            {0,0,0,0,0,0,0,0,0,0}
        };

        var graph = new Graph(map);

        var search = new Search(graph);
        search.Start(graph.nodes[0], graph.nodes[2]);

        while(!search.finished)
            search.Step();

        //print("Search done. Path length :" + search.path.Count + " / iterations :" + search.iterations);

        ResetMapGroup(graph);

        var tempV3 = new Vector3[search.path.Count];
        var count = 0;

        //print("The next following elements will be the path found.");
        foreach(var node in search.path)
        {
            getImage(node.label).color = Color.red;
            tempV3[count++] = getPos(node.label);
        }
        pathFound = tempV3;
	}
	
    Image getImage(string label)
    {
        //print(label);
        var id = Int32.Parse(label);
        var go = mapGroup.transform.GetChild(id).gameObject;
        //var temp = go.transform.parent.TransformDirection(go.transform.localPosition);
        //var temp = Camera.main.ScreenToWorldPoint(go.GetComponent<RectTransform>().localPosition);
        //var temp = mapGroup.transform.GetChild(id).transform.localPosition;

        //var temp = mapGroup.GetComponent<GridLayoutGroup>();

        //Vector3 toadd = new Vector3((id % 10)*3f+.5f, -(id / 10)*3f+.5f, 0);

        //Vector3 pos = mapGroup.transform.parent.position + toadd;
        Vector3 pos = go.GetComponent<RectTransform>().InverseTransformPoint(go.GetComponent<RectTransform>().anchoredPosition3D);
        //print(pos);

        //print("Name : " + go.name+" x :" + pos.x + " y : " + pos.y);
        return go.GetComponent<Image>();
    }

    Vector3 getPos(string label)
    {
        var id = Int32.Parse(label);
        Vector3 toadd = new Vector3(id % 10, -id / 10, 0);
        return mapGroup.transform.parent.position + toadd;
    }

    void ResetMapGroup(Graph graph)
    {
        foreach(var node in graph.nodes)
        {
            getImage(node.label).color = node.adjecent.Count == 0 ? Color.white : new Color(0,0,0,0);
        }
    }

	public Vector3[] getPath()
    { return pathFound; }
}
