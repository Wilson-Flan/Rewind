using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering.PostProcessing;
using UnityEngine.SceneManagement;

public class PlayerRewind : MonoBehaviour
{
    public Transform playerTransform;
    public Rigidbody playerRB;
    public Transform cameraTransform;

    private Vector3 initPos;
    private Quaternion initRot;

    private bool rewinding;
    private float t;
    private float factor;

    public PostProcessProfile ppp;
    private ColorGrading colorGrading;
    private ChromaticAberration chromaticAberration;
    private LensDistortion lensDistortion;
    private float distortion;
    private float saturation;
    private float chroma;

    void Awake()
    {
        colorGrading = ppp.GetSetting<ColorGrading>();
        lensDistortion = ppp.GetSetting<LensDistortion>();
        chromaticAberration = ppp.GetSetting<ChromaticAberration>();
    }

    private void Start()
    {
        initPos = playerTransform.position;
        initRot = cameraTransform.rotation;
        t = 1f;
        Time.timeScale = 1f;
    }

    private void Update()
    {
        if (playerTransform.position.y < 0f && !rewinding)
        {
            StartRewind();
        }
        if (!rewinding)
        {
            return;
        }
        playerTransform.position = Vector3.Lerp(playerTransform.position, initPos, t);
        // cameraTransform.rotation = Quaternion.Lerp(cameraTransform.rotation, initRot, t);
        t += Time.deltaTime * 0.1f;
        factor = 1 - Mathf.Clamp(t * 10f, 0f, 1f);
        distortion = -150f * factor;
        saturation = -100f * factor;
        chroma = factor;
        UpdatePPP();
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
        lensDistortion.enabled.value = true;
        chromaticAberration.enabled.value = true;
    }

    void StopRewinding()
    {
        rewinding = false;
        t = 1f;
        playerRB.useGravity = true;
        colorGrading.saturation.value = 0f;
        lensDistortion.enabled.value = false;
        chromaticAberration.enabled.value = false;
        //SceneManager.LoadSceneAsync(SceneManager.GetActiveScene().name, LoadSceneMode.Single);
    }

    void UpdatePPP()
    {
        if (Mathf.Abs(colorGrading.saturation.value - saturation) < 0.1f)
        {
            return;
        }
        colorGrading.saturation.value = Mathf.Lerp(colorGrading.saturation.value, saturation, Time.deltaTime * 6f);
        chromaticAberration.intensity.value = Mathf.Lerp(chromaticAberration.intensity.value, chroma, Time.deltaTime * 6f);
        lensDistortion.intensity.value = Mathf.Lerp(lensDistortion.intensity.value, distortion, Time.deltaTime * 6f);
    }
}
