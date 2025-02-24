using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon : MonoBehaviour
{
    public int minDamage;
    public int maxDamage;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Fire(Ray rayToFire)
    {
        if(Physics.Raycast(rayToFire, out RaycastHit raycastHit))
        {
            Character characterHit = raycastHit.collider.gameObject.GetComponentInParent<Character>();
            if(characterHit != null)
            {
                int damage = -GetDamage();
                characterHit.RPC_ChangeHp(damage);
                characterHit.ChangeHpLocally(damage);
            }
        }
    }

    public int GetDamage()
    {
        return UnityEngine.Random.Range(minDamage, maxDamage+1);
    }
}
