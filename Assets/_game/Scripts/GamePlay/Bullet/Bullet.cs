using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Lean.Pool;

public class Bullet : MonoBehaviour
{
    [SerializeField] private float rotateSpeed = 400f;

    private float speed = 10f;

    private float indexTime = 1f;
    private Character attacker;
    private Vector3 direction;
    private Action<Character, Bullet> onHitTarget;

    public Character Attacker { get => attacker; }

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
        Invoke(nameof(ReturnToPool), indexTime);
        onHitTarget = callBack;
        this.transform.rotation = Quaternion.Euler(new Vector3(-90, 0, 0));
        this.attacker = attacker;
        this.direction = direction;
        this.direction = this.direction.normalized;
        this.gameObject.SetActive(true);
    }

    public void ReturnToPool()
    {
        // return bullet to pool
        //LeanPool.Despawn(this.gameObject);
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
                    Player player = Attacker as Player;
                    if (player != null)
                    {
                        GameManager.Instance.Index++;
                        Debug.Log(GameManager.Instance.Index);
                        DataManager.Instance.CoinData ++;
                        GameManager.Instance.CoinText.text = DataManager.Instance.CoinData.ToString();
                        LevelManager.Instance.CameraPlus();
                    }

                    ((Bot)character).DelayDead();
                    LeanPool.Despawn(this.gameObject);
                    Attacker.Kill++;
                }

                if (other.CompareTag(CacheString.PLAYER_TAG))
                {
                    ((Player)character).OnDeath();
                    LeanPool.Despawn(this.gameObject);
                    UIManager.Instance.OnReviveUI();
                    Attacker.Kill++;
                }

                Attacker.gameObject.transform.localScale = (1f + Mathf.Log10(Attacker.Kill + 1f)) * Vector3.one;
                //Attacker.Bullet.transform.localScale += new Vector3(Attacker.Bullet.transform.localScale.x * 0.1f, Attacker.Bullet.transform.localScale.y * 0.1f, Attacker.Bullet.transform.localScale.z * 0.1f);
                indexTime = 1f + Mathf.Log10(Attacker.Kill + 1f);
                speed = 1f + Mathf.Log10(Attacker.Kill + 1f) * 10f;
                OnHitTarget(character, this);
            }
        }
    }

    public void OnHitTarget(Character victim, Bullet bullet)
    {
        onHitTarget?.Invoke(victim, bullet);
    }

}
