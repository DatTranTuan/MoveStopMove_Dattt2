using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    [SerializeField] private float speed;
    [SerializeField] private float rotateSpeed = 400f;

    private Character attacker;
    private Vector3 direction;
    private Action<Character> onHitTarget;

    public static Stack<Bullet> stack = new Stack<Bullet>();

    public Character Attacker { get => attacker;}

    private void Update()
    {
        transform.Rotate(Vector3.back, rotateSpeed * Time.deltaTime);
        if (Attacker is not null)
        {
            transform.position += direction.normalized * speed * Time.deltaTime;
        }
    }

    public void Init(Vector3 direction, Character attacker, Action<Character> callBack)
    {
        Invoke(nameof(ReturnToPool), 1f);
        onHitTarget = callBack;
        this.attacker = attacker;
        this.direction = direction;
        this.direction = this.direction.normalized;
        this.gameObject.SetActive(true);
    }

    public void ReturnToPool()
    {
        // return bullet to pool
        stack.Push(this);
        // set attacker = null to make Update stop 
        attacker = null;
        this.gameObject.SetActive(false);
    }

    public void OnHitTarget(Character target)
    {
        onHitTarget?.Invoke(target);
    } 

    //private void OnEnable()
    //{
    //    transform.GetComponent<Rigidbody>().WakeUp();
    //    Invoke("HideBullet", 2.0f);
    //}

    //private void OnDisable()
    //{
    //    transform.GetComponent<Rigidbody>().Sleep();
    //    CancelInvoke();
    //}

    //private void HideBullet()
    //{
    //    gameObject.SetActive(false);
    //}

   
}
