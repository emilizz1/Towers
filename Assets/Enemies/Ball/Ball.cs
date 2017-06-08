using System;
using UnityEngine;

[RequireComponent(typeof(UnityEngine.AI.NavMeshAgent))]
public class Ball : MonoBehaviour
{
    [SerializeField] private float m_MovePower = 5; 

    public UnityEngine.AI.NavMeshAgent agent { get; private set; }
    public Transform target;
    
    private Rigidbody m_Rigidbody;


    private void Start()
    {
        m_Rigidbody = GetComponent<Rigidbody>();

        agent = GetComponentInChildren<UnityEngine.AI.NavMeshAgent>();

        agent.updateRotation = false;
        agent.updatePosition = true;
    }

    private void Update()
    {
        if (target != null)
            agent.SetDestination(target.position);

        if (agent.remainingDistance > agent.stoppingDistance)
            Move(agent.desiredVelocity);
        else
            Move(Vector3.zero);
    }

    public void SetTarget(Transform target)
    {
        this.target = target;
    }

    public void Move(Vector3 moveDirection)
    {
            m_Rigidbody.AddForce(moveDirection * m_MovePower);
    }
}
