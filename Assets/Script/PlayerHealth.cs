using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class PlayerHealth : LivingEntity
{
    //living 안에 Idamage 포함
    public Slider healthSlider;

    public AudioClip deathClip;
    public AudioClip hitClip;
    public AudioClip itemPickupClip;

    private AudioSource playerAudioPlayer;
    private Animator playerAnimator;

    private PlayerMove playermovement;
    private PlayerShoot playershooter;

    private void Awake()
    {
        playerAnimator = GetComponent<Animator>();
        playerAudioPlayer = GetComponent<AudioSource>();

        playermovement = GetComponent<PlayerMove>();
        playershooter = GetComponent<PlayerShoot>();
    }

    protected override void OnEnable()
    {
        base.OnEnable();
        healthSlider.gameObject.SetActive(true);
        healthSlider.maxValue = startingHealth;
        healthSlider.value = health;

        playermovement.enabled = true;
        playershooter.enabled = true;

    }
    public override void RestoreHealth(float newHealth)
    {
        base.RestoreHealth(newHealth);
        healthSlider.value = health;
    }

    public override void OnDamage(float damage, Vector3 hitPoint, Vector3 hitNormal)
    {
        if(!dead)
        {
            playerAudioPlayer.PlayOneShot(hitClip);
        }
        base.OnDamage(damage, hitPoint, hitNormal);
        healthSlider.value = health;

    }

    public override void Die()
    {
        base.Die();
        healthSlider.gameObject.SetActive(false);
        playerAudioPlayer.PlayOneShot(deathClip);
        playerAnimator.SetTrigger("Die");
        playermovement.enabled = false;
        playershooter.enabled = false;

    }

    private void OnTriggerEnter(Collider other)
    {
        if(!dead)
        {
            Iitem item = other.GetComponent<Iitem>();
            if(item != null)
            {
                item.Use(gameObject);
                playerAudioPlayer.PlayOneShot(itemPickupClip);
            }
        }
    }
}
