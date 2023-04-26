using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LightRoop : MonoBehaviour
{
    [Range(0, 100)]
    public float range = 0;

    public float startRange = 0;
    public float pingpongSpeed = 0;

    private float lightRange = 0;
    private Light light;
    // Start is called before the first frame update
    void Start()
    {
        light = this.transform.GetComponent<Light>();
    }

    // Update is called once per frame
    void Update()
    {
        lightRange += Time.deltaTime * pingpongSpeed;
        light.range = Mathf.PingPong(lightRange, range) / 10 + startRange;
    }
}
