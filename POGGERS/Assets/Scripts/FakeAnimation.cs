using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FakeAnimation : MonoBehaviour {

	public Sprite LeftAttack1;
	public Sprite LeftAttack2;
	public Sprite RightAttack1;
	public Sprite RightAttack2;
	public Sprite CenterAttack1;
	public Sprite CenterAttack2;
	public Sprite LeftMove;
	public Sprite RightMove;
	public Sprite Block;
	public Sprite Idle;

	public GameObject straightslash;
	public GameObject rightslash;
	public GameObject leftslash;

	public AudioClip swing;
	public AudioClip move;
	public AudioClip block;

	private AudioSource src;
	private SpriteRenderer renderer;
	private PlayerController2 player;
	private string nextanim;

	private int timer;

	// Use this for initialization
	void Start () {
		renderer = GetComponent<SpriteRenderer> ();
		player = GetComponentInParent<PlayerController2> ();
		src = GetComponent<AudioSource> ();
	}
	
	// Update is called once per frame
	void Update () {
		if (player.getCurrentActionString () == "None") {
			ChangeSprite ();
			timer++;
		}
		if (timer > 60) {
			this.transform.localScale = new Vector3(4.0f, 4.0f, 1.0f);
			renderer.sprite = Idle;
			straightslash.SetActive(false);
			leftslash.SetActive(false);
			rightslash.SetActive(false);
			timer = 0;
			nextanim = "";
		}
		PrepSprite ();
	}

	void PrepSprite () {
		if (player.getCurrentActionString () == "Left Attack") {
			renderer.sprite = LeftAttack1;
			src.clip = swing;
			src.Play ();
			nextanim = "LeftAttack";
		}
		if (player.getCurrentActionString () == "Straight Attack") {
			renderer.sprite = CenterAttack1;
			src.clip = swing;
			src.Play ();
			nextanim = "StraightAttack";
		}
		if (player.getCurrentActionString () == "Right Attack") {
			this.transform.localScale = new Vector3(-4.0f, 4.0f, 1.0f);
			renderer.sprite = RightAttack1;
			src.clip = swing;
			src.Play ();
			nextanim = "RightAttack";
		}
		if (player.getCurrentActionString () == "Move Left") {
			renderer.sprite = LeftMove;
			src.clip = move;
			src.Play ();
			nextanim = "LeftMove";
		}
		if (player.getCurrentActionString () == "Move Right") {
			this.transform.localScale = new Vector3(-4.0f, 4.0f, 1.0f);
			renderer.sprite = RightMove;
			src.clip = move;
			src.Play ();
			nextanim = "RightMove";
		}
		if (player.getCurrentActionString () == "Block") {
			nextanim = "Block";
			renderer.sprite = Block;
			src.clip = block;
			src.Play ();
		}
	}

	void ChangeSprite () {
		if (nextanim == "LeftAttack") {
			renderer.sprite = LeftAttack2;
			leftslash.SetActive(true);
		}
		if (nextanim == "StraightAttack") {
			renderer.sprite = CenterAttack2;
			straightslash.SetActive(true);
		}
		if (nextanim == "RightAttack") {
			renderer.sprite = RightAttack2;
			rightslash.SetActive(true);
		}
		if (nextanim == "LeftMove") {
			renderer.sprite = LeftMove;
		}
		if (nextanim == "RightMove") {
			renderer.sprite = RightMove;
		}
		if (nextanim  == "Block") {
			renderer.sprite = Block;
		}
	}
}
