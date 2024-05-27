using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Destination : MonoBehaviour
{
    public static Destination instance;
    public GuestController controller;
    public GuestController eventController;

    private void Awake()
    {
        instance = this;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.GetComponent<GuestController>() != null)
        {
            if (other.GetComponent<GuestController>().transform.tag == "Event")
            {
                if(other.GetComponent<GuestController>() == eventController)
                {
                    other.GetComponent<GuestController>().ChangeState(GuestState.Event);
                }
            }
            else
            {
                if(other.GetComponent<GuestController>() == controller)
                {
                    other.GetComponent<GuestController>().ChangeState(GuestState.Worry);
                    other.GetComponent<GuestController>().transform.LookAt(transform.parent.transform.position);
                }
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if(other.GetComponent<GuestController>() != null)
        {
            if(controller != null)
            {
                if (other.GetComponent<GuestController>() == controller)
                {
                    controller = null;
                }
            }
        }

        if(other.GetComponent<GuestController>().transform.tag == "Event")
        {
            if(eventController != null)
            {
                if(other.GetComponent<GuestController>() == eventController)
                {
                    eventController = null;
                }
            }
        }
    }
}
