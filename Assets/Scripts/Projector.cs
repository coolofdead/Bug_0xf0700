using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Projector : MonoBehaviour
{
    public bool autoStartProjecting = true;

    [Header("Screen")]
    public Image screenImage;
    public Animator projectorAnimator;
    public Animator screenAnimator;
    public AnimationClip startProjectorAnimation;
    public AnimationClip swapSlidesAnimation;
    public Sprite[] projectedTutoFrames;

    public float changeFrameAfterSec;

    [Header("Slides")]
    public Image timeLeftFill;
    public Image[] slidesCount;
    public Color slideDisplayedColor;
    public Color slideNotDisplayedColor;

    private int currentFrame = 0;

    private void Start()
    {
        if (autoStartProjecting) StartProjecting();
    }

    public void StartProjecting()
    {
        projectorAnimator.Play("ProjectorLight");
        
        StartCoroutine(DisplayFrames());
    }

    private IEnumerator DisplayFrames()
    {
        screenAnimator.Play("StartProjector");

        yield return new WaitForSeconds(startProjectorAnimation.length);

        while (true)
        {
            screenImage.sprite = projectedTutoFrames[currentFrame];
            StartCoroutine(UpdateFillSlides());

            for (int i = 0; i < slidesCount.Length; i++)
            {
                slidesCount[i].color = currentFrame >= i ? slideDisplayedColor : slideNotDisplayedColor;
            }

            yield return new WaitForSeconds(changeFrameAfterSec);

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
