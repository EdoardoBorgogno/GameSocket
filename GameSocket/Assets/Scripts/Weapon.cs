using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon : MonoBehaviour
{
    public Transform firePoint;
    public GameObject bulletPrefab;
    public bool canShot = true;
    [SerializeField] private AudioSource shot;

    // Update is called once per frame
    void Update()
    {
        if (Input.GetButtonDown("Fire1") && PlayerPrefs.GetString("color") == this.name)
        {
            Shoot();

        }
    }

    public void Shoot()
    {
        if (canShot)
        {
            shot.Play();
            if(PlayerPrefs.GetString("color") == this.name)
            SocketClient.Send("</SHOOT/>");
            Instantiate(bulletPrefab, firePoint.position, firePoint.rotation);

            canShot = false;

            StartCoroutine(ableToShot());
        }
    }

    IEnumerator ableToShot()
    {
        yield return new WaitForSeconds(0.7f);

        canShot = true;
    }
}