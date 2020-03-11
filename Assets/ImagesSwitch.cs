using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ImagesSwitch : MonoBehaviour
{
    [SerializeField] private SpriteRenderer BackGround;
    [SerializeField] private SceneReloaderScript Reloader;
    [SerializeField] private Text Text;
    [SerializeField] private List<ImageAndText> ImageList;
    [SerializeField] private float timeToWait = 5;
    private float timeRem = 0;
    private int image = -1;
    private bool newText = false;
    void Update()
    {
        if(timeRem <= 0)
        {
            if (image == ImageList.Count)
            {
                Reloader.ReStart(true);
            }
            else
            {
                SwitchImageAndText();
                timeRem = timeToWait;
                newText = true;
            }
        }
        else
        {
            timeRem = timeRem - Time.deltaTime;
        }
    }
    void SwitchImageAndText()
    {
        image++;
        BackGround.sprite = ImageList[image].Image;
        Text.text = "";
        StartCoroutine(DrawText(ImageList[image].Text));
    }
    IEnumerator DrawText(string text)
    {
        while (text.Length != 0)
        {
            Text.text = Text.text + text[0];
            text = text.Substring(1);
            yield return new WaitForSeconds(0.05f);
            Debug.Log(text);
        }
    }
}
