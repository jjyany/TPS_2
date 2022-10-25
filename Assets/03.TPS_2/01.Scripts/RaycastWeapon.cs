using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RaycastWeapon : MonoBehaviour
{
    class Bullet
    {
        public float time;
        public Vector3 initialPosition;
        public Vector3 initialVelocity;
        public TrailRenderer trailRenderer;
        public int bounce;
    }

    public bool isFiring = false;
    public int fireRate = 25;
    public float bulletSpeed = 1000.0f;
    public float bulletDrop = 0.0f;

    public ParticleSystem muzzlePose;
    public ParticleSystem hitEffect;
    public TrailRenderer bulletTracer;
    public Transform raycastOrigin;
    public Transform raycastDestination;
    public AnimationClip weaponAnimation;
    public string weaponName;

    private float accumulatedTime = 0.0f;
    private float maxLifeTime = 3.0f;
    private Ray ray;
    private RaycastHit hit;
    private List<Bullet> bullets = new List<Bullet>();
    private Vector3 GetPosition(Bullet bullet)
    {
        Vector3 gravity = Vector3.down * bulletDrop;
        return (bullet.initialPosition) + (bullet.initialVelocity * bullet.time) + (0.5f * gravity * bullet.time * bullet.time);
    }
    private Bullet CreateBullet(Vector3 position, Vector3 velocity)
    {
        Bullet bullet = new Bullet();
        bullet.initialPosition = position;
        bullet.initialVelocity = velocity;
        bullet.time = 0.0f;
        bullet.trailRenderer = Instantiate(bulletTracer, ray.origin, Quaternion.identity);
        bullet.trailRenderer.AddPosition(position);
        return bullet;
    }
    public void UpdateFireing(float deltaTime)
    {
        accumulatedTime += deltaTime;
        float fireInterval = 1.0f / fireRate;
        while (accumulatedTime >= 0.0f)
        {
            FireBullet();
            accumulatedTime -= fireInterval;
        }
    }

    public void UpdateBullet(float deltaTime)
    {
        SimulateBullets(deltaTime);
        DestroyBullets();

    }

    private void SimulateBullets(float deltaTime)
    {
        bullets.ForEach(bullet =>
        {
            Vector3 p0 = GetPosition(bullet);
            bullet.time += deltaTime;
            Vector3 p1 = GetPosition(bullet);
            RaycastSegment(p0, p1, bullet);
        });
    }

    private void DestroyBullets()
    {
        bullets.RemoveAll(bullet => bullet.time >= maxLifeTime);
    }

    private void RaycastSegment(Vector3 start, Vector3 end, Bullet bullet)
    {
        Vector3 direction = end - start;
        float distance = direction.magnitude;
        ray.origin = start;
        ray.direction = direction;
        if (Physics.Raycast(ray, out hit, distance))
        {
            hitEffect.transform.position = hit.point;
            hitEffect.transform.forward = hit.normal;
            hitEffect.Emit(1);

            bullet.trailRenderer.transform.position = hit.point;
            bullet.time = maxLifeTime;
        }
        else
        {
            bullet.trailRenderer.transform.position = end;
        }
    }

    public void StartFiring()
    {
        isFiring = true;
        FireBullet();
    }

    private void FireBullet()
    {
        muzzlePose.Play();

        Vector3 velocity = (raycastDestination.position - raycastOrigin.position).normalized * bulletSpeed;
        var bullet = CreateBullet(raycastOrigin.position, velocity);
        bullets.Add(bullet);
        //ray.origin = raycastOrigin.position;
        //ray.direction = raycastDestination.position - raycastOrigin.position;

        //var tracer = Instantiate(bulletTracer, ray.origin, Quaternion.identity);
        //tracer.AddPosition(ray.origin);


    }

    public void StopFiring()
    {
        isFiring = false;
    }
}
