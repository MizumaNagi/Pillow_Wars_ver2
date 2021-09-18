using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ChangeButton : MonoBehaviour
{
    int PlayerjoinNumbers = 2;

    public Text PlayNambertext;

    public Sprite[] StageImage;

    [SerializeField] int StageSelect_now = 0;

    [SerializeField] int ColumnSelect_now = 0;

    public Image Spriteimage;

    public Button[] ColumnSelect;

    public Button PlayerSelect;

    public Button StageSelect;

    private float moveXCT = 0.3f;
    private float remainMoveXCT = 0f;
    private float inputThreshold = 0.8f;

    private void Start()
    {
        SetSprite();
        PlayerSelect.Select();
    }

    private void Update()
    {
        remainMoveXCT -= Time.deltaTime;

        if (Input.GetKeyDown(KeyCode.LeftArrow) || (Input.GetAxis(InputManager.Instance.playerInput[0].MoveX) < -inputThreshold && remainMoveXCT < 0))
        {
            if(ColumnSelect_now == 0)
            {
                OnPlayLeftArrow();
            }
            else if(ColumnSelect_now == 1)
            {
                OnStageLeftArrow();
            }
            remainMoveXCT = moveXCT;
        }
        if(Input.GetKeyDown(KeyCode.RightArrow) || (Input.GetAxis(InputManager.Instance.playerInput[0].MoveX) > inputThreshold && remainMoveXCT < 0))
        {
            if (ColumnSelect_now == 0)
            {
                OnPlayRightArrow();
            }
            else if (ColumnSelect_now == 1)
            {
                OnStageRightArrow();
            }
            remainMoveXCT = moveXCT;
        }
        if(Input.GetKeyDown(KeyCode.UpArrow) || Input.GetAxis(InputManager.Instance.playerInput[0].MoveY) < -inputThreshold)
        {
            if (ColumnSelect_now != 0)
            {
                ColumnSelect_now--;
            }
        }
        if(Input.GetKeyDown(KeyCode.DownArrow) || Input.GetAxis(InputManager.Instance.playerInput[0].MoveY) > inputThreshold)
        {
            if (ColumnSelect_now == 0)
            {
                ColumnSelect_now++;
            }
        }
        if(Input.GetKeyDown(KeyCode.Space) || Input.GetButtonDown(InputManager.Instance.playerInput[0].Start))
        {
            GameManager.Instance.joinPlayers = PlayerjoinNumbers;
            GameManager.Instance.joinNpcs = 6 - PlayerjoinNumbers;
            GameManager.Instance.selectStageNo = StageSelect_now;
            SceneController.Instance.LoadLoadingScene(SCENE_NAME.SELECT, SCENE_NAME.GAME);
        }
    }

    public void OnPlayLeftArrow()
    {
        PlayerjoinNumbers = 2;
        PlayNambertext.text = "Player:2\n<color=red>NPC:4</color>";
    }

    public void OnPlayRightArrow()
    {
        PlayerjoinNumbers = 4;
        PlayNambertext.text = "Player:4\n<color=red>NPC:2</color>";
    }

    public void OnStageLeftArrow()
    {
        if(StageSelect_now > 0)
        {
            StageSelect_now--;
            SetSprite();
        }
    }

    public void OnStageRightArrow()
    {
        if(StageSelect_now < 1)
        {
            StageSelect_now++;
            SetSprite();
        }
    }

    public void SetSprite()
    {
        Spriteimage.sprite = StageImage[StageSelect_now];
    }

}
