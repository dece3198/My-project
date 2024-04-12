using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoadManager : Singleton<RoadManager>
{
    public Road[] roads;

    private void Awake()
    {
        roads = transform.GetComponentsInChildren<Road>();
    }

    private void Update()
    {
        if (roads[0].guest != null)
        {
            if (roads[0].guest.guestState == GuestState.Out)
            {
                for (int i = 1; i < roads.Length; i++)
                {
                    if (roads[i].guest != null)
                    {
                        if(roads[i].guest.guestState == GuestState.Idle)
                        {
                            roads[i].guest.agent.SetDestination(roads[i - 1].transform.position);
                        }
                    }
                }
            }
        }
        else
        {
            for (int i = 1; i < roads.Length; i++)
            {
                if (roads[i].guest != null)
                {
                    if (roads[i].guest.guestState == GuestState.Idle)
                    {
                        roads[i].isGuest = true;
                        roads[i].guest.agent.SetDestination(roads[i - 1].transform.position);
                    }
                }
            }
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.GetComponent<Guest>() != null)
        {
            if(other.GetComponent<Guest>().guestState == GuestState.Walk)
            {
                Bell.instance.Add(1);
                for (int i = 0; i < roads.Length; i++)
                {
                    if (roads[i].guest == null)
                    {
                        roads[i].isGuest = true;
                        other.GetComponent<Guest>().agent.SetDestination(roads[i].transform.position);
                        return;
                    }
                }
            }
        }
    }
}
