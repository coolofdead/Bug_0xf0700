using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Projector : MonoBehaviour
{
    [Header("Screen")]
    public Image screenImage;
    public Animator screenAnimator;
    public AnimationClip swapSlidesAnimation;
    public Sprite[] projectedTutoFrames;

    public float changeFrameAfterSec;

    [Header("Screen")]
    public Image timeLeftFill;
    public Image[] slidesCount;
    public Color slideDisplayedColor;
    public Color slideNotDisplayedColor;

    private int currentFrame = 0;

    private void Start()
    {
        StartCoroutine(DisplayFrames());
    }

    private IEnumerator DisplayFrames()
    {
        while (true)
        {
            screenImage.sprite = projectedTutoFrames[currentFrame];
            StartCoroutine(UpdateFillSlides());

            yield return new WaitForSeconds(changeFrameAfterSec);

            for (int i = 0; i < slidesCount.Length; i++)
            {
                slidesCount[i].color = currentFrame >= i ? slideDisplayedColor : slideNotDisplayedColor;
            }
            screenAnimator.Play("SwapSlide");
            yield return new WaitForSeconds(swapSlidesAnimation.length);

            currentFrame = currentFrame + 1 == projectedTutoFrames.Length ? 0 : currentFrame+1;
        }
    }

    private IEnumerator UpdateFillSlides()
    {
        float currentTime = 0;
        while (currentTime < changeFrameAfterSec)
        {
            currentTime += Time.deltaTime;

            timeLeftFill.fillAmount = currentTime / changeFrameAfterSec;
            yield return null;
        }
        timeLeftFill.fillAmount = 1;
    }
}
