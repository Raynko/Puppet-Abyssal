﻿using UnityEngine;

public class SpawnPlayer : MonoBehaviour
{
    private void Awake()
    {
        GameObject.FindGameObjectWithTag("Player").transform.position = transform.position;
        GameObject.FindGameObjectWithTag("MainCamera").transform.position = transform.position;
    }
}
