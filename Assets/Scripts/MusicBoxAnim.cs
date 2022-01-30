using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MusicBoxAnim : MonoBehaviour
{

    public List<MusicalAction> actions;
    private int index = 0; 
    // Start is called before the first frame update
    void Start()
    {
        Invoke(nameof(PlayAnimSound),2f);
    }

    private void PlayAnimSound()
    {
        Invoke(nameof(PlayAnimSound), 2f);
        this.GetComponent<Instrument>().PlayAction(actions[index]);
        index++;
        if(index >= actions.Count)
        {
            index = 0; 
        }
    }
}
