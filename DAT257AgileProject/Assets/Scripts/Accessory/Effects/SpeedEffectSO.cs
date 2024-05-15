using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(fileName = "SpeedEffect", menuName = "Effects/Speed Effect")]
public class SpeedEffectSO : EffectSO
{

    [SerializeField]
    int speed;
    public override void ApplyEffect()
    {
        PlayerController playerController = FindAnyObjectByType<PlayerController>();
        playerController.speed += speed;
    }

    public override void UnApplyEffect()
    {
        PlayerController playerController = FindAnyObjectByType<PlayerController>();
        playerController.speed -= speed;
    }
}
