using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpinImage : MonoBehaviour
{
    private void Start()
    {
        StartCoroutine(KeepSpinImage());
    }

    private IEnumerator KeepSpinImage()
    {
        while (true)
        {
            transform.Rotate(0, 0, -10);
            yield return null;
        }
    }
}
