using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager 
{
    AudioSource[] _audioSources = new AudioSource[(int)Define.SoundMode.MaxCount];

    AudioClip _bgmClip = null;
    Dictionary<string, AudioClip> _audioClips = new Dictionary<string, AudioClip>();

    //이용 방법 - LoadClip으로 일단 로드 후에 Play할 것
    public void Init()
    {
        GameObject root = GameObject.Find("@Sound");
        if(root == null)
        {
            root = new GameObject { name = "@Sound" };
            Object.DontDestroyOnLoad(root);

            string[] soundNames = System.Enum.GetNames(typeof(Define.SoundMode));
            for(int i = 0; i < soundNames.Length - 1; i++)
            {
                GameObject go = new GameObject { name = soundNames[i] };
                _audioSources[i] = go.AddComponent<AudioSource>();
                go.transform.parent = root.transform;
            }

            _audioSources[(int)Define.SoundMode.BGM].loop = true;
        }
    }

    public void Play(AudioClip clip, Define.SoundMode type = Define.SoundMode.Effect, float pitch = 1.0f)
    {
        if(clip == null)
        {
            Debug.Log($"Audio clip does not found");
            return;
        }

        if(type == Define.SoundMode.BGM)
        {
            AudioSource source = _audioSources[(int)Define.SoundMode.BGM];
            if (source.isPlaying == true)
                source.Stop();
            source.clip = clip;
            source.pitch = pitch;
            source.Play();
        }
        else
        {
            AudioSource source = _audioSources[(int)Define.SoundMode.Effect];
            source.pitch = pitch;
            source.PlayOneShot(clip);
        }
    }

    public void Play(string key, Define.SoundMode type = Define.SoundMode.Effect, float pitch = 1.0f)
    {
        AudioClip clip = null;

        if(type == Define.SoundMode.BGM)
        {
            clip = _bgmClip;
        }
        else
        {
            if(_audioClips.TryGetValue(key, out clip) == false)
            {
                Debug.LogError($"You need to load clip first {key}");
                return;
            }
        }

        Play(clip, type, pitch);
    }

    /*public void Play(string path, Define.SoundMode type = Define.SoundMode.Effect, float pitch = 1.0f)
    {
        AudioClip clip = GetOrAddClip(path, type);
        Play(clip, type, pitch);
    }*/

    /*public AudioClip GetOrAddClip(string path, Define.SoundMode type = Define.SoundMode.Effect)
    {
        if (path.Contains("Sound/") == false)
            path = $"Sound/{path}";

        AudioClip clip = null;

        if(type == Define.SoundMode.BGM)
        {
            clip = Managers.Resource.Load<AudioClip>(path, null).Result as AudioClip;
        }
        else
        {
            if(_audioClips.TryGetValue(path, out clip) == false)
            {
                clip = Managers.Resource.Load<AudioClip>(path, null).Result as AudioClip;
                _audioClips.Add(path, clip);
            }
        }
        if (clip == null)
            Debug.Log($"Audio Clip does not found : {path}");

        return clip;
    }*/

    public void LoadClip(string key, Define.SoundMode type = Define.SoundMode.Effect)
    {
        if (key.Contains("Sound/") == false)
            key = $"Sound/{key}";

        if(type == Define.SoundMode.BGM)
        {
            Managers.Resource.Load<AudioClip>(key, BGMAudioClipLoad);
        }
        else
        {
            AudioClip clip = null;
            if(_audioClips.TryGetValue(key, out clip) == false)
            {
                Managers.Resource.Load<AudioClip>(key, EffectAudioClipLoad);
            }
        }
    }

    private void BGMAudioClipLoad(AudioClip clip)
    {
        _bgmClip = clip;
    }
    private void EffectAudioClipLoad(AudioClip clip)
    {
        _audioClips.Add(clip.name, clip);
    }

    public void Clear()
    {
        foreach (AudioSource source in _audioSources)
        {
            source.clip = null;
            source.Stop();
        }
        _audioClips.Clear();
    }
}
