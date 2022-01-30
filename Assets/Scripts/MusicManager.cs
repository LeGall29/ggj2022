using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using DG.Tweening;

public class MusicManager : MonoBehaviour
{
    public static MusicManager Instance;

    public Instrument currentInstrument;
    public AudioSource backgroundMusic;
    public AudioSource guitarMusic;
    public AudioSource trumpetMusic;
    public AudioSource vibraphoneMusic;

    public float guitarVolume = 0.7f;
    public float vibraphoneVolume = 0.7f;
    public float trumpetVolume = 0.6f;

    private bool guitarIsUnlocked = false;
    private bool vibraphoneIsUnlocked = false;
    private bool trumpetIsUnlocked = false;

    private void Awake()
    {
        Instance = this;
        guitarMusic.volume = 0;
        trumpetMusic.volume = 0;
        vibraphoneMusic.volume = 0;
        backgroundMusic.volume = 1f;
    }
    public void AddInstrumentMusic(InstrumentType _type)
    {
        switch (_type)
        {
            case InstrumentType.Guitar:
                guitarMusic.DOFade(guitarVolume, 1f);
                guitarIsUnlocked = true;
                break;
            case InstrumentType.Vibraphone:
                vibraphoneMusic.DOFade(vibraphoneVolume, 1f);
                vibraphoneIsUnlocked = true;
                break;
            case InstrumentType.Trumpet:
                trumpetMusic.DOFade(trumpetVolume, 1f);
                trumpetIsUnlocked = true;
                break;
        }
    }
    public void Mute()
    {
        backgroundMusic.DOFade(0, 1f);
        guitarMusic.DOFade(0, 1f);
        trumpetMusic.DOFade(0, 1f);
        vibraphoneMusic.DOFade(0, 1f);

    }
    public void Unmute()
    {
        backgroundMusic.DOFade(1f, 1f);

        if (guitarIsUnlocked)
            guitarMusic.DOFade(guitarVolume, 1f);

        if (trumpetIsUnlocked)
            trumpetMusic.DOFade(trumpetVolume, 1f);

        if (vibraphoneIsUnlocked)
            vibraphoneMusic.DOFade(vibraphoneVolume, 1f);
    }

}
