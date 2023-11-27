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
    private Action<Character, Bullet> onHitTarget;

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

    public void Init(Vector3 direction, Character attacker, Action<Character, Bullet> callBack)
    {
        Invoke(nameof(ReturnToPool), 1f);
        onHitTarget = callBack;
        this.transform.rotation = Quaternion.Euler(new Vector3 (-90,0,0));
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

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag(CacheString.PLAYER_TAG) || other.CompareTag(CacheString.BOT_TAG))
        {
            Character character = other.GetComponent<Character>();
            if (this.Attacker != character)
            {
                //bullet.OnHitTarget(this, bullet);
                this.gameObject.SetActive(false);
                if (other.CompareTag(CacheString.BOT_TAG))
                {
                    ((Bot)character).DelayDead();
                }

                if (other.CompareTag(CacheString.PLAYER_TAG))
                {
                    ((Player)character).OnDeath();
                }

                Attacker.gameObject.transform.localScale += new Vector3(Attacker.transform.localScale.x * 0.04f, Attacker.transform.localScale.y * 0.04f, Attacker.transform.localScale.z * 0.04f);
                OnHitTarget(character, this);
            }
        }
    }

    public void OnHitTarget(Character victim,Bullet bullet)
    {
        onHitTarget?.Invoke(victim, bullet);
    } 

}
