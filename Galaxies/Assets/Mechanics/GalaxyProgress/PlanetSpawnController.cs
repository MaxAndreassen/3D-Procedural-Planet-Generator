using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using static Planet;

public class PlanetSpawnController : MonoBehaviour
{
    public int galaxyWidth;
    public int galaxyHeight;
    public int density;
    public int roughness;

    public PlanetSpawner spawnerPrefab;

    public Planet[] planetPrefabs;

    public PlanetSpawner[] planetSpawners;

    Dictionary<PlanetType, float> planetsDictionary = new Dictionary<PlanetType, float>();

    // Start is called before the first frame update
    void Start()
    {
        SpawnSpawners();
        RandomizeSpawners();
        InitialiseDictionary();
        StartCoroutine(SpawnLoop());
    }

    public void InitialiseDictionary()
    {
        foreach (var planetType in planetPrefabs.Select(p => p.planetType).Distinct())
        {
            planetsDictionary.Add(planetType, 1f);
        }
    }

    public void SpawnSpawners()
    {
        planetSpawners = new PlanetSpawner[(galaxyHeight * 2) * (galaxyWidth * 2)];

        for (var x = 0; x < galaxyWidth * 2; x++)
        {
            for (var y = 0; y < galaxyHeight * 2; y++)
            {
                var spawner = Instantiate(spawnerPrefab);

                var random = Random.Range(0, 5);

                if (random == 0)
                    spawner.gameObject.SetActive(false);

                spawner.transform.localPosition = new Vector3((x - galaxyWidth) * density, 0, (y - galaxyHeight) * density);

                if (Mathf.Abs(((x - galaxyWidth) * density) + ((y - galaxyHeight) * density)) < 0.1)
                    spawner.gameObject.SetActive(false);

                planetSpawners[(x * galaxyHeight * 2) + y] = spawner;
            }
        }
    }

    public void RandomizeSpawners()
    {
        var rnd = new System.Random();

        planetSpawners = planetSpawners
            .OrderBy(x => Vector3.Distance(Camera.main.gameObject.transform.position, x.transform.position))
            .ToArray();
    }

    IEnumerator SpawnLoop ()
    {
        var i = 0;

        while (i < planetSpawners.Length)
        {
            if (!planetSpawners[i].isActiveAndEnabled)
            {
                i++;
                continue;
            }

            yield return new WaitForSeconds(6f);

            var randomValue = Random.Range(0f, 1f);

            float maxRatio = planetsDictionary.Sum(p => p.Value);

            var ratioModifier = 0f;

            var planetType = PlanetType.Desert;

            foreach (var keyValuePair in planetsDictionary)
            {
                float ratio = (keyValuePair.Value / maxRatio) + ratioModifier;

                Debug.Log(i.ToString() + " - " + randomValue.ToString() + " - " + ratio.ToString() + keyValuePair.Key.ToString());

                ratioModifier += ratio;

                if (randomValue < ratio)
                {
                    planetType = keyValuePair.Key;
                    break;
                }
            }

            for (var x = 0; x < planetsDictionary.Count; x++)
            {
                var key = planetsDictionary.ElementAt(x).Key;

                if (key== planetType)
                    continue;

                planetsDictionary[key] = planetsDictionary[key] + 5;
            }

            var planets = planetPrefabs.Where(p => p.planetType == planetType).ToList();

            planetSpawners[i].Spawn(planets[Random.Range(0, planets.Count)], roughness);

            i++;
        }
    }
}
