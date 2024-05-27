using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PeopleGenerator : MonoBehaviour
{
    [SerializeField] private List<GameObject> peopleList = new List<GameObject>();
    [SerializeField] private Queue<GameObject> peopleQueue = new Queue<GameObject>();
    [SerializeField] private GameObject destination;
    [SerializeField] private float generatorCool;
    public bool isGenerator;

    private void Start()
    {
        if(isGenerator)
        {
            for (int i = 0; i < peopleList.Count; i++)
            {
                GameObject pp = Instantiate(peopleList[i], transform);
                pp.transform.position = transform.position;
                peopleQueue.Enqueue(pp);
                pp.gameObject.SetActive(false);
            }
            GuestExitPool();
            StartCoroutine(GeneratorCo());
            isGenerator = false;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.GetComponent<GuestController>() != null)
        {
            GuestController Enterpp = other.GetComponent<GuestController>();
            Enterpp.ChangeState(GuestState.Idle);
            Enterpp.agent.ResetPath();
            Enterpp.transform.parent = transform;
            Enterpp.transform.position = transform.position;
            Enterpp.gameObject.SetActive(false);
            peopleQueue.Enqueue(Enterpp.gameObject);
            
        }
    }

    private void Update()
    {
        if(peopleQueue.Count >= 7)
        {
            isGenerator = true;
        }

        if(isGenerator)
        {
            GuestExitPool();
            StartCoroutine(GeneratorCo());
            isGenerator = false;
        }
    }

    private void GuestExitPool()
    {
        GameObject exitpp = peopleQueue.Dequeue();
        exitpp.gameObject.SetActive(true);
        exitpp.GetComponent<GuestController>().destination = destination;
        exitpp.GetComponent<GuestController>().ChangeState(GuestState.Walk);
    }

    private IEnumerator GeneratorCo()
    {
        for (int i = 0; i < 6; i++)
        {
            yield return new WaitForSeconds(generatorCool);
            GuestExitPool();
        }
    }
}
