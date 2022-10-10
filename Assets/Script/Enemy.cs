using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UI;

public class Enemy : LivingEntity
{
    public Gun gun;
    public LayerMask whatIsTarget;

    private LivingEntity targetEntity;
    private NavMeshAgent pathFinder;

    public ParticleSystem hitEffect;
    public AudioClip deathSound;
    public AudioClip hitSound;

    private Animator enemyAnimator;
    private AudioSource enemyAudioPlayer;
    private Renderer enemyRenderer;

    public float damage = 20f;
    public float timeBetAttack = 0.5f;
    private float lastAttackTime;

    private BoxCollider BoxC;



    private bool hasTarget
    {
        get
        {
            if(targetEntity != null && !targetEntity.dead)
            {
                return true;
            }
            return false;
        }
    }

    private void Awake()
    {
        pathFinder = GetComponent<NavMeshAgent>();
        enemyAnimator = GetComponent<Animator>();
        enemyAudioPlayer = GetComponent<AudioSource>();
        enemyRenderer = GetComponentInChildren<Renderer>();
        BoxC = GetComponent<BoxCollider>();

    }

    public void Setup(float newHealth,float newDamage,float newSpeed,Color skinColor)
    {
        startingHealth = newHealth;
        health = newHealth;
        damage = newDamage;
        pathFinder.speed = newSpeed;
        enemyRenderer.material.color = skinColor;
    }


    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(UpdatePath());
    }
    void Update()
    {
        enemyAnimator.SetBool("HasTarget", hasTarget);
    }

    private IEnumerator UpdatePath()
    {
        while(!dead)
        {
            if(hasTarget)
            {
                pathFinder.isStopped = false;
                pathFinder.SetDestination(targetEntity.transform.position);
            }
            else
            {
                pathFinder.isStopped = true;
                Collider[] colliders = Physics.OverlapSphere(transform.position, 100f, whatIsTarget);
                for(int i =0;i<colliders.Length; i++)
                {
                    LivingEntity livingEntity = colliders[i].GetComponent<LivingEntity>();
                    if(livingEntity != null && !livingEntity.dead)
                    {
                        targetEntity = livingEntity;
                        break;
                    }
                }
            }
            yield return new WaitForSeconds(0.25f);
        }
    }

    public override void OnDamage(float damage, Vector3 hitPoint, Vector3 hitNormal)
    {
        if(!dead)
        {
            hitEffect.transform.position = hitPoint;
            hitEffect.transform.rotation = Quaternion.LookRotation(hitNormal);
            hitEffect.Play();

            enemyAudioPlayer.PlayOneShot(hitSound);
        }
        base.OnDamage(damage, hitPoint, hitNormal);

    }

    public override void Die()
    {
        
        gun.KillCount(); //킬스트릭1 증가
        Collider[] enemyColliders = GetComponents<Collider>();
        Rigidbody enemyRigid = GetComponent<Rigidbody>();
        gameObject.layer = 7;
        //for (int i = 0; i < enemyColliders.Length; i++)
        //{
        //    enemyColliders[i].enabled = false;
        //}
        pathFinder.isStopped = true;
        pathFinder.enabled = false;
        enemyAnimator.SetTrigger("Die");
        enemyAudioPlayer.PlayOneShot(deathSound);
        base.Die();
    }

    //private IEnumerator boong()
    //{
    //    Collider[] enemyColliders = GetComponents<Collider>();
    //    Rigidbody enemyRigid = GetComponent<Rigidbody>();
    //    yield return new WaitForSeconds(2f);
    //    for (int i = 0; i < enemyColliders.Length; i++)
    //    {
    //        enemyColliders[i].enabled = false;
    //    }
    //}

    private void OnTriggerStay(Collider other)
    {
        if (!dead && Time.time >= lastAttackTime + timeBetAttack)
        {
            LivingEntity attackTarget = other.GetComponent<LivingEntity>();
            if (attackTarget != null && attackTarget == targetEntity)
            {
                lastAttackTime = Time.time;
                Vector3 hitPoint = other.ClosestPoint(transform.position);
                Vector3 hitNormal = transform.position - other.transform.position;

                attackTarget.OnDamage(damage, hitPoint, hitNormal);
            }
        }
    }

    //private IEnumerator EnemyAtk()
    //{
    //    yield return new WaitForSeconds(0.25f);
    //    BoxC.enabled = true;
    //    LivingEntity attackTarget = GetComponent<LivingEntity>();
    //    yield return new WaitForSeconds(0.25f);
    //    BoxC.enabled = false;
    //}
    //private void OnTriggerEnter(Collider other)
    //{
    //    StartCoroutine(EnemyAtk());
       
    //}
    //private void OnTriggerExit(Collider other)
    //{
    //    StopCoroutine(EnemyAtk());
    //}


}
