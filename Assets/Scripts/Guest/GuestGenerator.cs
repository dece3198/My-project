using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class GuestGenerator : Singleton<GuestGenerator>
{
    [SerializeField] private GameObject guestParent;
    [SerializeField] private List<GameObject> guests = new List<GameObject>();
    [SerializeField] private List<GameObject> guestPool = new List<GameObject>();
    int rand = 0;

    private void Start()
    {
        for(int i = 0; i < guests.Count; i++)
        {
            for(int j = 0; j < 5; j++)
            {
                GameObject guest = Instantiate(guests[i]);
                guest.transform.parent = transform;
                guest.transform.position = transform.position;
                guestPool.Add(guest);
                guest.SetActive(false);
            }
        }
    }


    public IEnumerator GuestCo()
    {
        for (int i = 0; i < 4; i++)
        {
            GuestExitPool();
            yield return new WaitForSeconds(15f);
        }
    }

    private void GuestExitPool()
    {
        rand = Random.Range(0, guestPool.Count);
        GameObject guest = guestPool[rand];
        guestPool.Remove(guestPool[rand]);
        guest.SetActive(true);
    }

    public void GuestEnterPool(GameObject _guest)
    {
        _guest.gameObject.SetActive(false);
        _guest.transform.position = transform.position;
        guestPool.Add(_guest);
    }

}
