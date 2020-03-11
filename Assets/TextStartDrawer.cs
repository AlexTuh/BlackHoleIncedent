using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TextStartDrawer : MonoBehaviour
{
    [SerializeField] private PlayerController2D Player;
    [SerializeField] private GameObject Text;
    [SerializeField] private float seconds = 3f;
    private void Awake()
    {
        Text.SetActive(false);
        StartCoroutine(DrawText());
    }
    IEnumerator DrawText()
    {
        yield return new WaitForSeconds(0.1f);
        Player.canWalk = false;
        yield return new WaitForSeconds(1);
        Text.SetActive(true);
        yield return new WaitForSeconds(seconds);
        Text.SetActive(false);
        Player.canWalk = true;
    }
}
