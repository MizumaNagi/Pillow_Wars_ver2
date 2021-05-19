using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerManager : SingletonMonoBehaviour<PlayerManager>
{
    [SerializeField] private GameObject[] playerPrefabs;
    [SerializeField] private GameObject pillowPrefab;
    [SerializeField] private Transform charctersParent;

    private Vector3[] spawnPos = { new Vector3(-10, 0, -10), new Vector3(10, 0, 10), new Vector3(-10, 0, 10), new Vector3(10, 0, -10) };

    public Transform pillowParent;
    public List<CharacterData> charaDatas = new List<CharacterData>();

    public void Init()
    {
        for(int i = 0; i < GameManager.Instance.joinPlayers; i++)
        {
            GameObject obj = Instantiate(playerPrefabs[i], spawnPos[i], Quaternion.identity);
            obj.transform.GetChild(0).localPosition = Vector3.zero;
            obj.name = "Player" + i;
            obj.transform.SetParent(charctersParent, true);

            GameObject pillow = Instantiate(pillowPrefab);
            pillow.name = i.ToString();
            pillow.transform.SetParent(obj.transform);
            pillow.transform.localPosition = InputManager.Instance.moveData.pillowSpawnPos;
            charaDatas.Add(new CharacterData(obj));

            // TODO:–³‘Ê‚ÈGetcomponent...
            //pillow.AddComponent<PillowController>();
            pillow.GetComponent<PillowController>().characterData = charaDatas[i];
        }

        InputManager.Instance.characterDatas = charaDatas;
    }


    public void UpdateMethod()
    {
        CoolTimeElapse();
    }

    private void Damage()
    {
        
    }

    private void CoolTimeElapse()
    {
        for (int i = 0; i < GameManager.Instance.joinPlayers; i++)
        {
            charaDatas[i].remainthrowCT -= Time.deltaTime;
        }
    }
}
