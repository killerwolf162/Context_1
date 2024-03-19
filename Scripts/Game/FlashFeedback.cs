using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FlashFeedback : MonoBehaviour
{

    public Image image;
    [SerializeField]
    private float invisable_time, visable_time;

    private void Awake()
    {
        image = GetComponent<Image>();
    }

    private void Start()
    {
        StartCoroutine(flash_coroutine());
    }

    private IEnumerator flash_coroutine()
    {
        image.enabled = true;
        yield return new WaitForSeconds(invisable_time);

        image.enabled = false;
        yield return new WaitForSeconds(visable_time);
        StartCoroutine(flash_coroutine());
    }


}
