using UnityEngine;

[RequireComponent(typeof(PlanetSpawner))]
public class SelectablePlanet : Selectable
{
    private PlanetSpawner planetSpawner;

    public void Start()
    {
        planetSpawner = GetComponent<PlanetSpawner>();
        Initialise();
    }

    void OnMouseDown()
    {
        ToggleSelection();
    }

    public override void UpdateLogic()
    {
        selectImage.SetActive(selectedUuid == uuid && planetSpawner.spawned == true);
    }
}
