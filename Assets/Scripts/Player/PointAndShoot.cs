using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PointAndShoot : MonoBehaviour
{
    [SerializeField] Camera playerCamera;
    [SerializeField] Transform rotationPoint;
    [SerializeField] Transform bulletStart;
    [SerializeField] GameObject waterPrefab;
    [SerializeField] GameObject lightningPrefab;
    [SerializeField] float bulletSpeed;
    [SerializeField] float bulletCooldown;
    Vector3 mousePosition;
    bool isShootReady = true;

    

    private void Update()
    {
        mousePosition = playerCamera.ScreenToWorldPoint(Input.mousePosition);
        Vector2 aimDirection = mousePosition - transform.position;
        float aimAngle = Mathf.Atan2(aimDirection.y,aimDirection.x) * Mathf.Rad2Deg;
        rotationPoint.rotation = Quaternion.Euler(0, 0, aimAngle);

        if (Input.GetMouseButton(0) && isShootReady)
        {
            SpawnBullet(waterPrefab);
        }
        if (Input.GetMouseButton(1) && isShootReady) 
        {
            SpawnBullet(lightningPrefab);
        }
    }

    void SpawnBullet(GameObject bulletPrefab)
    {
        var bullet = Instantiate(bulletPrefab, bulletStart.position, rotationPoint.rotation);
        bullet.GetComponent<Rigidbody2D>().AddForce(bulletStart.right * bulletSpeed, ForceMode2D.Impulse);
        StartCoroutine(ShootDelay());
    }

    IEnumerator ShootDelay()
    {
        isShootReady = false;
        Debug.Log("Shoot");
        yield return new WaitForSeconds(bulletCooldown);
        Debug.Log("Ready to shoot");
        isShootReady = true;
    }
}
