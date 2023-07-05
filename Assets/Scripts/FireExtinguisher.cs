using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireExtinguisher : ObjectPickable
{
    [Header("Exctinct")]
    public float exctinctRange = 4f;
    public float forwardRangeAdditive = 2f;
    public float sphereCastInterval = 0.5f;
    public float timeToExctingFire = 1f;

    [Header("Audio")]
    public AudioSource audioSource;

    public override void Pick()
    {
        base.Pick();

        StartCoroutine(Exctinct());
    }

    public override void Release()
    {
        base.Release();

        audioSource.Stop();
        StopAllCoroutines();
    }

    private IEnumerator Exctinct()
    {
        audioSource.Play();

        while (true)
        {
            Ray ray = new Ray(transform.position + transform.forward * forwardRangeAdditive, Vector3.one);
            foreach (var hit in Physics.SphereCastAll(ray, exctinctRange))
            {
                if (hit.collider.TryGetComponent(out Computer computer))
                {
                    if (!computer.IsOnFire) continue;

                    StartCoroutine(ExctingAfter(computer));
                    
                }
            }

            yield return new WaitForSeconds(sphereCastInterval);
        }
    }

    private IEnumerator ExctingAfter(Computer computer)
    {
        yield return new WaitForSeconds(timeToExctingFire);

        computer.fire.Stop();
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.green;
        Gizmos.DrawSphere(transform.position + transform.forward * forwardRangeAdditive, exctinctRange);
    }
}
