using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnSpheres : MonoBehaviour
{
    public GameObject sphere;
    public int amount = 10;
    public float radius;
    public Vector3 center;

    void Awake()
    {
        DrawSpheres(center, radius, amount);
    }

    // Draw a sphere in the XY plane with a specfied position, number of sides
    // and radius.
    void DrawSpheres(Vector3 center, float radius, int numSides)
    {
        Vector3 startCorner = new Vector3(radius, 0) + center;

        Vector3 previousCorner = startCorner;

        // For each corner after the starting corner...
        for (int i = 1; i < numSides; i++)
        {
            // Calculate the angle of the corner in radians.
            float cornerAngle = 2f * Mathf.PI / (float)numSides * i;
            
            // Get the X and Y coordinates of the corner point.
            Vector3 currentCorner = new Vector3(Mathf.Cos(cornerAngle) * radius, Mathf.Sin(cornerAngle) * radius) + center;

            //Place the sphere
            Spawn(currentCorner);

            // Having used the current corner, it now becomes the previous corner.
            previousCorner = currentCorner;
        }
    }

    private void Spawn(Vector3 position)
    {
        GameObject newSphere = Instantiate(sphere, position, Quaternion.identity);
    }

    private List<GameObject> SpawnSpheresList()
    {
        return new List<GameObject>(amount);
    }
}
