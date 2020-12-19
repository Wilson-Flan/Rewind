using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering.PostProcessing;
using UnityEngine.SceneManagement;

public class YellowPlate : MonoBehaviour
{
    public Transform playerTransform;
    public Rigidbody playerRB;
    public Transform cameraTransform;

    private Vector3 initPos;
    private Quaternion initRot;

    private bool rewinding;
    private float t;
    private float factor;
    private bool yellow = false;

    private void Start()
    {
        initPos = playerTransform.position;
        initRot = cameraTransform.rotation;
        t = 1f;
        Time.timeScale = 1f;
    }

    private void Update()
    {
        if (yellow && !rewinding)
        {
            if (SceneManager.GetActiveScene().name == "LevelTwo")
            {
                SceneManager.LoadScene("Menu");
            }
            StartRewind();
        }
        if (!rewinding)
        {
            return;
        }
        playerTransform.position = Vector3.Lerp(playerTransform.position, initPos, t);
        // cameraTransform.rotation = Quaternion.Lerp(cameraTransform.rotation, initRot, t);
        t += Time.deltaTime * 0.1f;
        if (Vector3.Distance(playerTransform.position, initPos) < 0.1f)
        {
            StopRewinding();
        }
    }
    void StartRewind()
    {
        rewinding = true;
        t = 0f;
        playerRB.useGravity = false;
        playerRB.velocity = Vector3.zero;
    }

    void StopRewinding()
    {
        rewinding = false;
        t = 1f;
        playerRB.useGravity = true;
        SceneManager.LoadScene("LevelTwo");
    }

    private void OnTriggerEnter(Collider other)
    {
        yellow = true;
    }
}
