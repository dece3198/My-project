using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Generator : MonoBehaviour
{
    public static Generator instance;
    [SerializeField] private List<GameObject> guests = new List<GameObject>();
    [SerializeField] private List<GameObject> eventGuest = new List<GameObject>();
    [SerializeField] private List<GameObject> guestPool = new List<GameObject>();
    [SerializeField] private Slot[] slots;
    [SerializeField] private GameObject[] destination;
    [SerializeField] private NavManager navManager;
    [SerializeField] private GameObject warningText;
    private Dictionary<ClassType, int> itemTypes = new Dictionary<ClassType, int>();
    private GameObject curGuest;
    int rand = 0;

    private void Awake()
    {
        instance = this;
        itemTypes.Add(ClassType.Normal, 15);
        itemTypes.Add(ClassType.Rare, 10);
        itemTypes.Add(ClassType.Unique, 5);
        itemTypes.Add(ClassType.Legend, 0);
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
        StartCoroutine(EventCo());
    }

    private void Update()
    {
        if(slots.Length > 0)
        {
            for(int i = 0; i < slots.Length; i++)
            {
                if (slots[i].item != null)
                {
                    if (slots[i].buyType == BuyType.None)
                    {
                        IEnumerator enumerator = GuestCo(i);
                        StartCoroutine(enumerator);
                        slots[i].buyType = BuyType.Sale;
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
        curGuest.GetComponent<GuestController>().destination = destination[_count];
        curGuest.GetComponent<GuestController>().navManager = navManager;
        curGuest.GetComponent<GuestController>().ChangeState(GuestState.Walk);
        destination[_count].GetComponent<Destination>().controller = curGuest.GetComponent<GuestController>();
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

    private IEnumerator EventCo()
    {
        for(int i = 0; i < eventGuest.Count; i++)
        {
            yield return new WaitForSeconds(300);
            GameObject eGuest = Instantiate(eventGuest[i], transform);
            eGuest.transform.position = transform.position;
            eGuest.GetComponent<GuestController>().navManager = navManager;
            eGuest.GetComponent<GuestController>().destination = destination[1].gameObject;
            eGuest.GetComponent<GuestController>().ChangeState(GuestState.Walk);
            warningText.SetActive(true);
            destination[1].GetComponent<Destination>().eventController = eGuest.GetComponent<GuestController>();
            if (slots[1].item != null)
            {
                slots[1].item.classType = slots[1].itemClass;
                Inventory.instance.AcquireItem(slots[1].item);
                slots[1].ClearSlot();
                destination[1].GetComponent<Destination>().controller.ChangeState(GuestState.Out);
            }
        }
    }
}
