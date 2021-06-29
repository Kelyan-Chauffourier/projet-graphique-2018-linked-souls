using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class WeakEnnemyController : NetworkBehaviour {
    [SyncVar]
    public int health;

	public int speed = 1;
	[SyncVar]
	private Vector3 nextPoint;

	private Animator animator;
	private Collider2D hitbox; 

	// Use this for initialization
	void Start () {
		animator = this.GetComponent<Animator>();
        setNextPoint();
		hitbox = this.GetComponent<Collider2D>();
	}
	
	// Update is called once per frame
	void Update () {
		transform.position = Vector3.Lerp(transform.position, nextPoint,
			(speed / Vector2.Distance(transform.position, nextPoint)) * Time.fixedDeltaTime);
		if (this.transform.position == nextPoint)
		{
			setNextPoint();
		}

	}

	void setNextPoint()
	{
		int direction = Random.Range(0, 3);
		Vector3 directionVector;
			switch (direction)
		{
			case 0 :
				directionVector = Vector2.up;
				break;
			case 1 :
				directionVector = Vector2.down;
				break;
			case 2 :
				directionVector = Vector2.left;
				break;
			default:
				directionVector = Vector2.right;
				break;
		}

		Collider2D collider = Physics2D.OverlapCircle(gameObject.transform.position + directionVector, 1, 0, -1, 1 );
		if (collider == null)
		{
			nextPoint.x = this.transform.position.x + ( directionVector.x);
			nextPoint.y = this.transform.position.y + ( directionVector.y);
			nextPoint.z = this.transform.position.z;
		}
		else
		{
			setNextPoint();
		}
	}

	public void OnTriggerEnter2D(Collider2D other)
	{
		if(other.tag == "sword")
		{
			animator.SetTrigger("dead");
			hitbox.gameObject.SetActive(false);
			Destroy(this);
		}
	}
}
