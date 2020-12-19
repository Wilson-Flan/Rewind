using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlatformRaise : MonoBehaviour
{
    public GameObject thing;
    public Transform platform;
    private float finalPos;
    public GameObject disabled;

    public float raise = 50f;
    public bool active;
    private float temp;

    public float speed = 15f;

    public bool raising = false;

    private void Start()
    {
        temp = platform.position.y;
        finalPos = platform.position.y;
        finalPos += raise;
    }

    private void Update()
    {
        if (platform.position.y != temp)
        {
            if (!raising)
            {
                finalPos += platform.position.y - temp;
            }
            temp = platform.position.y;
        }
        if (raising)
        {
            platform.position = platform.position + new Vector3(0, speed * Time.deltaTime, 0);
        }
        if (platform.position.y > finalPos && raising)
        {
            GetComponent<PlatformRaise>().enabled = active;
            if (disabled != null)
            {
                disabled.SetActive(active);
            }
            raising = false;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        thing.SetActive(true);
        raising = true;
    }
}
