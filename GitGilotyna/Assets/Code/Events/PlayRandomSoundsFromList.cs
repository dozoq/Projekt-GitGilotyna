using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Code.General;
using UnityEngine;
using Random = UnityEngine.Random;

public class PlayRandomSoundsFromList : MonoBehaviour
{
    [SerializeField] private List<string> ClipNames;
    private SoundSystem _soundSystem;

    private void Start()
    {
        _soundSystem = GameManager.instance.soundSystem;
    }

    public void PlayRandom()
    {
        _soundSystem.PlaySfx(ClipNames[Random.Range(0, ClipNames.Count - 1)]);
    }
}
