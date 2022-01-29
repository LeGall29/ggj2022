using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MusicManager : MonoBehaviour
{
    public static MusicManager Instance;

    public Instrument currentInstrument;

    private void Awake()
    {
        Instance = this;
    }

}
