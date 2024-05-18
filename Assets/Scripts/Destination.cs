using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Destination : MonoBehaviour
{

    private void OnTriggerEnter(Collider other)
    {
        if(other.GetComponent<GuestController>() != null)
        {
            other.GetComponent<GuestController>().ChangeState(GuestState.Worry);
        }
    }
}
