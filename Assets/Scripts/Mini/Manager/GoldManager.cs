using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class GoldManager : MonoBehaviour
{
    public static GoldManager instance;
    [SerializeField] private TextMeshProUGUI goldText;
    public int gold;

    private void Awake()
    {
        instance = this;
        gold = 100;
    }

    private void Update()
    {
        goldText.text = gold.ToString();
    }
}
