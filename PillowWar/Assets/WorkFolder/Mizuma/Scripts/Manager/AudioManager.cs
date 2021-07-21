using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : SingletonMonoBehaviour<AudioManager>
{
    [SerializeField] private int bgmChannelValue;
    [SerializeField] private int seChannelValue;


    private void Start()
    {
        for(int i = 0; i < bgmChannelValue; i++)
        {

        }
    }
}
