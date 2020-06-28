using System;
using UnityEngine;

public class Selectable : MonoBehaviour
{
    public static string selectedUuid;

    public string uuid;

    public GameObject selectImage;

    public void ToggleSelection()
    {
        if (selectedUuid == uuid)
            selectedUuid = new Guid().ToString();
        else
            selectedUuid = uuid;
    }

    public void Initialise()
    {
        uuid = Guid.NewGuid().ToString();
    }

    // Update is called once per frame
    void Update()
    {
        UpdateLogic();
    }

    public virtual void UpdateLogic()
    {
        selectImage.SetActive(selectedUuid == uuid);
    }
}
