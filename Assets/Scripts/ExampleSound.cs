using System;
using System.Collections;
using System.Collections.Generic;
using Ami.BroAudio;
using UnityEngine;

public class ExampleSound : MonoBehaviour
{
    [SerializeField] private SoundID _music = default;

    private void Start()
    {
        BroAudio.Play(_music);
    }
}
