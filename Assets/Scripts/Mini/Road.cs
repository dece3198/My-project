using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Road : MonoBehaviour
{
    public Guest guest;
    public bool isGuest = false;
    public bool isClose = false;
    [SerializeField] private GameObject exitRoad;

    private void Update()
    {
        if(guest != null)
        {
            if (transform.tag == "Road")
            {
                if (guest.guestState == GuestState.Out)
                {
                    guest.agent.SetDestination(exitRoad.transform.position);
                }
            }

            if(isClose)
            {
                guest.ChangeState(GuestState.Out);
                isClose = false;
            }
        }
        
    }

    private void OnTriggerEnter(Collider other)
    {
        if(guest == null)
        {
            if(isGuest)
            {
                guest = other.GetComponent<Guest>();
                if (transform.tag == "Road")
                {
                    guest.ChangeState(GuestState.Buy);
                    return;
                }
                guest.ChangeState(GuestState.Idle);
                isGuest = false;
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        guest = null;
    }
}
