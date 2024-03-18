using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class gizmoHelper : MonoBehaviour
{
    public Color gizmoColor = Color.white;
    public float gizmoSize = .5f;
    private void OnDrawGizmos()
    {
        Gizmos.color = gizmoColor;
        Gizmos.DrawSphere(transform.position, gizmoSize);
    }
}
