using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(LineRenderer))]
public class RaycastGun : MonoBehaviour
{
    public float gunRange = 50f;
    public float fireRate = 0.2f;
    public float laserDuration = 10f;
    
    private Camera _mainCamera;
    private LineRenderer laserLine;
    private Transform laserOrigin;
    private float fireTimer;

    void Awake()
    {
        // get a reference to our main camera
        if (_mainCamera == null)
        {
            _mainCamera = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<Camera>();
        }
        laserLine = GetComponent<LineRenderer>();
        laserOrigin = GetComponent<Transform>();
    }

    void Update()
    {
        //fireTimer += Time.deltaTime;
        //if (Input.GetButtonDown("Fire1") && fireTimer > fireRate)
        //{
        //    fireTimer = 0;
        //    laserLine.SetPosition(0, laserOrigin.position);
        //    Vector3 rayOrigin = _mainCamera.ViewportToWorldPoint(new Vector3(0.5f, 0.5f, 0));
        //    RaycastHit hit;
        //    if (Physics.Raycast(rayOrigin, _mainCamera.transform.forward, out hit, gunRange))
        //    {
        //        laserLine.SetPosition(1, hit.point);
        //        //Destroy(hit.transform.gameObject);
        //    }
        //    else
        //    {
        //        laserLine.SetPosition(1, rayOrigin + (_mainCamera.transform.forward * gunRange));
        //    }
        //    StartCoroutine(ShootLaser());
        //}

        laserLine.SetPosition(0, laserOrigin.position);
        Vector3 rayOrigin = _mainCamera.ViewportToWorldPoint(new Vector3(0.5f, 0.5f, 0));
        RaycastHit hit;
        if (Physics.Raycast(rayOrigin, _mainCamera.transform.forward, out hit, gunRange))
        {
            laserLine.SetPosition(1, hit.point);
            Destroy(hit.transform.gameObject);
        }
        else
        {
            laserLine.SetPosition(1, rayOrigin + (_mainCamera.transform.forward * gunRange));
        }

    }

    IEnumerator ShootLaser()
    {
        laserLine.enabled = true;
        yield return new WaitForSeconds(laserDuration);
        laserLine.enabled = false;
    }
}