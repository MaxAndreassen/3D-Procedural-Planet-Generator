using UnityEngine;

public class Rotate : MonoBehaviour
{
    public float speed = 1f;

    float modifier = 1f;

    private void Start()
    {
        modifier = Random.Range(0.5f, 1f);

        var flip = Random.Range(0, 2);

        if (flip == 1)
            modifier *= -1;
    }

    // Update is called once per frame
    void Update()
    {
        transform.Rotate(new Vector3(Time.deltaTime * speed * modifier, Time.deltaTime * speed, 0));
    }
}
