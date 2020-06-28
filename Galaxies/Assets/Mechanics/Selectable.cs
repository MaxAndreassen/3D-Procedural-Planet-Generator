using System;
using UnityEngine;

public class Selectable : MonoBehaviour
{
    public static string selectedUuid;

    public static Vector3 selectedPosition;

    public string uuid;

    public GameObject selectImage;

    public void ToggleSelection()
    {
        if (selectedUuid == uuid)
        {
            selectedUuid = string.Empty;
            selectedPosition = Vector3.zero;
        }
        else
        {
            selectedUuid = uuid;
            selectedPosition = transform.position;
        }
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
