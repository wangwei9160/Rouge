using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using static UnityEditor.Progress;

public class NoticeInfoUI : MonoBehaviour 
{
    public Image[] images;
    public Text message;

    public float mCurrentSecond = 0;
    public float startTime = 0.3f;
    public float speed = 40f;
    public float fadeDuration = 2f;

    public void Start()
    {
        images = GetComponentsInChildren<Image>();
        message = gameObject.transform.Find("Text").GetComponent<Text>();
    }

    private void Update()
    {
        mCurrentSecond += Time.deltaTime;

        if (mCurrentSecond >= startTime)
        {
            float fadeProgress = (mCurrentSecond - startTime) / fadeDuration;
            fadeProgress = Mathf.Clamp01(fadeProgress);
            if(fadeProgress >= 0.8f)
            {
                Destroy(gameObject);
                return;
            }
            foreach (var item in images)
            {
                Color color = item.color;
                color.a = 1 - fadeProgress;
                item.color = color;
            }
            var c = message.color;
            c.a = 1 - fadeProgress;
            message.color = c;
            gameObject.transform.position += new Vector3(0, speed * Time.deltaTime , 0);
        }
    }
    public void SetInfo(string info)
    {
        message.text = info;
    }

}