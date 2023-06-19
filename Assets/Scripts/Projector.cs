using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Projector : MonoBehaviour
{
    public Image screenImage;
    public Animator screenAnimator;
    public AnimationClip swapSlidesAnimation;
    public Sprite[] projectedTutoFrames;

    public float changeFrameAfterSec;

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

            yield return new WaitForSeconds(changeFrameAfterSec);

            screenAnimator.Play("SwapSlide");

            yield return new WaitForSeconds(swapSlidesAnimation.length);

            currentFrame = currentFrame + 1 == projectedTutoFrames.Length ? 0 : currentFrame+1;
        }
    }
}
