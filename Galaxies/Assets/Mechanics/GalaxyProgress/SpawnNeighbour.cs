using System.Collections;
using System.Linq;
using UnityEngine;
using static Planet;

[RequireComponent(typeof(PlanetSpawner))]
public class SpawnNeighbour : MonoBehaviour
{
    public PlanetSpawner planetSpawnerPrefab;

    public float maxDistance;
    public float minDistance;
    public int maxNeighbours;

    private void Start()
    {
        StartCoroutine(SpawnLoop());
    }

    IEnumerator SpawnLoop()
    {
        var planetSpawner = GetComponent<PlanetSpawner>();

        yield return new WaitForSeconds(4f);

        var currentPlanetType = planetSpawner.planet.planetType;

        var planetPrefabs = FindObjectOfType<PlanetSpawnController>().planetPrefabs;

        var acceptablePlanetPrefabs = planetPrefabs
            .Where(p => p.planetType != currentPlanetType)
            .ToList();

        var otherSpawners = FindObjectsOfType<PlanetSpawner>()
            .Where(p => Mathf.Abs(p.transform.position.x - transform.position.x) < maxDistance)
            .Where(p => Mathf.Abs(p.transform.position.z - transform.position.z) < maxDistance)
            .ToList();

        if (otherSpawners.Count > maxNeighbours)
            yield break;

        var position = NeighbourPosition(transform.position);

        var positionFound = false;

        for (var i = 0; i < 50; i++)
        {
            var collidingSpawners = otherSpawners
            .Where(p => Mathf.Abs(p.transform.position.x - position.x) < minDistance)
            .Where(p => Mathf.Abs(p.transform.position.z - position.z) < minDistance);

            Debug.Log(collidingSpawners.Count());

            if (!collidingSpawners.Any())
            {
                positionFound = true;
                break;
            }

            position = NeighbourPosition(transform.position);
        }

        if (!positionFound)
            yield break;

        var newSpawner = Instantiate(planetSpawnerPrefab);

        newSpawner.transform.position = position;

        newSpawner.Spawn(acceptablePlanetPrefabs[Random.Range(0, acceptablePlanetPrefabs.Count)], 0);

    }

    Vector3 NeighbourPosition (Vector3 basePos)
    {
        var positiveX = Random.Range(0, 2) == 1 ? 1 : -1;
        var positiveZ = Random.Range(0, 2) == 1 ? 1 : -1;

        return new Vector3(
            basePos.x + Random.Range(minDistance, maxDistance) * positiveX,
            basePos.y,
            basePos.z + Random.Range(minDistance, maxDistance) * positiveZ
        );
    }
}
