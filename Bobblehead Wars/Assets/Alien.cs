using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.Events;

public class Alien : MonoBehaviour
{

    public Transform target;
    private NavMeshAgent agent;

    public Animator arenaAnimator;

    public float navigationUpdate;
    private float navigationTime = 0;
public Rigidbody head;
    public bool isAlive = true;

    public UnityEvent OnDestroy;

    public int HeadPop = 0;


    

    // Start is called before the first frame update
    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
    }

    // Update is called once per frame
    void Update()
    {



if (isAlive)
            { 

        if (target != null)
        {
                                navigationTime += Time.deltaTime;
             if (navigationTime > navigationUpdate)
            {
                agent.destination = target.position;
                navigationTime = 0;
            }
        }
            }
        



    }


    void OnTriggerEnter(Collider other)
    {



        if (isAlive)
        {
            Die();
            head.GetComponent<SelfDestruct>().Initiate();
            SoundManager.Instance.PlayOneShot(SoundManager.Instance.alienDeath);
        }
    }

    public void Die()
    {
        isAlive = false;
        head.GetComponent<Animator>().enabled = false;
        head.isKinematic = false;
        head.useGravity = true;
        head.GetComponent<SphereCollider>().enabled = true;
        head.gameObject.transform.parent = null;
        head.velocity = new Vector3(0, 26.0f, 3.0f);

        HeadPop += 1;

        if (HeadPop == 50)
        {
            Invoke("endGame", 2.0f);
        }



        OnDestroy.Invoke();
        OnDestroy.RemoveAllListeners();
        SoundManager.Instance.PlayOneShot(SoundManager.Instance.alienDeath);

       
      

        Destroy(gameObject);
    }

    private void endGame()
    {
        SoundManager.Instance.PlayOneShot(SoundManager.Instance.
        elevatorArrived);
        arenaAnimator.SetTrigger("PlayerWon");
    }



}
