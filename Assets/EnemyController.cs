using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        GetComponent<Character>().OnCharacterDeath.AddListener(EnemyDeath);
    }

    private void EnemyDeath()
    {
        GetComponent<Animator>().SetBool("Death", true);
        Destroy(gameObject, 5);
    }
}
