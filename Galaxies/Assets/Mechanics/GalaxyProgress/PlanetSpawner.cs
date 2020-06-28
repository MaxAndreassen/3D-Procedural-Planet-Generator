using UnityEngine;

public class PlanetSpawner : MonoBehaviour
{
    [HideInInspector]
    public bool spawned = false;

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

        spawned = true;

        instance.GeneratePlanet();

        instance.transform.parent = transform;

        instance.transform.localPosition = Vector3.zero;

        gameObject.AddComponent<SphereCollider>().radius = 4.5f;

    }
}
