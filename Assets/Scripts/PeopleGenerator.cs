using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PeopleGenerator : MonoBehaviour
{
    [SerializeField] private List<GameObject> peopleList = new List<GameObject>();
    [SerializeField] private Stack<GameObject> peopleStack = new Stack<GameObject>();

    private void Start()
    {
        for (int i = 0; i < peopleList.Count; i++)
        {
            GameObject pp = Instantiate(peopleList[i], transform);
            pp.transform.position = transform.position;
            peopleStack.Push(pp);
            pp.gameObject.SetActive(false);
        }
    }

    private void GuestExitPool()
    {
        GameObject exitpp = peopleStack.Pop();
    }

    public void GuestEnterPool()
    {

    }
}
