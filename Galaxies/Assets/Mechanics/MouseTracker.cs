using UnityEngine;

public class MouseTracker : MonoBehaviour
{
    // Update is called once per frame
    void Update()
    {
        var v3 = Input.mousePosition;

        v3 = Camera.main.ScreenToWorldPoint(v3);

        v3.y = 0;

        transform.position = v3;
    }
}
