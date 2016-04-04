using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using System;

public class PathFindingTest : MonoBehaviour {

    public GameObject mapGroup;

	// Use this for initialization
	void Start () {
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

        print("Search done. Path length :" + search.path.Count + " / iterations :" + search.iterations);

        ResetMapGroup(graph);

        foreach(var node in search.path)
        {
            getImage(node.label).color = Color.red;
        }
	}
	
    Image getImage(string label)
    {
        var id = Int32.Parse(label);
        var go = mapGroup.transform.GetChild(id).gameObject;
        //var temp = go.transform.parent.TransformDirection(go.transform.localPosition);
        //var temp = Camera.main.ScreenToWorldPoint(go.GetComponent<RectTransform>().localPosition);
        //var temp = mapGroup.transform.GetChild(id).transform.localPosition;

        //var temp = mapGroup.GetComponent<GridLayoutGroup>();

        Vector3 pos = mapGroup.transform.parent.position + mapGroup.transform.localPosition;
        print(pos);

        //print("Name : " + go.name+" x :" + temp.x + " y : " + temp.y);
        return go.GetComponent<Image>();
    }

    void ResetMapGroup(Graph graph)
    {
        foreach(var node in graph.nodes)
        {
            getImage(node.label).color = node.adjecent.Count == 0 ? Color.white : Color.gray;   
        }
    }

	// Update is called once per frame
	void Update () {
	    
	}
}
