using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RarityStarSpin : MonoBehaviour
{
    private void OnEnable()
    {
        StartCoroutine(SpinStar());
        Managers.Sound.PlaySFX(Define.SFX.RarityStar);
    }

    private IEnumerator SpinStar()
    {
        while(!Managers.Game.isGameOver)
        {
            transform.Rotate(0.0f, 0.0f, 2.0f);
            yield return null;
        }
    }
}
