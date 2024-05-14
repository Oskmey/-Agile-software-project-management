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
        if (playerController.speed > 5)
        {
            playerController.speed = 5f;
        }
        else
        {
        playerController.speed = 5f + speed;
        }
    }
}
