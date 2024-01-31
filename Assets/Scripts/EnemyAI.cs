using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAI : MonoBehaviour
{

    private Transform playerTransfrom;
    public float speed = 20f;
    private float duration = 1;
    public float time = 10f;

    // Start is called before the first frame update
    void Start()
    {
        playerTransfrom = GameObject.FindWithTag("Player").transform;
    }

    // Update is called once per frame
    void Update()
    {
       


    }
    
}
