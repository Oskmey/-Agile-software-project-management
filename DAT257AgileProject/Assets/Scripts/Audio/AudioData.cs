using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioData : ScriptableObject
{
    [SerializeField]
    private AudioType audioType;

    [SerializeField]
    private AudioClip audioClip;

    public AudioType AudioType => audioType;
    public AudioClip AudioClip => audioClip;
}
