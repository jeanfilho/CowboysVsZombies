using UnityEngine;
using System.Collections;

public class SimpleNavMeshAgent : MonoBehaviour
{
    bool active = false;

    public Transform target;
    NavMeshAgent agent;


    // Use this for initialization
    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
    }

    // Update is called once per frame
    void Update()
    {
        agent.SetDestination(target.position);
    }

    public void setTarget(GameObject targetPlayer)
    {
        target = targetPlayer.transform;
    }

    public void setActive(bool value)
    {
        active = value;
    }

    public void initialize(GameObject targetPlayer)
    {
        setTarget(targetPlayer);
        setActive(true);
    }

}
