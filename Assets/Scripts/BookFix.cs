using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class BookFix : MonoBehaviour
{
    public TextMeshProUGUI currentPageCodeTMP;
    public TextMeshProUGUI nextPageCodeTMP;
    public int currentPage;

    public Animator pageAnimator;
    public float updateCurrentPageDelay = 0.3f;
    public float timeToFlipPage = 1.2f;

    private bool flippingPage = false;

    private void OnEnable()
    {
        ResetFirstPage();
    }

    public void ResetFirstPage()
    {
        currentPageCodeTMP.text = FormatFixCodeOnPageAtIndex(currentPage);
    }

    public void FlipNextPage()
    {
        if (currentPage == FixCodeManager.Instance.FixCodes.Length-1 || flippingPage) return;

        flippingPage = true;
        currentPage++;
        nextPageCodeTMP.text = FormatFixCodeOnPageAtIndex(currentPage);
        pageAnimator.Play("NextPage");
        Invoke("UpdateCurrentPage", updateCurrentPageDelay);
        Invoke("DoneFlippingPage", timeToFlipPage);
    }

    public void FlipPreviousPage()
    {
        if (currentPage == 0 || flippingPage) return;

        flippingPage = true;
        nextPageCodeTMP.text = FormatFixCodeOnPageAtIndex(currentPage);
        currentPage--;
        currentPageCodeTMP.text = FormatFixCodeOnPageAtIndex(currentPage);
        pageAnimator.Play("PreviousPage");
        Invoke("DoneFlippingPage", timeToFlipPage);
    }

    private void UpdateCurrentPage()
    {
        currentPageCodeTMP.text = FormatFixCodeOnPageAtIndex(currentPage);
    }

    private void DoneFlippingPage()
    {
        flippingPage = false;
    }

    private string FormatFixCodeOnPageAtIndex(int page)
    {
        return FixCodeManager.Instance.FixCodes[page].pcId + "\n" + FixCodeManager.Instance.FixCodes[page].fixCode;
    }
}
