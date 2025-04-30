using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MusicPlayer : MonoBehaviour
{

    public AudioSource introSource;
    public AudioSource loopSource;
    
    // Start is called before the first frame update
    void Start()
    {
    double startTime = AudioSettings.dspTime;

    introSource.PlayScheduled(startTime);
    introSource.SetScheduledEndTime(startTime + introSource.clip.length);

    loopSource.PlayScheduled(startTime + introSource.clip.length);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
