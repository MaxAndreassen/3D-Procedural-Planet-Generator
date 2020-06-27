using System.Linq;
using UnityEngine;

[RequireComponent(typeof(PlanetSpawnController))]
public class CameraController : MonoBehaviour
{
    PlanetSpawnController psController;

    private void Start()
    {
        psController = GetComponent<PlanetSpawnController>();
    }

    // Update is called once per frame
    void Update()
    {
        var largestX = psController.planetSpawners
            .Where(p => p.isActiveAndEnabled && p.spawned == true)
            .Max(p => Mathf.Abs(p.transform.localPosition.x) * 0.6f);

        var largestZ = psController.planetSpawners
            .Where(p => p.isActiveAndEnabled && p.spawned == true)
            .Max(p => Mathf.Abs(p.transform.localPosition.z));

        var max = Mathf.Max(largestX, largestZ);

        if (max > Camera.main.orthographicSize)
            Camera.main.orthographicSize = max;
    }
}
