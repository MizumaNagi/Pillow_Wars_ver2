using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerManager : SingletonMonoBehaviour<PlayerManager>
{
    [SerializeField] private GameObject[] playerPrefabs;
    [SerializeField] private Transform charctersParent;

    private Vector3[] spawnPos = { new Vector3(-10, 0, -10), new Vector3(10, 0, 10), new Vector3(-10, 0, 10), new Vector3(10, 0, -10) };

    public List<CharacterData> charaDatas = new List<CharacterData>();

    public void Init()
    {
        for(int i = 0; i < GameManager.Instance.joinPlayers; i++)
        {
            GameObject obj = Instantiate(playerPrefabs[i], spawnPos[i], Quaternion.identity);
            obj.transform.GetChild(0).localPosition = Vector3.zero;
            obj.name = "Player" + (i + 1);
            obj.transform.SetParent(charctersParent, true);

            charaDatas.Add(new CharacterData(obj));
        }
    }
}
