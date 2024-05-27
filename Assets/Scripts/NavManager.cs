using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NavManager : MonoBehaviour
{
    public GameObject exit;

    private void OnTriggerEnter(Collider other)
    {
        if(other.GetComponent<GuestController>() != null)
        {
            if (other.GetComponent<GuestController>().state == GuestState.Walk)
            {
                other.GetComponent<GuestController>().agent.SetDestination(other.GetComponent<GuestController>().destination.transform.position);
            }
            else
            {
                other.GetComponent<GuestController>().agent.SetDestination(exit.transform.position);
            }
        }
    }
}
