using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Minimap : MonoBehaviour
{
    public RectTransform minimap;
    public RectTransform map2DEnd;
    public RectTransform playerInMinimap;
    public Transform map3DParentCenter;
    public Transform map3DEnd;

    private Vector3 normalized;
    private Vector3 mapped;

    private void Update()
    {
        normalized = Divide(map3DParentCenter.InverseTransformPoint(transform.position), map3DEnd.position - map3DParentCenter.position);
        normalized.y = normalized.z;
        mapped = Multiply(normalized, map2DEnd.localPosition);
        mapped.z = 0;
        minimap.localPosition = -mapped;

        float angleZ = Mathf.Atan2(transform.forward.z, transform.forward.x) * Mathf.Rad2Deg;
        playerInMinimap.eulerAngles = new Vector3(0, 0, angleZ - 90);
    }

    private static Vector3 Divide(Vector3 a, Vector3 b)
    {
        return new Vector3(a.x / b.x, a.y / b.y, a.z / b.z);
    }

    private static Vector3 Multiply(Vector3 a, Vector3 b)
    {
        return new Vector3(a.x * b.x, a.y * b.y, a.z * b.z);
    }
}
