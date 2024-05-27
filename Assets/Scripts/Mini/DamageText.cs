using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class DamageText : MonoBehaviour
{
    public float moveSpeed;
    public float alphaSpeed;
    TextMeshProUGUI text;
    Color alpha;

    private void Awake()
    {
        text = GetComponent<TextMeshProUGUI>();
        alpha = text.color;
    }

    private void OnEnable()
    {
        alpha.a = 255f;
        if(transform.tag == "Warning")
        {
            StartCoroutine(TextCo());
        }
    }

    IEnumerator TextCo()
    {
        for(int i = 0; i < 2; i++)
        {
            yield return new WaitForSeconds(5f);
            alpha.a = 255f;
        }
        gameObject.SetActive(false);
    }

    private void Update()
    {
        transform.Translate(new Vector3(0, moveSpeed * Time.deltaTime, 0));
        alpha.a = Mathf.Lerp(alpha.a, 0, Time.deltaTime * alphaSpeed);
        text.color = alpha;

        if (transform.tag != "Warning")
        {
            if (alpha.a <= 1)
            {
                gameObject.SetActive(false);
            }
        }
    }
}
