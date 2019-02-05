using UnityEngine.Audio;
using System;
using UnityEngine;
using System.Collections;

public class AudioManager : MonoBehaviour {

    public static AudioManager Instance;

    public AudioMixerGroup musicMixer;
    public AudioMixerGroup sfxMixer;
    public Sound[] sounds;

	// Use this for initialization
	void Awake () {

        if (Instance == null)
            Instance = this;
        else
        {
            Destroy(gameObject);
            return;
        }

        foreach (Sound s in sounds)
        {
            s.source = gameObject.AddComponent<AudioSource>();
            s.source.clip = s.clip;

            s.source.volume = s.volume;
            s.source.pitch = s.pitch;
            s.source.loop = s.loop;
            
            switch (s.soundType)
            {
                case Sound.SoundType.Music:
                    s.source.outputAudioMixerGroup = musicMixer;
                    break;
                case Sound.SoundType.SFX:
                    s.source.outputAudioMixerGroup = sfxMixer;
                    break;
            }
        }
	}
	
    /// <summary>
    /// Play the Audioclip at the set pitch.
    /// </summary>
    /// <param name="name">Name of the Audioclip</param>
	public void Play(string name)
    {
        // In 'sounds' find 'sound' where 'sound.name' equals 'name'
        Sound s = Array.Find(sounds, sound => sound.name == name);
        if (s == null)
        {
            Debug.LogError("Sound: " + name + " not found!");
            return;
        }

        s.source.Play();
    }

    /// <summary>
    /// Play the Audioclip at a random pitch between minPitch and maxPitch.
    /// </summary>
    /// <param name="name">Name of the Audioclip</param>
    /// <param name="minPitch">Minimum pitch</param>
    /// <param name="maxPitch">Maximum pitch</param>
    public void PlayWithRandomPitch(string name, float minPitch, float maxPitch)
    {
        Sound s = Array.Find(sounds, sound => sound.name == name);
        if (s == null)
        {
            Debug.LogError("Sound: " + name + " not found!");
            return;
        }

        float randomPitch = UnityEngine.Random.Range(minPitch, maxPitch);

        s.source.pitch = randomPitch;
        s.source.Play();
    }

    /// <summary>
    /// Change the pitch of the AudioClip
    /// </summary>
    /// <param name="name">Name of the Audioclip</param>
    /// <param name="pitch">New pitch</param>
    public void ChangePitch(string name, float pitch)
    {
        Sound s = Array.Find(sounds, sound => sound.name == name);
        if (s == null)
        {
            Debug.LogError("Sound: " + name + " not found!");
            return;
        }

        s.source.pitch = pitch;
    }
}
