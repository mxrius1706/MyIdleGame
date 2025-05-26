using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayOneShot : StateMachineBehaviour
{
    [SerializeField] public AudioClip soundToPlay;
    [SerializeField, Range(0f, 1f)] public float volume = 1f;

    public bool playOnEnter = true;
    public bool playOnExit = false;
    public bool playAfterDelay = false;
    public float delay = 0.25f;

    private float timeSinceEntered = 0f;
    private bool hasDelayedSoundPlayed = false;

    // Für Loop-Erkennung
    private int lastLoopCount = -1;

    public override void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        if (playOnEnter)
        {
            PlaySound(animator);
        }

        timeSinceEntered = 0f;
        hasDelayedSoundPlayed = false;
        lastLoopCount = Mathf.FloorToInt(stateInfo.normalizedTime); // Für Looperkennung
    }

    public override void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        // Verzögerter Sound
        if (playAfterDelay && !hasDelayedSoundPlayed)
        {
            timeSinceEntered += Time.deltaTime;
            if (timeSinceEntered >= delay)
            {
                PlaySound(animator);
                hasDelayedSoundPlayed = true;
            }
        }

        // 🎯 NEU: Sound bei jedem Animationsloop abspielen
        int currentLoop = Mathf.FloorToInt(stateInfo.normalizedTime);
        if (currentLoop > lastLoopCount)
        {
            lastLoopCount = currentLoop;
            PlaySound(animator);
        }
    }

    public override void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        if (playOnExit)
        {
            PlaySound(animator);
        }

        lastLoopCount = -1;
    }

    private void PlaySound(Animator animator)
    {
        if (soundToPlay != null)
        {
            AudioSource.PlayClipAtPoint(soundToPlay, animator.transform.position, volume);
        }
    }
}
