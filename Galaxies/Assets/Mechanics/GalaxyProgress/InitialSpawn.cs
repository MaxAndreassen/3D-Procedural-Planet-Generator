using UnityEngine;

[RequireComponent(typeof(PlanetSpawner))]
public class InitialSpawn : MonoBehaviour
{
    public Planet prefab;

    // Start is called before the first frame update
    void Start()
    {
        GetComponent<PlanetSpawner>().Spawn(prefab, 0);
    }
}
