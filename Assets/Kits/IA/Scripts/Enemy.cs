using UnityEngine;
using UnityEngine.AI;

public class Enemy : MonoBehaviour
{
    [SerializeField] Transform target;
    [SerializeField] Transform patrolPointsParent;
    [SerializeField] float reachDistance = 2f;


    NavMeshAgent agent;
    Sight sight;
    int currentPatrolPoint = 0;

    private void Awake()
    {
        agent = GetComponent<NavMeshAgent>();
        sight = GetComponent<Sight>();
    }

    // Update is called once per frame
    void Update()
    {
        target = sight.GetPlayerInSight();

        if (target != null) {
        agent.SetDestination(target.position);
    }
        else {
            //Patrol
            Vector3 nextPosition = patrolPointsParent.GetChild(currentPatrolPoint).position;
            agent.SetDestination(nextPosition);
            if (Vector3.Distance(nextPosition, transform.position) < reachDistance)
            {
                currentPatrolPoint++;
                if (currentPatrolPoint >= patrolPointsParent.childCount)
                {
                    currentPatrolPoint = 0;
                }
            }
        }
    }
}
