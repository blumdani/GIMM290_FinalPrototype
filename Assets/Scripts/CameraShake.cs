using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraShake : MonoBehaviour
{
    [SerializeField] private Camera cam;
    [SerializeField] private float shakeDuration;
    [SerializeField] private float shakeMagnitude;

    public void ShakeCamera()
    {
        StartCoroutine(Shake(shakeDuration, shakeMagnitude));
    }

    //Coroutine to handle camera shake
    IEnumerator Shake(float duration, float amount)
    {
        float elapsed = 0f;
        while(elapsed < duration)
        {
            Vector3 vector = Vector3.zero + Random.insideUnitSphere * amount;
            vector.z  = 0;
            cam.transform.localPosition = vector;
            elapsed += Time.deltaTime;
            yield return null;
        }
        cam.transform.localPosition = Vector3.zero;
    }
}
