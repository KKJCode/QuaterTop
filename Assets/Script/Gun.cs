using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.Rendering;
using UnityEngine.UI;

public class Gun : MonoBehaviour
{
    public enum State
    {
        Ready,
        Empty,
        Reloading
    }

    public State state { get; private set; }
    public Transform fireTransform;
    public ParticleSystem muzzleEffect;
    public ParticleSystem shellEject;
    private LineRenderer BulletLine;
    private AudioSource gunAudioPlayer;
    public AudioClip shotClip;
    public AudioClip reloadClip;
    public Animator animatorrrr;
    private PlayerInput playerInput;
    

    public float damage = 25;
    private float fireDistance = 50f;

    public int ammoRemain = 100;
    public int magCapacity = 25;
    public int magAmmo;

    public float timeBetFire = 0.12f;
    public float reloadTime = 1.8f;
    private float lastFireTime;
    public GameObject[] tembox;

    public GameObject grenade;
    public int grenadeCount;
    public AudioClip clipG;
    public ParticleSystem ThrowG;


    public int Killstreak = 0;

    Image Gimage;
    Image G2image;

    private void Awake()
    {
        gunAudioPlayer = GetComponent<AudioSource>();
        BulletLine = GetComponent<LineRenderer>();
        animatorrrr = GetComponentInParent<Animator>();
        BulletLine.positionCount = 2;
        BulletLine.enabled = false;
        Gimage = GameObject.Find("MobileSingleStickControl").transform.Find("Grenade3").GetComponent<Image>();
        G2image = GameObject.Find("MobileSingleStickControl").transform.Find("Grenade3").transform.Find("Bomb").GetComponent<Image>();
        Gimage.enabled = false;
        G2image.enabled = false;
        GameObject.Find("MobileSingleStickControl").transform.Find("Grenade3").GetComponent<Image>().enabled = false;

    }

    private void OnEnable()
    {
        magAmmo = magCapacity;
        state = State.Ready;
        lastFireTime = 0;
    }
    private void Update()
    {
        UpdateUI();
        Gcount();
    }

    public void Fire()
    {
        if(state == State.Ready&&Time.time >= lastFireTime+timeBetFire)
        {
            lastFireTime = Time.time;
            Shot();
            animatorrrr.Play("Recoil", -1, 0);
            

        }
    }

    public void Shot()
    {
        RaycastHit hit;
        Vector3 hitPosition = Vector3.zero;

        if (Physics.Raycast(fireTransform.position, fireTransform.forward, out hit, fireDistance))
        {
            IDamageable target = hit.collider.GetComponent<IDamageable>();
            if(target != null)
            {
                Debug.Log("EnemyHit!");
                target.OnDamage(damage, hit.point, hit.normal);
            }
            hitPosition = hit.point;

            if (hit.collider.CompareTag("ItemBox"))
            {
                
                Instantiate(tembox[0], hit.point, Quaternion.identity);
                Destroy(hit.transform.gameObject);
                Instantiate(tembox[1], hit.point, Quaternion.Euler(new Vector3(90f, 0, 0)));
            }
            else if (hit.collider.CompareTag("ItemBox1"))
            {
                Instantiate(tembox[2], hit.point, Quaternion.identity);
                Destroy(hit.transform.gameObject);
                Instantiate(tembox[1], hit.point, Quaternion.Euler(new Vector3(90f, 0, 0)));
            }
            else if (hit.collider.CompareTag("ItemBox2"))
            {
                Instantiate(tembox[3], hit.point, Quaternion.identity);
                Destroy(hit.transform.gameObject);
                Instantiate(tembox[1], hit.point, Quaternion.Euler(new Vector3(90f, 0, 0)));
            }
        }
        else
        {
            hitPosition = fireTransform.position + fireTransform.forward * fireDistance;
        }

        StartCoroutine(ShotEffect(hitPosition));
        magAmmo--;
        if(magAmmo <=0)
        {
            state = State.Empty;
        }
    }

    private IEnumerator ShotEffect(Vector3 hitPosition)
    {
        muzzleEffect.Play();
        shellEject.Play();
        gunAudioPlayer.PlayOneShot(shotClip);

        BulletLine.SetPosition(0, fireTransform.position);
        BulletLine.SetPosition(1, hitPosition);

        BulletLine.enabled = true;
        yield return new WaitForSeconds(0.03f);
        BulletLine.enabled = false;
    }
    public bool Reload()
    {
        if (state == State.Reloading || ammoRemain <= 0 || magAmmo >= magCapacity)
        {
            return false;
        }
        StartCoroutine(ReloadRoutine());
        return true;
    }

    public void KillCount()
    {
        Killstreak++;
        Debug.Log("KILL++");

        if(Killstreak >=5 )
        {
            grenadeCount++;
            Killstreak = 0;

        }
    }
    public void Gcount()
    {
        if(grenadeCount > 0)
        {
            Gimage.enabled = true;
            G2image.enabled = true;
        }
        else if (grenadeCount <= 0)
        {
            Gimage.enabled = false;
            G2image.enabled = false;
        }
    }

    //private void OnTriggerEnter(Collider other)
    //{
    //    GameObject hitEffect = Instantiate(granades, transform.position, transform.rotation);
    //    Destroy(hitEffect, 3f);
    //    Collider[] collider = Physics.OverlapSphere(transform.position, 10f, layermask);
    //    for(int i =0; i<collider.Length; i++)
    //    {
    //        Vector3 ground = new Vector3(transform.position.x, transform.position.y, transform.position.z);
    //        ground.y -=2f;
    //        Rigidbody enemyRid = collider[i].GetComponent<Rigidbody>();
    //        enemyRid.AddExplosionForce(500, ground, 10);
    //        if (collider[i].GetComponent<Enemy>())
    //            collider[i].GetComponent<Enemy>().OnDamage(100,transform.position,transform.position);


    //    }
    //}

    public void Granade()
    {
        Debug.Log("BOOM");
        ThrowG.Play();

        GameObject far = Instantiate(grenade, fireTransform.transform.position, Quaternion.identity);
        far.GetComponent<Rigidbody>().AddForce(fireTransform.forward * 10,ForceMode.Impulse);            
        GetComponent<AudioSource>().PlayOneShot(clipG);
        grenadeCount--;
    }



    private IEnumerator ReloadRoutine()
    {
        state = State.Reloading;
        gunAudioPlayer.PlayOneShot(reloadClip);
        yield return new WaitForSeconds(reloadTime);
        int ammoTofill = magCapacity - magAmmo;
        if(ammoRemain < ammoTofill)
        {
            ammoTofill = ammoRemain;
        }
        magAmmo += ammoTofill;
        ammoRemain -= ammoTofill;
        state = State.Ready;
    }


    private void UpdateUI()
    {
        UIManager.instance.UpdateKillText(Killstreak);
    }


}
