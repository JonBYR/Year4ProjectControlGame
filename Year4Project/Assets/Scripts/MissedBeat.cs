using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MissedBeat : MonoBehaviour
{
    public IEnumerator Shake(float duration, float magnitude)
    {
        Vector3 originalPos = transform.localPosition; //code taken from this video https://www.youtube.com/watch?v=9A9yj8KnM8c
        float elapsed = 0f;
        while(elapsed < duration)
        {
            float x = Random.Range(-1f, 1f) * magnitude;
            float y = Random.Range(-1f, 1f) * magnitude;
            transform.localPosition = new Vector3(x, y, originalPos.z);
            elapsed += Time.deltaTime;
            yield return null;
        }
        transform.localPosition = originalPos; //return to original position after coroutine has finished
    }
}
