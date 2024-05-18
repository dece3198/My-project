using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Generator : MonoBehaviour
{
    public static Generator instance;
    [SerializeField] private List<GameObject> guests = new List<GameObject>();
    [SerializeField] private List<GameObject> guestPool = new List<GameObject>();
    [SerializeField] private Slot[] slots;
    [SerializeField] private GameObject[] destination;
    [SerializeField] private NavManager navManager;
    private Dictionary<ClassType, int> itemTypes = new Dictionary<ClassType, int>();
    private GameObject curGuest;
    int rand = 0;
    public bool isItem = false;

    private void Awake()
    {
        instance = this;
        itemTypes.Add(ClassType.Normal, 20);
        itemTypes.Add(ClassType.Rare, 15);
        itemTypes.Add(ClassType.Unique, 10);
        itemTypes.Add(ClassType.Legend, 5);
    }

    private void Start()
    {
        for (int i = 0; i < guests.Count; i++)
        {
            for (int j = 0; j < 5; j++)
            {
                GameObject guest = Instantiate(guests[i]);
                guest.transform.parent = transform;
                guest.transform.position = transform.position;
                guestPool.Add(guest);
                guest.SetActive(false);
            }
        }
    }

    private void Update()
    {
        if(slots.Length > 0)
        {
            for(int i = 0; i < slots.Length; i++)
            {
                if (slots[i].item != null)
                {
                    if (isItem)
                    {
                        if (slots[i].buyType == BuyType.None)
                        {
                            StartCoroutine(GuestCo(i));
                            slots[i].buyType = BuyType.Sale;
                            isItem = false;
                        }

                    }
                }
            }
        }
    }
    private IEnumerator GuestCo(int _count)
    {
        yield return new WaitForSeconds(itemTypes[slots[_count].itemClass]);
        GuestExitPool();
        curGuest.GetComponent<GuestController>().buyItem = slots[_count];
        curGuest.GetComponent<GuestController>().destination = navManager.gameObject;
        curGuest.GetComponent<GuestController>().slot = slots[_count];
        curGuest.GetComponent<GuestController>().ChangeState(GuestState.Walk);
        navManager.destination = destination[_count];
    }

    private void GuestExitPool()
    {
        rand = Random.Range(0, guestPool.Count);
        curGuest = guestPool[rand];
        guestPool.Remove(guestPool[rand]);
        curGuest.SetActive(true);
    }

    public void GuestEnterPool(GameObject _guest)
    {
        _guest.gameObject.SetActive(false);
        _guest.transform.position = transform.position;
        guestPool.Add(_guest);
    }
}
