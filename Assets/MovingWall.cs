using Pathfinding;
using System.Collections;
using UnityEngine;

public class MovingWall : MonoBehaviour
{
    public Transform startPosition;
    public Transform endPosition;
    public float speed = 2.0f;
    public AstarPath aStar;
    private bool movingToEnd = true;
    public float scanInterval = 5.0f;
    public GameObject movableArea;

    private void Start()
    {
        aStar = GameObject.Find("A*").GetComponent<AstarPath>();
        StartCoroutine(ScanGraphsRegularly());
    }

    void Update()
    {
        float step = speed * Time.deltaTime;

        if (movingToEnd)
        {
            transform.position = Vector3.MoveTowards(transform.position, endPosition.position, step);

            if (Vector3.Distance(transform.position, endPosition.position) < 0.001f)
            {
                movingToEnd = false;
            }
        }
        else
        {
            transform.position = Vector3.MoveTowards(transform.position, startPosition.position, step);

            if (Vector3.Distance(transform.position, startPosition.position) < 0.001f)
            {
                movingToEnd = true;
            }
        }
        
    }
    IEnumerator ScanGraphsRegularly()
    {
        while (true)
        {
            Bounds bounds = movableArea.GetComponent<BoxCollider2D>().bounds;
            
            aStar.UpdateGraphs(bounds, 1f);
            //// Wait for the specified interval
            //yield return new WaitForSeconds(scanInterval);

            //// Trigger a graph scan to update the navigation graphs
            ////aStar.ScanAsync();
            //UpdateGraphRegion();
        }
    }

    void UpdateGraphRegion()
    {
        Bounds bounds = movableArea.GetComponent<BoxCollider2D>().bounds;
        //GraphUpdateObject guo = new GraphUpdateObject(bounds);

        
        //guo.modifyWalkability = true;
        //guo.setWalkability = false;

        aStar.UpdateGraphs(bounds,1f);
    }
}
