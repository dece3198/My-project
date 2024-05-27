using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class GoldManager : MonoBehaviour
{
    public static GoldManager instance;
    [SerializeField] private TextMeshProUGUI goldText;
    public TextMeshProUGUI goldTextB;
    public int gold;

    private void Awake()
    {
        instance = this;
        gold = 10;
    }

    private void Update()
    {
        goldText.text = gold.ToString();
    }
}
