using System.Collections.Generic;
using UnityEngine;

public class HyperspaceRoute : MonoBehaviour
{
    public List<HyperspaceDestination> Planets = new List<HyperspaceDestination>();

    public float width;

    public void Update()
    {
        gameObject.GetComponent<MeshFilter>().sharedMesh = new Mesh();

        var mesh = gameObject.GetComponent<MeshFilter>().sharedMesh;

        var vertices = new Vector3[Planets.Count * 4];

        for (var p = 0; p < Planets.Count; p++)
        {
            vertices[(p * 4) + 0] = new Vector3(Planets[p].transform.position.x + width, 0, Planets[p].transform.position.z + width);
            vertices[(p * 4) + 1] = new Vector3(Planets[p].transform.position.x - width, 0, Planets[p].transform.position.z + width);
            vertices[(p * 4) + 2] = new Vector3(Planets[p].transform.position.x - width, 0, Planets[p].transform.position.z - width);
            vertices[(p * 4) + 3] = new Vector3(Planets[p].transform.position.x + width, 0, Planets[p].transform.position.z - width);
        }

        var triangles = new int[12 * (Planets.Count - 1)];

        for (var p = 0; p < Planets.Count - 1; p++)
        {
            triangles[(p * 12) + 0] = (p * 4) + 0;
            triangles[(p * 12) + 1] = (p * 4) + 1;
            triangles[(p * 12) + 2] = ((p + 1) * 4) + 0;

            triangles[(p * 12) + 3] = (p * 4) + 1;
            triangles[(p * 12) + 4] = ((p + 1) * 4) + 0;
            triangles[(p * 12) + 5] = ((p + 1) * 4) + 1;

            triangles[(p * 12) + 6] = (p * 4) + 1;
            triangles[(p * 12) + 7] = (p * 4) + 2;
            triangles[(p * 12) + 8] = ((p + 1) * 4) + 1;

            triangles[(p * 12) + 9] = (p * 4) + 2;
            triangles[(p * 12) + 10] = ((p + 1) * 4) + 1;
            triangles[(p * 12) + 11] = ((p + 1) * 4) + 2;
        }

        mesh.Clear();
        mesh.vertices = vertices;
        mesh.triangles = triangles;
        mesh.RecalculateNormals();
    }
}
