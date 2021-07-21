using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// TODO:���V��p

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
    /// BGM���Đ�����
    /// </summary>
    /// <param name="bgmName">�Đ�����Clip</param>
    /// <returns>�Đ��ɐ���������</returns>
    public bool BGMPlay(BGMName bgmName)
    {
        AudioSource source = FindNotUseAudioSource(AudioType.BGM, false);

        if (source.isPlaying)
        {
            Debug.LogError("��Đ�����AusioSource�����Ă���");
            return false;
        }

        source.clip = bgmClips[(int)bgmName];
        source.Play();
        return true;
    }

    /// <summary>
    /// SE���Đ�����
    /// </summary>
    /// <param name="seName">�Đ�����Clip</param>
    /// <returns>�Đ��ɐ���������</returns>
    public bool SEPlay(SEName seName)
    {
        AudioSource source = FindNotUseAudioSource(AudioType.SE, true);

        if (source.isPlaying)
        {
            Debug.LogError("��Đ�����AusioSource�����Ă���");
            return false;
        }

        source.clip = seClips[(int)seName];
        source.Play();
        return true;
    }

    /// <summary>
    /// �Đ����Ă��Ȃ�AudioSource�������Ă���
    /// </summary>
    /// <param name="type">BGM or SE</param>
    /// <param name="noHitIsMakeAudioSource">�S�čĐ����̍ېV����AudioSource����邩</param>
    /// <returns>��Đ�����AudioSource</returns>
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
    /// �Đ�����AudioSource�������Ă���
    /// </summary>
    /// <param name="type">BGM or SE</param>
    /// <returns>�~�߂Ĕ�Đ����ɂȂ���AudioSource</returns>
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
        Debug.LogError("�g�p,���g�pAudioSource����");
        return null;
    }

    /// <summary>
    /// �V����AudioSource�����
    /// </summary>
    /// <param name="type">BGM or SE</param>
    /// <returns>���ꂽAudioSource</returns>
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