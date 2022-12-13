using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon : MonoBehaviour
{
    public Transform firePoint;
    public GameObject bulletPrefab;
    [SerializeField] private AudioSource shot;

    // Update is called once per frame
    void Update()
    {
        if (Input.GetButtonDown("Fire1") && PlayerPrefs.GetString("color") == this.name)
        {
            Shoot();
            SocketClient.Send("</SHOOT/>");
        }
    }

    public void Shoot()
    {
        shot.Play();
        // Logica di sparo.
        Debug.Log("SPARO DA SCRIPT (WEAPON.CS)");
        //SocketClient.Send("</SHOT/>");
        Instantiate(bulletPrefab, firePoint.position, firePoint.rotation);
    }
}
