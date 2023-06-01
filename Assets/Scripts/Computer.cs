using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Computer : MonoBehaviour
{
    public static System.Action onComputerBugResolve;

    public ParticleSystem bugFeedback;

    public GameObject bugScreen;
    public MeshRenderer mr;
    public Material[] bugScreenMaterials;

    public void CreateBug()
    {
        bugScreen.SetActive(true);
        bugFeedback.gameObject.SetActive(true);
        mr.material = bugScreenMaterials[Random.Range(0, bugScreenMaterials.Length)];
    }

    public void ResolveBug()
    {
        bugScreen.SetActive(false);
        bugFeedback.gameObject.SetActive(false);

        onComputerBugResolve?.Invoke();
    }
}
