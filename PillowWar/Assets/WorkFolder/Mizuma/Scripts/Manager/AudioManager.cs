using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// TODO:試遊会用

public class AudioManager : SingletonMonoBehaviour<AudioManager>
{
    [SerializeField] private GameObject bgmObj;
    [SerializeField] private GameObject seObj;
    [SerializeField] private AudioClip[] bgmClips;
    [SerializeField] private AudioClip[] seClips;
    [SerializeField] private int bgmChannelValue;
    [SerializeField] private int seChannelValue;

    private List<AudioSource> bgmSources = new List<AudioSource>();
    private List<AudioSource> seSources = new List<AudioSource>();

    private void Start()
    {
        for (int i = 0; i < bgmChannelValue; i++)
        {
            MakeAudioSource(AudioType.BGM);
        }

        for (int i = 0; i < seChannelValue; i++)
        {
            MakeAudioSource(AudioType.SE);
        }
    }

    /// <summary>
    /// BGMを再生する
    /// </summary>
    /// <param name="bgmName">再生するClip</param>
    /// <returns>再生に成功したか</returns>
    public bool BGMPlay(BGMName bgmName)
    {
        AudioSource source = FindNotUseAudioSource(AudioType.BGM, false);

        if (source.isPlaying)
        {
            Debug.LogError("非再生中のAusioSource持ってきて");
            return false;
        }

        source.clip = bgmClips[(int)bgmName];
        source.Play();
        return true;
    }

    /// <summary>
    /// SEを再生する
    /// </summary>
    /// <param name="seName">再生するClip</param>
    /// <returns>再生に成功したか</returns>
    public bool SEPlay(SEName seName)
    {
        AudioSource source = FindNotUseAudioSource(AudioType.SE, true);

        if (source.isPlaying)
        {
            Debug.LogError("非再生中のAusioSource持ってきて");
            return false;
        }

        source.clip = seClips[(int)seName];
        source.Play();
        return true;
    }

    /// <summary>
    /// 再生していないAudioSourceを見つけてくる
    /// </summary>
    /// <param name="type">BGM or SE</param>
    /// <param name="noHitIsMakeAudioSource">全て再生中の際新たにAudioSourceを作るか</param>
    /// <returns>非再生中のAudioSource</returns>
    private AudioSource FindNotUseAudioSource(AudioType type, bool noHitIsMakeAudioSource)
    {
        if (type == AudioType.BGM)
        {
            foreach (AudioSource source in bgmSources)
            {
                if (source.isPlaying == false)
                {
                    return source;
                }
            }

            if (noHitIsMakeAudioSource)
            {
                AudioSource newSource = MakeAudioSource(AudioType.BGM);
                bgmSources.Add(newSource);
                return newSource;
            }
        }
        else
        {
            foreach (AudioSource source in seSources)
            {
                if (source.isPlaying == false)
                {
                    return source;
                }
            }

            if (noHitIsMakeAudioSource)
            {
                AudioSource newSource = MakeAudioSource(AudioType.SE);
                seSources.Add(newSource);
                return newSource;
            }
        }

        return FindUseAudioSource(type);
    }

    /// <summary>
    /// 再生中のAudioSourceを見つけてくる
    /// </summary>
    /// <param name="type">BGM or SE</param>
    /// <returns>止めて非再生中になったAudioSource</returns>
    private AudioSource FindUseAudioSource(AudioType type)
    {
        if (type == AudioType.BGM)
        {
            foreach (AudioSource source in bgmSources)
            {
                if (source.isPlaying == true)
                {
                    source.Stop();
                    return source;
                }
            }
        }
        else
        {
            foreach (AudioSource source in seSources)
            {
                if (source.isPlaying == true)
                {
                    source.Stop();
                    return source;
                }
            }
        }
        Debug.LogError("使用,未使用AudioSource無し");
        return null;
    }

    /// <summary>
    /// 新たにAudioSourceを作る
    /// </summary>
    /// <param name="type">BGM or SE</param>
    /// <returns>作られたAudioSource</returns>
    private AudioSource MakeAudioSource(AudioType type)
    {
        AudioSource source;

        if (type == AudioType.BGM)
        {
            source = bgmObj.AddComponent<AudioSource>();
            bgmSources.Add(source);
            source.playOnAwake = true;
            source.loop = true;
        }
        else
        {
            source = seObj.AddComponent<AudioSource>();
            seSources.Add(source);
            source.playOnAwake = false;
            source.loop = false;
        }

        return source;
    }
}

public enum AudioType
{
    BGM,
    SE
}

public enum BGMName
{
    Name01,
    Name02,
    Name03
}

public enum SEName
{
    Name01,
    Name02,
    Name03
}