﻿using UnityEngine;
using System.Collections;

public class Attack3State : MonoBehaviour {
private Animator anim;
	// Use this for initialization
	void Start () {
		anim = GetComponent<Animator>();
	}
	
	// Update is called once per frame
	void Update () {
		anim.Play("BossAttack3");
	}
}
