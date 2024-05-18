using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NavManager : MonoBehaviour
{
    public GameObject destination;
    public GameObject exit;

    private void OnTriggerEnter(Collider other)
    {
        if(other.GetComponent<GuestController>() != null)
        {
            if(other.GetComponent<GuestController>().state == GuestState.Walk)
            {
                other.GetComponent<GuestController>().agent.SetDestination(destination.transform.position);
                other.GetComponent<GuestController>().navManager = this;
            }
            else
            {
                other.GetComponent<GuestController>().agent.SetDestination(exit.transform.position);
            }
        }
    }
}
