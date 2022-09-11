using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Health : MonoBehaviour
{
    public static event Action OnEnemyDead;
    public static event Action<int, int> OnPlayerhealthChanged;
    [SerializeField] private AudioSource EnemyAudioSource;
    [SerializeField] private AudioClip explosionSound;
    

    [SerializeField] private int initialHealth;
    [SerializeField] private int currentHealth;
    [SerializeField] private bool isPlayer;

    private CameraShake cameraShake;

    private void Awake()
    {
        EnemyAudioSource = GetComponent<AudioSource>();
        if (isPlayer)
            currentHealth = initialHealth;

        cameraShake = Camera.main.GetComponent<CameraShake>();
        //cameraShake = FindObjectOfType<CameraShake>();
    }

    // private void OnTriggerEnter(Collider other)
    // {
    //     Debug.Log(other.name);
    //     if(other.TryGetComponent<Damager>(out Damager damager))
    //     {
    //         LoseHealth(damager.GetDamage());

    //         damager.HitSomething();
    //     }

    //     /*
    //     Damager dmgr = other.GetComponent<Damager>();

    //     if (dmgr != null)
    //         dmgr.HitSomething();

    //     */
    // }

    private void OnTriggerEnter2D(Collider2D other)
    {
        Debug.Log("trigger " + other.gameObject.name);

        if (other.TryGetComponent<Damager>(out Damager damager))
        {
            LoseHealth(damager.GetDamage());

            damager.HitSomething();

            if (isPlayer && cameraShake != null)
                cameraShake.Shake();
        }

        /*
        Damager dmgr = other.GetComponent<Damager>();

        if (dmgr != null)
            dmgr.HitSomething();

        */
    }

    private void LoseHealth(int damage)
    {
        currentHealth -= damage;

        if (isPlayer)
            OnPlayerhealthChanged?.Invoke(initialHealth, currentHealth);

        CheckIfDead();
    }

    private void CheckIfDead()
    {
        if (currentHealth <= 0)
        {
            if (!isPlayer)
            {
                EnemyAudioSource.PlayOneShot(explosionSound);
                OnEnemyDead?.Invoke();
            
            }
            else
            {
                GameManager.Instance.GameOver();
            }
           
            Destroy(gameObject);
        }
    }

    public void SetInitialHealth(int health)
    {
        currentHealth = health;
    }
}
