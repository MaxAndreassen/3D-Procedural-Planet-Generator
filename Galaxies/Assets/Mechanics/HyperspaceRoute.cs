using UnityEngine;

public class HyperspaceRoute : MonoBehaviour
{
    public float width;

    public void Update()
    {
        var mesh = gameObject.GetComponent<MeshFilter>().sharedMesh;

        if (string.IsNullOrEmpty(SelectablePlanet.selectedUuid))
        {
            mesh.Clear();
            return;
        }

        var vertices = new Vector3[4];
        var triangles = new int[6];

        var position1 = SelectablePlanet.selectedPosition;
        var position2 = Input.mousePosition;
        position2 = Camera.main.ScreenToWorldPoint(position2);

        vertices[0] = position1 + new Vector3(width, 0, 0);
        vertices[1] = position1 + new Vector3(-width, 0, 0);
        vertices[2] = position2 + new Vector3(width, 0, 0);
        vertices[3] = position2 + new Vector3(-width, 0, 0);

        triangles[0] = 0;
        triangles[1] = 1;
        triangles[2] = 2;
        triangles[3] = 1;
        triangles[4] = 2;
        triangles[5] = 3;

        mesh.Clear();
        mesh.vertices = vertices;
        mesh.triangles = triangles;
        mesh.RecalculateNormals();
    }
}
