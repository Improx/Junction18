using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Robber : MonoBehaviour
{

	public float FlashlightRadiance = 0;

	public bool Detained;

	public static List<Robber> All = new List<Robber>();

	private SpriteRenderer _spriteRenderer;
	private Sprite _defaultSprite;
	[SerializeField]
	private Sprite _detainedSprite;

	// Use this for initialization
	void Awake()
	{
		All.Add(this);
	}

	private void Start()
	{
		//if (!GetComponent<Player>().isLocalPlayer) GetComponentInChildren<SpriteRenderer>().enabled = false;
		_spriteRenderer = GetComponent<PlayerMove>().PlayerModel.GetComponent<SpriteRenderer>();
		_defaultSprite = _spriteRenderer.sprite;
	}

	private void OnDestroy()
	{
		All.Remove(this);
	}

	public void Detain()
	{
		if (Detained) return;

		Detained = true;
		var mover = GetComponent<PlayerMove>();
		mover.enabled = false;
		var rigidBody = GetComponent<Rigidbody2D>();

		rigidBody.constraints = RigidbodyConstraints2D.FreezeAll;

		DetainScreen.Instance.Display();
	}

	public void ShowDetainedSprite()
	{
		_spriteRenderer.sprite = _detainedSprite;
	}

	public void ShowNormalSprite()
	{
		_spriteRenderer.sprite = _defaultSprite;
	}
}