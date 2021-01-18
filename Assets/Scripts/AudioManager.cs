using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    [SerializeField] GameObject FPSCharacter;

    AudioSource _audioSource = null;
    FPSMotor _motor = null;
    private void Awake()
    {
        _audioSource = GetComponent<AudioSource>();
        _motor = FPSCharacter.GetComponent<FPSMotor>();
    }

    private void Update()
    {
        if(_motor._isGrounded && _motor.isSkiing && _audioSource.isPlaying==false)
        {
            _audioSource.Play();
        } else if(_motor._isGrounded == false || _motor.isSkiing == false)
        {
            _audioSource.Stop();
        }
    }

}
