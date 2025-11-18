using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

[RequireComponent(typeof(Camera))]

public class PixelateEffect : MonoBehaviour
{
    public Material pixelateMaterial;
    private float pixelSize = 1f;
    private bool isPlaying = false;

    void OnRenderImage(RenderTexture src, RenderTexture dest)
    {
        if (isPlaying)
        {
            pixelateMaterial.SetFloat("_PixelSize", pixelSize);
            Graphics.Blit(src, dest, pixelateMaterial);
        }
        else
        {
            Graphics.Blit(src, dest);
        }
    }

    public void PlayPixelateEffect(float maxPixelSize, float duration, int nextSceneIndex)
    {
        if (!isPlaying)
        {
            StartCoroutine(PixelateRoutine(maxPixelSize, duration, nextSceneIndex));
        }
    }

    private IEnumerator PixelateRoutine(float maxPixelSize, float duration, int nextSceneIndex)
    {
        isPlaying = true;

        float elapsed = 0f;
        while (elapsed < duration)
        {
            elapsed += Time.deltaTime;
            float t = elapsed / duration;
            pixelSize = Mathf.Lerp(1f, maxPixelSize, t);
            yield return null;
        }

        pixelSize = maxPixelSize;

        yield return new WaitForSeconds(0.1f); // Delay opcional

        SceneManager.LoadScene(nextSceneIndex);
    }

}

