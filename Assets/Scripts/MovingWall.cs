using Pathfinding;
using System.Collections;
using UnityEngine;

public class MovingWall : MonoBehaviour
{
    public Transform startPosition;
    public Transform endPosition;
    public float speed = 2.0f;
    public AstarPath aStar;
    private bool haciaFin = true;
    public float scanInterval = 0.25f;
    public GameObject movableArea;

    private void Start()
    {
        aStar = GameObject.Find("A*").GetComponent<AstarPath>();
        StartCoroutine(ScanGraphsRegularly());
    }

    void Update()
    {
        float step = speed * Time.deltaTime;

        if (haciaFin)
        {
            transform.position = Vector3.MoveTowards(transform.position, endPosition.position, step);

            if (Vector3.Distance(transform.position, endPosition.position) < 0.001f)
            {
                haciaFin = false;
            }
        }
        else
        {
            transform.position = Vector3.MoveTowards(transform.position, startPosition.position, step);

            if (Vector3.Distance(transform.position, startPosition.position) < 0.001f)
            {
                haciaFin = true;
            }
        }
        
    }
    IEnumerator ScanGraphsRegularly()
    {
        while (true)
        {
            yield return new WaitForSeconds(scanInterval);

            //aStar.ScanAsync();
            if (!aStar.IsAnyGraphUpdateQueued && !aStar.IsAnyGraphUpdateInProgress) UpdateGraphRegion();
        }
    }

    void UpdateGraphRegion()
    {
        Bounds bounds = movableArea.GetComponent<BoxCollider2D>().bounds;
        //GraphUpdateObject guo = new GraphUpdateObject(bounds);

        //guo.modifyWalkability = true;
        //guo.setWalkability = false;

        aStar.UpdateGraphs(bounds);
    }
}
