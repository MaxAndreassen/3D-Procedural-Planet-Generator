using UnityEngine;

public class PlanetSpawner : MonoBehaviour
{
    public Planet prefab;

    // Start is called before the first frame update
    void Start()
    {
        var instance = Instantiate(prefab);

        instance.GeneratePlanet();

        instance.transform.parent = transform;

        instance.transform.localPosition = Vector3.zero;
    }
}
