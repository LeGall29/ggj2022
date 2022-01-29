using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class MusicManager : MonoBehaviour
{
    public static MusicManager Instance;

    public Instrument currentInstrument;
    public AudioMixerSnapshot snap_WithoutGuitar;
    public AudioMixerSnapshot snap_WithGuitar;
    public AudioMixerSnapshot snap_Muted;
    public AudioMixer mixer;

    private AudioMixerSnapshot currentSnapshot;

    private void Awake()
    {
        Instance = this;
        currentSnapshot = snap_WithoutGuitar;
    }
    public void AddGuitar()
    {
        mixer.TransitionToSnapshots(new AudioMixerSnapshot[] { currentSnapshot, snap_WithGuitar }, new float[] { 0, 1 }, 2f);
        currentSnapshot = snap_WithGuitar;
    }
    public void Mute()
    {
        mixer.TransitionToSnapshots(new AudioMixerSnapshot[] { currentSnapshot, snap_Muted }, new float[] { 0, 1 }, 2f);
    }
    public void Unmute()
    {
        mixer.TransitionToSnapshots(new AudioMixerSnapshot[] { snap_Muted, currentSnapshot }, new float[] { 0, 1 }, 2f);
    }

}
