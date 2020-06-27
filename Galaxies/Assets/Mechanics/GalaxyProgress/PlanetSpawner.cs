using UnityEngine;

public class PlanetSpawner : MonoBehaviour
{
    // Start is called before the first frame update
    public void Spawn(Planet prefab, int roughness)
    {
        transform.localPosition = new Vector3
        (
            transform.localPosition.x + Random.Range(-roughness, roughness),
            transform.localPosition.y,
            transform.localPosition.z + Random.Range(-roughness, roughness)
        );

        var instance = Instantiate(prefab);

        instance.GeneratePlanet();

        instance.transform.parent = transform;

        instance.transform.localPosition = Vector3.zero;
    }
}
