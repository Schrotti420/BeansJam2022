using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class GuardAnimator : MonoBehaviour
{
    private NavMeshAgent navMeshAgent;
    private AnimationInstancing.AnimationInstancing animationInstancing;

    private Vector3 lastPosition;
    // Start is called before the first frame update
    void Start()
    {
        navMeshAgent = this.GetComponent<NavMeshAgent>();
        animationInstancing = this.GetComponentInChildren<AnimationInstancing.AnimationInstancing>();

        lastPosition = transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        float delta = Vector3.Distance(transform.position, lastPosition);
        animationInstancing.playSpeed = delta / Time.deltaTime;
        lastPosition = transform.position;
    }
}
