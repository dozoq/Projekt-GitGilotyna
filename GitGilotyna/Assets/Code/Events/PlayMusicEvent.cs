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
        
    }

    public void Play()
    {
        if(!_soundSystem) _soundSystem  = GameManager.instance.soundSystem;
        try
        {
            _soundSystem.PlaySfx(clipName);

        }
        catch (Exception e)
        {
            Debug.LogError(e + clipName);
            throw;
        }
    }
}
