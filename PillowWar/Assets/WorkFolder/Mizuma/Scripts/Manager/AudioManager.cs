using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : SingletonMonoBehaviour<AudioManager>
{
    [SerializeField] private GameObject bgmObj;
    [SerializeField] private GameObject seObj;
    [SerializeField] private AudioClip[] bgmClips;
    [SerializeField] private float bgmVolume;
    [SerializeField] private AudioClip[] seClips;
    [SerializeField] private float seVolume;
    [SerializeField] private int bgmChannelValue;
    [SerializeField] private int seChannelValue;

    [SerializeField] private List<AudioSource> bgmSources = new List<AudioSource>();
    [SerializeField] private List<AudioSource> seSources = new List<AudioSource>();

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
    public bool BGMPlay(BGMName bgmName, float waitTime = 0f)
    {
        AudioSource source = FindNotUseAudioSource(AudioType.BGM, false);

        if (source == null)
        {
            Debug.LogError("AudioSource無いじゃん");
            return false;
        }

        source.volume = bgmVolume;
        source.clip = bgmClips[(int)bgmName];

        if (waitTime != 0f) StartCoroutine(DelayBgmPlay(waitTime, source));
        else source.Play();

        return true;
    }

    private IEnumerator DelayBgmPlay(float waitTime, AudioSource source)
    {
        yield return new WaitForSeconds(waitTime);
        source.Play();
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

        source.volume = seVolume;
        source.clip = seClips[(int)seName];
        source.time = 0;

        switch (seName)
        {
            case SEName.Walk:
                {
                    source.pitch = Random.Range(0.9f, 1.1f);
                    source.volume *= 0.7f;
                    break;
                }
            case SEName.Run:
                {
                    source.pitch = Random.Range(0.9f, 1.1f);
                    source.volume *= 0.4f;
                    break;
                }
            case SEName.HitPillow:
                {
                    source.time = 0.55f;
                    break;
                }
            case SEName.InBed:
                {
                    break;
                }
            case SEName.TeacherAppears:
                {
                    source.volume *= 0.7f;
                    break;
                }
            case SEName.OpenDoor:
                {
                    break;
                }
            case SEName.CloseDoor:
                {
                    break;
                }
            case SEName.GetItem:
                {
                    break;
                }
        }

        source.minDistance = 5000;
        source.maxDistance = 30000;
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
                return source;
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
    Title,
    Select,
    Main,
    Result
}

public enum SEName
{
    Walk,
    Run,
    HitPillow,
    InBed,
    TeacherAppears,
    OpenDoor,
    CloseDoor,
    GetItem
}