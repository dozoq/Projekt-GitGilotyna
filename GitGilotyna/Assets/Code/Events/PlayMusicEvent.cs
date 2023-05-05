using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Code.General;
using UnityEngine;

public class PlayMusicEvent : MonoBehaviour
{
    [SerializeField] private string clipName;
    private SoundSystem _soundSystem;

    private void Start()
    {
        _soundSystem = GameManager.instance.soundSystem;
    }

    public void Play()
    {
        _soundSystem.PlaySfx(clipName);
    }
}
