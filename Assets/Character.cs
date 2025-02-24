using Fusion;
using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Events;

public class Character : NetworkBehaviour
{

    public int hpMax;
    public UnityEvent OnCharacterDeath;

    [Networked, OnChangedRender(nameof(OnHpChanged))]
    public int Hp { get; set; }


    public override void Spawned()
    {
        base.Spawned();
        OnHpChanged();
    }

    public void OnHpChanged() 
    {
        GetComponentInChildren<TextMeshPro>().text = Hp.ToString() + "/" + hpMax.ToString();
    }

    [Rpc(sources:RpcSources.All , targets: RpcTargets.StateAuthority)]
    public void RPC_ChangeHp(int hpDelta) 
    {
        Hp = Mathf.Clamp(Hp+hpDelta,0,hpMax);
        if (Hp == 0)
        {
            CharacterDeath();
        }
    }

    internal void ChangeHpLocally(int damage)
    {
        GetComponentInChildren<TextMeshPro>().text = (Hp-damage).ToString() + "/" + hpMax.ToString();
    }

    private void CharacterDeath()
    {
        OnCharacterDeath.Invoke();
    }

    public bool IsDead()
    {
        return Hp == 0;
    }

    public void UseWeapon(Ray rayToFire)
    {
        GetEquipedWeapon().Fire(rayToFire);
    }

    public Weapon GetEquipedWeapon() 
    {
        return GetComponentInChildren<Weapon>();
    }
}
