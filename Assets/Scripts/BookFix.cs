using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class BookFix : MonoBehaviour
{
    [Header("Book")]
    public TextMeshProUGUI currentPageCodeTMP;
    public TextMeshProUGUI animatedPageCodeTMP;
    public TextMeshProUGUI pageInfo;
    public int currentPage;

    [Header("Page")]
    public Animator pageAnimator;
    public float updateNextPageDelay = 0.3f;
    public float updatePreviousPageDelay = 0.3f;
    public float timeToFlipPage = 1.2f;

    private bool flippingPage = false;
    
    private void Start()
    {
        UpdateCurrentPage();
    }

    public void FlipNextPage()
    {
        if (currentPage == FixCodeManager.Instance.FixCodes.Length - 1 || flippingPage) return;

        flippingPage = true;
        animatedPageCodeTMP.text = FormatFixCodeOnPageAtIndex(currentPage);
        currentPage++;
        pageAnimator.Play("NextPage");

        Invoke("UpdateCurrentPage", updateNextPageDelay);
        StartCoroutine(DoneFlippingPage(timeToFlipPage));
    }

    public void FlipPreviousPage()
    {
        if (currentPage == 0 || flippingPage) return;

        flippingPage = true;
        currentPage--;
        animatedPageCodeTMP.text = FormatFixCodeOnPageAtIndex(currentPage);
        pageAnimator.Play("PreviousPage");

        Invoke("UpdateCurrentPage", updatePreviousPageDelay);
        StartCoroutine(DoneFlippingPage(timeToFlipPage));
    }

    private void UpdateCurrentPage()
    {
        currentPageCodeTMP.text = FormatFixCodeOnPageAtIndex(currentPage);
        pageInfo.text = $"{currentPage + 1}/{FixCodeManager.Instance.FixCodes.Length}";
    }

    private IEnumerator DoneFlippingPage(float waitTime)
    {
        yield return new WaitForSeconds(waitTime);

        flippingPage = false;
    }

    private string FormatFixCodeOnPageAtIndex(int page)
    {
        return FixCodeManager.Instance.FixCodes[page].pcId + "\n" + FixCodeManager.Instance.FixCodes[page].fixCode;
    }
}
