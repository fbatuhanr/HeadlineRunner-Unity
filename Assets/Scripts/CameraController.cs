using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    [SerializeField] private Transform character;
    private float characterOffsetZ;

    [HideInInspector] public bool isFollowingAvailable;

    [SerializeField] private float shakeDuration = 0.5f, shakeStrength=5f;
    [SerializeField] private int shakeVibrato = 5;
    
    private void Start()
    {
        Application.targetFrameRate = 60;
        isFollowingAvailable = true;
        characterOffsetZ = character.position.z - transform.position.z;
    }

    private void Update()
    {
        if (!isFollowingAvailable) return;

        var position = transform.position;
        position.z = character.position.z - characterOffsetZ;
        transform.position = position;
    }

    public void PlayerPassedDoorAnimation()
    {
        transform.DOShakeRotation(
            shakeDuration,
            shakeStrength,
            shakeVibrato);
    }

    public void PlayerDeathCameraAnimation()
    {
        transform.DOMove(
            new Vector3(character.position.x, 6, character.position.z-5),
            1f).SetEase(Ease.Flash);

        transform.DORotate(
            new Vector3(25, 0, 0),
            1f
        ).SetEase(Ease.Linear);
        
    }
}
