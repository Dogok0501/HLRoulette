using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoroutineManager
{
    private Dictionary<float, WaitForSeconds> timeInterval = new Dictionary<float, WaitForSeconds>();

    public WaitForSeconds WaitForSecondsEx(float seconds)
    {
        if (!timeInterval.ContainsKey(seconds))
            timeInterval.Add(seconds, new WaitForSeconds(seconds));
        return timeInterval[seconds];
    }

    // 같은 시간을 공유하는 WaitForSeconds를 캐싱하여 GC 최적화
}
