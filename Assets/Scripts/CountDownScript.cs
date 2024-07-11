using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CountDownScript : MonoBehaviour
{
    Text countDownText;
    int countTimer = 3;
    public GameObject countDownPopUp;
    private void OnEnable()
    {
        countTimer = 3;
        Time.timeScale = 0;
        countDownText = this.GetComponent<Text>();
        StartCoroutine(CountDownFn());

    }
    IEnumerator CountDownFn()
    {
        if (countTimer > 0)
        {
            countDownText.text = "" + countTimer;
            yield return new WaitForSecondsRealtime(1);
            countTimer--;
            StartCoroutine(CountDownFn());
        }
        else
        {
            countDownPopUp.SetActive(false);
            Time.timeScale = 1;
        }
    }

}
