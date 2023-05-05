using System;
using System.Collections.Generic;
using System.Linq;
using Unity.Mathematics;
using UnityEngine;
using Random = UnityEngine.Random;

public class SoundSystem : MonoBehaviour
{
    public List<AudioClip> bglibrary;
    public List<NamedAudioClip> fxlibrary;
    [SerializeField] private AudioSource bgSource;
    [SerializeField] private AudioSource fxSource;
    // Start is called before the first frame update
    public void MuteAll()
    {
        bgSource.volume = 1 - Mathf.Ceil(bgSource.volume);
        fxSource.volume = 1 - Mathf.Ceil(fxSource.volume);
    }
    
    public void PlaySfx(string name)
    {
        fxSource.PlayOneShot(fxlibrary.FirstOrDefault((x)=>x.name.Equals(name))?.clip);
    }

    public void Start()
    {
        InvokeRepeating(nameof(PlayRandomBackground),0,1f);
        PlaySfx("Click");
    }
    
    public void PlayBackground(AudioClip clip)
    {
        bgSource.PlayOneShot(clip);
    }
    private void PlayRandomBackground()
    {
        if (!bgSource.isPlaying)
        {
            var rand = Random.Range(0, bglibrary.Count - 1);
            bgSource.PlayOneShot(bglibrary[rand]);
        }
    }
}
[Serializable]
public class NamedAudioClip
{
    public string name;
    public AudioClip clip;
}
