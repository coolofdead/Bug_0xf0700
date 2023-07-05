using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using Cinemachine;
using System;

public class Computer : MonoBehaviour, IInteractableDisablePlayerMovement
{
    public static Action<Computer> onComputerFire;
    public static Action<Computer> onComputerHack;
    public static Action<Computer> onComputerFix;

    public static int TotalComputerPutOnFire { get; private set; }

    [Header("Computer")]
    [SerializeField] private CinemachineVirtualCamera computerCamera;
    [SerializeField] private RansomWare ransomWare;
    [SerializeField] private GameObject canvas;
    [SerializeField] private GameObject particles;
    [SerializeField] private GameObject bugRedLight;

    [Header("Fire")]
    [SerializeField] private float timeBeforePuttingFire = 15;
    [SerializeField] private Image timeLeftImage;
    [SerializeField] private GameObject extinguisherBigImageContainer;
    [SerializeField] private Image extinguisherBigImage;
    [SerializeField] private float blinkExtinguisherTime = 0.7f;
    [SerializeField] private GameObject timeLeftSlider;
    [field:SerializeField] public Fire fire { get; private set; }

    [Header("Outline")]
    [SerializeField] private Color hoverColor;
    [SerializeField] private Color pickedColor;
    [SerializeField] private Outline outline;
    [SerializeField] private float outlineAnimateTime = 0.4f;
    
    public bool IsBugged { get; private set; } = false;
    public bool IsOnFire => fire.start;
    [field: SerializeField] public bool CanBeHackHonce { get; private set; } = false;

    [field: SerializeField] public int FloorLevel { get; set; } = 1;

    private Action releasePlayerMovementCallback;

    private void Awake()
    {
        fire.onFireStop += OnFireStop;
    }

    public void CreateBug()
    {
        IsBugged = true;
        particles.SetActive(true);
        canvas.SetActive(true);
        bugRedLight.SetActive(true);
        timeLeftSlider.SetActive(true);

        LeanTween.value(timeLeftImage.gameObject, (float v) => timeLeftImage.fillAmount = v, 0, 1, timeBeforePuttingFire);
        Invoke("StartFire", timeBeforePuttingFire);

        onComputerHack?.Invoke(this);
    }

    private void OnFireStop()
    {
        extinguisherBigImageContainer.SetActive(false);
        LeanTween.cancel(extinguisherBigImage.gameObject);
        canvas.SetActive(false);
        onComputerFix?.Invoke(this);
    }

    public void StartFire()
    {
        if (!IsBugged) return;

        TotalComputerPutOnFire++;

        outline.enabled = false;
        particles.SetActive(false);
        bugRedLight.SetActive(false);
        timeLeftSlider.SetActive(false);
        extinguisherBigImageContainer.SetActive(true);
        LeanTween.cancel(timeLeftImage.gameObject);
        LeanTween.cancel(outline.gameObject);
        var targetColor = extinguisherBigImage.color;
        targetColor.a = 0;
        LeanTween.value(extinguisherBigImage.gameObject, (Color c) => extinguisherBigImage.color = c, extinguisherBigImage.color, targetColor, blinkExtinguisherTime).setLoopPingPong();

        computerCamera.enabled = false;
        releasePlayerMovementCallback?.Invoke();

        fire.StartFire();

        onComputerFire?.Invoke(this);
    }

    public void Interact()
    {
        if (!IsInteractable()) return;

        computerCamera.enabled = !computerCamera.enabled;

        ransomWare.EnableInputField(computerCamera.enabled);

        if (!computerCamera.enabled) releasePlayerMovementCallback?.Invoke();
    }

    public void FixBug()
    {
        if (!CanBeHackHonce) IsBugged = false;

        particles.SetActive(false);
        canvas.SetActive(false);
        bugRedLight.SetActive(false);
        timeLeftSlider.SetActive(false);
        LeanTween.cancel(timeLeftImage.gameObject);
        LeanTween.cancel(outline.gameObject);
        CancelInvoke("StartFire");

        computerCamera.enabled = false;
        releasePlayerMovementCallback?.Invoke();
        onComputerFix?.Invoke(this);
    }

    public void Hover()
    {
        if (fire.start) return;

        outline.enabled = true;
        outline.OutlineColor = hoverColor;
        LeanTween.value(outline.gameObject, (float v) => outline.OutlineWidth = v, 8f, 26f, outlineAnimateTime).setLoopPingPong();
    }

    public void ExitHover()
    {
        if (fire.start) return;

        outline.enabled = false;
        outline.OutlineColor = pickedColor;
        LeanTween.cancel(outline.gameObject);
    }

    public void DisablePlayerMovement(Action releasePlayerMovementCallback)
    {
        this.releasePlayerMovementCallback = releasePlayerMovementCallback;
    }

    public bool IsInteractable()
    {
        return !fire.start;
    }
}
