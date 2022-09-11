using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraShake : MonoBehaviour
{
    private Vector3 initialPos;

    [SerializeField][Range(0, 0.5f)] private float shakeDuration= 1;
    [SerializeField][Range(0,0.5f)] private float shakeMagnitude = 1;

    private void Start()
    {
        initialPos = transform.position;
    }

    public void Shake()
    {
        StartCoroutine(ShakeCoroutine());
    }

    private IEnumerator ShakeCoroutine()
    {
        float elapsedTime = 0;

        while(elapsedTime < shakeDuration)
        {
            elapsedTime += Time.deltaTime;

            Vector3 newPosition = 
                initialPos + (Vector3) Random.insideUnitCircle * shakeMagnitude;

            transform.position = newPosition;

            yield return new WaitForEndOfFrame();


        }

        transform.position = initialPos;
    }
}
