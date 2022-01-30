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
                guitarMusic.DOFade(guitarVolume, 3f);
                guitarIsUnlocked = true;
                break;
            case InstrumentType.Vibraphone:
                vibraphoneMusic.DOFade(vibraphoneVolume, 3f);
                vibraphoneIsUnlocked = true;
                break;
            case InstrumentType.Trumpet:
                trumpetMusic.DOFade(trumpetVolume, 3f);
                trumpetIsUnlocked = true;
                break;
        }
    }
    public void Mute()
    {
        backgroundMusic.DOFade(0, 2f);
        guitarMusic.DOFade(0, 2f);
        trumpetMusic.DOFade(0, 2f);
        vibraphoneMusic.DOFade(0, 2f);

    }
    public void Unmute()
    {
        backgroundMusic.DOFade(1f, 1f);

        if (guitarIsUnlocked)
            guitarMusic.DOFade(guitarVolume, 2f);

        if (trumpetIsUnlocked)
            trumpetMusic.DOFade(trumpetVolume, 2f);

        if (vibraphoneIsUnlocked)
            vibraphoneMusic.DOFade(vibraphoneVolume, 2f);
    }

}
