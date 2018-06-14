using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameSFX : MonoBehaviour {

	public AudioClip playerhurt;
	public AudioClip enemyhurt;

	private AudioSource src;
	// Use this for initialization
	void Start () {
		src = GetComponent<AudioSource> ();
	}
	
	public void PlayerHPSound() {
		src.clip = playerhurt;
		src.Play ();
	}
	public void EnemyHPSound() {
		src.clip = enemyhurt;
		src.Play ();
	}
}
