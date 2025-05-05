using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.Rendering;

public class Fader : MonoBehaviour
{
    [SerializeField] CanvasGroup canvasGroup;
    [SerializeField] float fadeSpeed = 1.0f;
    enum FadeState { IDLE, FADING_IN, FADING_OUT };
    FadeState fadeState = FadeState.IDLE;

    public UnityEvent onFinishFadeIn;
    public UnityEvent onFinishFadeOut;

    public void FadeIn()
    {
        fadeState = FadeState.FADING_IN;
    }

    public void FadeOut()
    {
        fadeState = FadeState.FADING_OUT;
    }

    private void Update()
    {
        switch (fadeState)
        {
            case FadeState.IDLE:
                break;
            case FadeState.FADING_IN:
                if (canvasGroup.alpha < 1.0f)
                {
                    canvasGroup.alpha = Mathf.Clamp01(canvasGroup.alpha + Time.deltaTime * fadeSpeed);
                    if (canvasGroup.alpha == 1.0f)
                    {
                        Debug.Log("Ha terminado fade in");
                        onFinishFadeIn?.Invoke();
                    }
                }
                break;
            case FadeState.FADING_OUT:
                if (canvasGroup.alpha >= 0.0f)
                {
                    canvasGroup.alpha = Mathf.Clamp01(canvasGroup.alpha - Time.deltaTime * fadeSpeed);
                    if (canvasGroup.alpha == 0.0f)
                    {
                        onFinishFadeOut?.Invoke();
                        Debug.Log("Ha terminado fade out");
                    }
                }
                break;
        }

    }
}
