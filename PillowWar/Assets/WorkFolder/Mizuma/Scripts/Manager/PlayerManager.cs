using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerManager : SingletonMonoBehaviour<PlayerManager>
{
    [SerializeField] private GameObject[] playerPrefabs;
    [SerializeField] private GameObject pillowPrefab;

    private Vector3[] spawnPos = { new Vector3(-10, 0, -10), new Vector3(10, 0, 10), new Vector3(-10, 0, 10), new Vector3(10, 0, -10) };
    private Transform charctersParent;
    public Transform PillowParent { get; private set; }

    public List<CharacterData> charaDatas = new List<CharacterData>();

    public void Init()
    {
        Debug.Log("Init");

        //GameObject emptyObj = Instantiate(new GameObject());
        GameObject emptyObj = new GameObject();
        charctersParent = emptyObj.transform;
        emptyObj.name = "CharaParent";
        
        //GameObject emptyObj2 = Instantiate(new GameObject());
        GameObject emptyObj2 = new GameObject();
        PillowParent = emptyObj2.transform;
        emptyObj2.name = "PillowParent";

        for (int i = 0; i < GameManager.Instance.joinPlayers; i++)
        {
            GameObject obj = Instantiate(playerPrefabs[i], spawnPos[i], Quaternion.identity);
            obj.transform.GetChild(0).localPosition = Vector3.zero;
            obj.name = "Player" + i;
            obj.transform.SetParent(charctersParent, true);

            GameObject pillow = Instantiate(pillowPrefab);
            pillow.name = i.ToString();
            pillow.transform.SetParent(obj.transform);
            pillow.transform.localPosition = InputManager.Instance.moveData.pillowSpawnPos;
            charaDatas.Add(new CharacterData(obj,i));

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
