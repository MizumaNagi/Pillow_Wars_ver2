using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ChangeButton : MonoBehaviour
{
    public Text PlayNambertext;

    public Sprite[] StageImage;

    int StageSelect_now = 0;

    int ColumnSelect_now = 0;

    public Image Spriteimage;

    public Button[] ColumnSelect;

    public Button PlayerSelect;

    public Button StageSelect;

    private void Start()
    {
        SetSprite();
        PlayerSelect.Select();
    }

    private void Update()
    {
        if(Input.GetKeyDown(KeyCode.LeftArrow))
        {
            if(ColumnSelect_now == 0)
            {
                OnPlayLeftArrow();
            }
            else if(ColumnSelect_now == 1)
            {
                OnStageLeftArrow();
            }
        }
        if(Input.GetKeyDown(KeyCode.RightArrow))
        {
            if (ColumnSelect_now == 0)
            {
                OnPlayRightArrow();
            }
            else if (ColumnSelect_now == 1)
            {
                OnStageRightArrow();
            }
        }
        if(Input.GetKeyDown(KeyCode.UpArrow))
        {
            if(ColumnSelect_now != 0)
            {
                ColumnSelect_now--;
            }
        }
        if(Input.GetKeyDown(KeyCode.DownArrow))
        {
            if(ColumnSelect_now == 0)
            {
                ColumnSelect_now++;
            }
        }
        if(Input.GetKeyDown(KeyCode.Space))
        {
            int PlayerjoinNumbers = int.Parse(PlayNambertext.text.ToString());
            GameManager.Instance.joinPlayers = PlayerjoinNumbers;
            GameManager.Instance.joinNpcs = 6 - PlayerjoinNumbers;
        }
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
