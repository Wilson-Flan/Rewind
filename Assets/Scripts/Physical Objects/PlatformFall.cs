using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlatformFall : MonoBehaviour
{
    public GameObject thing;
    public Transform platform;
    private float finalPos;
    public GameObject disabled;

    public float fall = 50f;
    public bool active;
    private float temp;

    public float speed = 15f;

    public bool falling = false;

    private void Start()
    {
        temp = platform.position.y;
        finalPos = platform.position.y;
        finalPos -= fall;
    }

    private void Update()
    {
        if (platform.position.y != temp)
        {
            if (!falling)
            {
                finalPos += platform.position.y - temp;
            }
            temp = platform.position.y;
        }
        if (falling)
        {
            platform.position = platform.position + new Vector3(0, -speed * Time.deltaTime, 0);
        }
        if (platform.position.y < finalPos && falling)
        {
            if (platform.position.y < 0)
            {
                GetComponent<PlatformFall>().enabled = active;
                if (disabled != null)
                {
                    disabled.SetActive(false);
                }
                thing.SetActive(false);
            }
            falling = false;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        falling = true; 
    }
}
