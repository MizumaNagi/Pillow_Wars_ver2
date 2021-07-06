using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ChangeButton : MonoBehaviour
{
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
            int PlayerjoinNumbers = int.Parse(PlayNambertext.text.ToString());
            GameManager.Instance.joinPlayers = PlayerjoinNumbers;
            GameManager.Instance.joinNpcs = 6 - PlayerjoinNumbers;
            SceneController.Instance.LoadScene(SCENE_NAME.GAME);
        }

        // Debug.Log($"moveX: {Input.GetAxis(InputManager.Instance.playerInput[0].MoveX)}\n" +
        //     $"moveY: {Input.GetAxis(InputManager.Instance.playerInput[0].MoveY)}");
    }

    public void OnPlayLeftArrow()
    {
        PlayNambertext.text = "2";
    }

    public void OnPlayRightArrow()
    {
        PlayNambertext.text = "4";
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
        if(StageSelect_now < 2)
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
