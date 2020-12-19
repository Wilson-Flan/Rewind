using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnablePlate : MonoBehaviour
{
    public GameObject thingy;
    public Transform thing;
    private void Update()
    {
        if (43.8f < thing.position.y && thing.position.y < 44.2f)
        {
            thingy.SetActive(true);
        }
    }
}
