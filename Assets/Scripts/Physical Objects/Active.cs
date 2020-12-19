using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Active : MonoBehaviour
{
    public GameObject thing;
    private void OnTriggerEnter(Collider other)
    {
        thing.SetActive(false);
    }
}
