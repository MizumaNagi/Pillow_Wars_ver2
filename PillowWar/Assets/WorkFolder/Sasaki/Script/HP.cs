using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class HP : MonoBehaviour
{

    int maxHp = 100;
    int currentHp;
    public Slider slider;

    // Start is called before the first frame update
    void Start()
    {
        slider.value = 1;
        currentHp = maxHp;
    }

    private void OnTriggerEnter(Collider collider)
    {
        if (collider.gameObject.tag == "Makera")
        {
            int damage = 20;

            currentHp = currentHp - damage;

            slider.value = (float)currentHp / (float)maxHp;

            // HP‚ª0‚É‚È‚Á‚½‚çŠÏí‰æ–Ê‚É‘JˆÚ‚·‚é
            if (currentHp == 0)
            {
                SceneManager.LoadScene("ŠÏí‰æ–Ê");
            }

        }
    }

    // Update is called once per frame
    void Update()
    {

    }

}
