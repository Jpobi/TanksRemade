using System.Collections.Generic;
using System.Linq;
using Unity.VisualScripting;
using UnityEngine;

[RequireComponent(typeof(PolygonCollider2D))]
public class DeformableCollider : MonoBehaviour
{
    private PolygonCollider2D colisionado;
    private Vector2[] originalVertices;

    public Material material;

    void Start()
    {
        colisionado = GetComponent<PolygonCollider2D>();
        material.color = Color.grey;
        GetComponent<MeshRenderer>().material=material;
        originalVertices = colisionado.points; // Store the original collider vertices
        UpdateMesh();
    }

    public void Deform(Vector2 impactPoint, Vector3 direction, float deformationRadius)
    {
        originalVertices = colisionado.points;
        Vector2 localImpactPoint = ConvertWorldToLocal(impactPoint);

        //Calculate the affected vertices and deform them
        for (int i = 0; i < originalVertices.Length; i++)
        {
            Vector2 vertex = originalVertices[i];
            float distance = Vector2.Distance(localImpactPoint, vertex);

            if (distance < deformationRadius)
            {
                vertex.Set((float)(vertex.x + (direction.normalized * 0.1f).x), (float)(vertex.y + (direction.normalized * 0.1f).y));
                originalVertices[i] = vertex;
            }
        }
        Vector2 newVertex = new Vector2(localImpactPoint.x + (direction.normalized * 0.1f).x, localImpactPoint.y + (direction.normalized * 0.1f).y);
        var newVertices = originalVertices.ToList<Vector2>();

        int indexBefore = PointsBetween(localImpactPoint);
        newVertices.Insert(indexBefore,newVertex);

        // Update the collider's shape with the modified vertices
        colisionado.points = newVertices.ToArray();
        //colisionado.SetPath(colisionado.pathCount-1, new[] { indexBefore != -1 ? originalVertices[indexBefore - 1] : originalVertices.Last(), indexBefore != -1 && indexBefore != originalVertices.Length - 1 ? originalVertices[indexBefore] : newVertex, originalVertices.First() });
        colisionado.SetPath(0, OrderPointsClockwise(newVertices));
        UpdateMesh();
    }

    Vector2 ConvertWorldToLocal(Vector2 worldPoint)
    {
        // Get the inverse of the GameObject's transformation matrix
        Matrix4x4 inverseTransformMatrix = colisionado.transform.worldToLocalMatrix;

        // Convert the world-space point to local-space using the inverse transformation matrix
        Vector2 localPoint = inverseTransformMatrix.MultiplyPoint(worldPoint);
        //Debug.Log("Local Point: " + localPoint); 
        return localPoint;
    }

    int PointsBetween(Vector2 worldPointToFind)
    {
        Vector2 localPoint = colisionado.transform.InverseTransformPoint(worldPointToFind);

        Vector2[] colliderPoints = colisionado.points;
        int pointCount = colliderPoints.Length;

        for (int i = 0; i < pointCount; i++)
        {
            Vector2 startPoint = colliderPoints[i];
            Vector2 endPoint = colliderPoints[(i + 1) % pointCount]; // Wrap around to the first point for the last pair

            // Calculate the vectors representing the line and the point relative to the start of the line
            Vector2 lineDirection = endPoint - startPoint;
            Vector2 pointDirection = localPoint - startPoint;

            // Calculate the dot product to determine the position of the point relative to the line
            float dotProduct = Vector2.Dot(lineDirection, pointDirection);

            if (dotProduct < 0)
            {
                //Debug.Log("Point is between points " + i + " and " + (i + 1));
                return i;
            }
        }
        return -1;
    }


    void UpdateMesh()
    {
        MeshFilter meshFilter = GetComponent<MeshFilter>();
        Mesh mesh = colisionado.CreateMesh(true, true);


        Vector3[] scaledVertices = new Vector3[mesh.vertices.Length];
        int[] newTriangles = new int[mesh.triangles.Length+1];
        for (int i = 0; i < scaledVertices.Length; i++)
        {
            scaledVertices[i] = colisionado.transform.InverseTransformPoint(mesh.vertices[i]);
            if(i%3 == 0 || i==mesh.vertices.Length-3 || i == mesh.vertices.Length - 2)
            {
                newTriangles[i] = i;
                newTriangles[i+1] = i+1;
                newTriangles[i+2] = i+2;
            }
        }
        mesh.vertices = scaledVertices;
        mesh.triangles = newTriangles;

        // Recalculate bounds and normals
        mesh.RecalculateBounds();
        mesh.RecalculateNormals();
        meshFilter.mesh = mesh;
    }

    public List<Vector2> OrderPointsClockwise(List<Vector2> points)
    {
        // Calculate the centroid of the points
        Vector2 centroid = Vector2.zero;
        foreach (Vector2 point in points)
        {
            centroid += point;
        }
        centroid /= points.Count;

        // Calculate the angles between the centroid and the points
        List<KeyValuePair<Vector2, float>> pointAngles = new List<KeyValuePair<Vector2, float>>();
        foreach (Vector2 point in points)
        {
            Vector2 fromCentroid = point - centroid;
            float angle = Mathf.Atan2(fromCentroid.y, fromCentroid.x);
            pointAngles.Add(new KeyValuePair<Vector2, float>(point, angle));
        }

        // Sort the points based on the angles
        pointAngles.Sort((a, b) => a.Value.CompareTo(b.Value));

        // Extract the sorted points
        List<Vector2> sortedPoints = new List<Vector2>();
        foreach (var pair in pointAngles)
        {
            sortedPoints.Add(pair.Key);
        }

        return sortedPoints;
    }

}
