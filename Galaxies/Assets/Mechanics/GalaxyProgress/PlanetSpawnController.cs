using System.Collections;
using System.Linq;
using UnityEngine;

public class PlanetSpawnController : MonoBehaviour
{
    public int galaxyWidth;
    public int galaxyHeight;
    public int density;
    public int roughness;

    public PlanetSpawner spawnerPrefab;

    public Planet[] planetPrefabs;

    public PlanetSpawner[] planetSpawners;

    // Start is called before the first frame update
    void Start()
    {
        SpawnSpawners();
        RandomizeSpawners();
        StartCoroutine(SpawnLoop());
    }

    public void SpawnSpawners()
    {
        planetSpawners = new PlanetSpawner[galaxyHeight * galaxyWidth * 4];

        for (var x = 0; x < galaxyWidth * 2; x++)
        {
            for (var y = 0; y < galaxyHeight * 2; y++)
            {
                var spawner = Instantiate(spawnerPrefab);

                spawner.transform.localPosition = new Vector3((x - galaxyWidth) * density, 0, (y - galaxyHeight) * density);

                planetSpawners[(x * galaxyWidth * 2) + y] = spawner;
            }
        }
    }

    public void RandomizeSpawners()
    {
        var rnd = new System.Random();
        planetSpawners = planetSpawners.OrderBy(x => rnd.Next()).ToArray();
    }

    IEnumerator SpawnLoop ()
    {
        var i = 0;

        while (i < planetSpawners.Length)
        {
            yield return new WaitForSeconds(0.2f);

            planetSpawners[i].Spawn(planetPrefabs[Random.Range(0, planetPrefabs.Length)], roughness);

            i++;
        }
    }
}
