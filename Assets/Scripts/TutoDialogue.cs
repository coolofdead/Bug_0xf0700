using System.Collections;
using System.Collections.Generic;
using StarterAssets;
using UnityEngine;

public class TutoDialogue : MonoBehaviour
{
    public FirstPersonController fpsController;
    public string[] tutoDialogue;

    private float defaultMoveSpeed;

    void Start()
    {
        defaultMoveSpeed = fpsController.MoveSpeed;
        //fpsController.MoveSpeed = 0;
        ShowTutoDialogue();
    }

    public void ShowTutoDialogue()
    {
        DialogueManager.Instance.ShowDialogue(tutoDialogue, OnDialogueDone);
    }

    private void OnDialogueDone()
    {
        fpsController.MoveSpeed = defaultMoveSpeed;
    }
}
