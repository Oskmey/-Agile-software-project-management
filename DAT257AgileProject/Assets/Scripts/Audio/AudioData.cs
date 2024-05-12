using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioData : ScriptableObject
{
    // TODO: field is probably unnecessary, so should remove. 
    [SerializeField]
    private AudioType audioType;

    [SerializeField]
    private AudioClip audioClip;

    [Tooltip("Value is [0,1], where 0 means no sound and 1 means no change. Values in between will lower the volume.")]
    [SerializeField]
    private float audioBalancingValue;

    public AudioType AudioType => audioType;
    public AudioClip AudioClip => audioClip;
    public float AudioBalancingValue => audioBalancingValue;
}
