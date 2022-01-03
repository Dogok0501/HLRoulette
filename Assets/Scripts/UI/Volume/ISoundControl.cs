using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

interface ISoundControl
{
    void SettingVolume(float volume);
    void OnOffSound(bool isOn);
}
