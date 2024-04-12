using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class GoldManager : Singleton<GoldManager>
{
    [SerializeField] private TextMeshProUGUI goldText;
    public int gold;

    private void Update()
    {
        goldText.text = gold.ToString();
    }
}
