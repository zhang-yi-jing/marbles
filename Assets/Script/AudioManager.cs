using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonosingletonTemp<AudioManager>
{
    private Dictionary<AudioClip, AudioSource> audioSources = new Dictionary<AudioClip, AudioSource>();
    private HashSet<AudioClip> currentlyPlayingClips = new HashSet<AudioClip>();

    private void Start()
    {
        // 初始化时不需要预先创建 AudioSource
    }

    private AudioSource GetOrCreateAudioSource(AudioClip clip)
    {
        if (!audioSources.TryGetValue(clip, out AudioSource source))
        {
            GameObject audioObject = new GameObject("AudioSource_" + clip.name);
            audioObject.transform.SetParent(transform);
            source = audioObject.AddComponent<AudioSource>();
            audioSources[clip] = source;
        }
        return source;
    }

    public void PlayAudio(AudioClip clip)
    {
        if (!IsPlaying(clip))
        {
            AudioSource source = GetOrCreateAudioSource(clip);
            source.clip = clip;
            source.loop = true; // 确保音频在播放时循环
            source.Play();
        }
    }

    public void PlayOneShot(AudioClip clip, bool isRepeat = false)
    {
        if (isRepeat || !currentlyPlayingClips.Contains(clip))
        {
            AudioSource source = GetOrCreateAudioSource(clip);
            source.PlayOneShot(clip);
            StartCoroutine(RemoveClipFromPlaying(clip, clip.length));
        }
    }

    private IEnumerator RemoveClipFromPlaying(AudioClip clip, float delay)
    {
        currentlyPlayingClips.Add(clip);
        yield return new WaitForSeconds(delay);
        currentlyPlayingClips.Remove(clip);
        RecycleAudioSource(clip);
    }

    public void StopAudio(AudioClip clip)
    {
        if (audioSources.TryGetValue(clip, out AudioSource source))
        {
            source.Stop();
        }
    }

    public void PauseAudio(AudioClip clip)
    {
        if (audioSources.TryGetValue(clip, out AudioSource source))
        {
            source.Pause();
        }
    }

    public void ResumeAudio(AudioClip clip)
    {
        if (audioSources.TryGetValue(clip, out AudioSource source))
        {
            source.UnPause();
        }
    }

    public bool IsPlaying(AudioClip clip)
    {
        return audioSources.TryGetValue(clip, out AudioSource source) && source.isPlaying;
    }

    private void RecycleAudioSource(AudioClip clip)
    {
        if (audioSources.TryGetValue(clip, out AudioSource source) && !source.isPlaying)
        {
            Destroy(source.gameObject);
            audioSources.Remove(clip);
        }
    }
}