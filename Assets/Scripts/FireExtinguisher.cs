using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireExtinguisher : ObjectPickable
{
    public float exctinctRange = 4f;
    public float forwardRangeAdditive = 2f;
    public float sphereCastInterval = 0.5f;

    public override void Pick()
    {
        base.Pick();

        StartCoroutine(Exctinct());
    }

    public override void Release()
    {
        base.Release();

        StopAllCoroutines();
    }

    private IEnumerator Exctinct()
    {
        while (true)
        {
            Ray ray = new Ray(transform.position + transform.forward * forwardRangeAdditive, Vector3.one);
            foreach (var hit in Physics.SphereCastAll(ray, exctinctRange))
            {
                if (hit.collider.TryGetComponent(out Computer computer))
                {
                    if (!computer.IsOnFire) continue;

                    computer.fire.Stop();
                }
            }

            yield return new WaitForSeconds(sphereCastInterval);
        }
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.green;
        Gizmos.DrawSphere(transform.position + transform.forward * forwardRangeAdditive, exctinctRange);
    }
}
