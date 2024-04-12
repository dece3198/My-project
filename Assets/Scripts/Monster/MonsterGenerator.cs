using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonsterGenerator : MonoBehaviour
{
    [SerializeField] private Stage stage;
    [SerializeField] private List<Transform> Pos = new List<Transform>();

    private void OnTriggerEnter(Collider other)
    {
        if(other.GetComponent<PlayerController>() != null)
        {

        }
    }

}
