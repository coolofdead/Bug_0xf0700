using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BookFix : MonoBehaviour
{
    public Sprite[] pagesSprites;
    public int currentPage;

    public Animator pageAnimator;
    public float updateCurrentPageDelay = 0.3f;
    public float timeToFlipPage = 1.2f;
    public Material currentPageMaterial;
    public Material nextPageMaterial;

    private bool flippingPage = false;

    private void OnEnable()
    {
        ResetFirstPage();
    }

    public void ResetFirstPage()
    {
        currentPageMaterial.mainTexture = pagesSprites[currentPage].texture;
    }

    public void FlipNextPage()
    {
        if (currentPage == pagesSprites.Length-1 || flippingPage) return;

        flippingPage = true;
        currentPage++;
        nextPageMaterial.mainTexture = pagesSprites[currentPage].texture;
        pageAnimator.Play("NextPage");
        Invoke("UpdateCurrentPage", updateCurrentPageDelay);
        Invoke("DoneFlippingPage", timeToFlipPage);
    }

    public void FlipPreviousPage()
    {
        if (currentPage == 0 || flippingPage) return;

        flippingPage = true;
        nextPageMaterial.mainTexture = pagesSprites[currentPage].texture;
        currentPage--;
        currentPageMaterial.mainTexture = pagesSprites[currentPage].texture;
        pageAnimator.Play("PreviousPage");
        Invoke("DoneFlippingPage", timeToFlipPage);
    }

    private void UpdateCurrentPage()
    {
        currentPageMaterial.mainTexture = pagesSprites[currentPage].texture;
    }

    private void DoneFlippingPage()
    {
        flippingPage = false;
    }
}
