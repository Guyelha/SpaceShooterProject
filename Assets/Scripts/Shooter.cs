using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shooter : MonoBehaviour
{
    [Header("General")]
    //[SerializeField] private GameObject projectilePrefab;
    [SerializeField] private string projectileTag;
    [SerializeField] private float projectileSpeed;
    [SerializeField] private float projectileLifeTime;
    [SerializeField] private float timeBetweenShots;
    [SerializeField] private float minTimeTexweenShots;
    [SerializeField] private float maxTimeTexweenShots;
    [SerializeField] private AudioSource audioSource;
    [SerializeField] private AudioClip shootingClip;


    [Space(10)]
    [Header("for enemies only")]
    [SerializeField] private bool isEnemy = false;

    [HideInInspector] public bool isShooting = false;

    Coroutine fireCoroutine;

    private void Start()
    {
        if (isEnemy)
            isShooting = true;
    }

    private void Update()
    {
        Fire();
    }

    private void Fire()
    {

        if (isShooting == true && fireCoroutine == null)
        {
            audioSource.PlayOneShot(shootingClip);  
            fireCoroutine = StartCoroutine(ContinuousFire());
        }
        else if (isShooting == false && fireCoroutine != null)
        {
            StopCoroutine(fireCoroutine);
            fireCoroutine = null;
        }
    }

    private IEnumerator ContinuousFire()
    {

        while (true)
        {
            /*
            GameObject newProjectile = Instantiate(
                projectilePrefab,
                transform.position,
                Quaternion.identity);
            */
            audioSource.PlayOneShot(shootingClip);

            GameObject newProjectile = ObjectsPool.Instance.SpawnFromPool(
                projectileTag,
                transform.position,
                Quaternion.identity
                );

            Rigidbody2D rb = newProjectile.GetComponent<Rigidbody2D>();
            rb.velocity = projectileSpeed * Vector2.up;

            //Destroy(newProjectile, projectileLifeTime);

            ObjectsPool.Instance.DespawnToPool(newProjectile, projectileLifeTime);

            float timeToWait;

            if (!isEnemy) // is player
            {
                timeToWait = timeBetweenShots;
            }
            else // is enemy
            {
                timeToWait = Random.Range(minTimeTexweenShots, maxTimeTexweenShots);
            }


            yield return new WaitForSeconds(timeToWait);

        }
    }
}
