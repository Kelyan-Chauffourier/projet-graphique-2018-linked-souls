using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel.Design;
using UnityEngine;

public class Spawnzone : MonoBehaviour
{
	//public SpawnZoneMaster master;
	public Spawnzone Up;
	public Spawnzone Down;
	public Spawnzone Left;
	public Spawnzone Right;
	private int PlayerCount ;
	private bool active = false;

	private void OnTriggerEnter2D(Collider2D other)
	{
		if (other.CompareTag("Player"))
		{
			Debug.Log("Changement zone joueur");
			PlayerCount++;
			if (PlayerCount ==1)
			{
				//master.AddActiveZone(this);
			}
			UpdateAdjacent();
		}
	}

	private void OnTriggerExit2D(Collider2D other)
	{
		if (other.CompareTag("Player"))
		{
			PlayerCount--;
			if (PlayerCount == 0)
			{
				//master.RemoveActiveZone(this);
			}
			UpdateAdjacent();
		}	
	}

	public void UpdateAdjacent()
	{
		//master.RefreshActive();
		Up.RefreshState();
		Up.Right.RefreshState();
		Right.RefreshState();
		Down.Right.RefreshState();
		Down.RefreshState();
		Down.Left.RefreshState();
		Left.RefreshState();
		Up.Left.RefreshState();
		RefreshState();
	}

	public void RefreshState()
	{
		bool state = false;
		state |= PlayerCount > 0;
		state |= (Up.PlayerCount>0);
		state |= (Left.PlayerCount > 0);
		state |= (Right.PlayerCount > 0);
		state |= (Down.PlayerCount > 0);
		state |= (Up.Left.PlayerCount > 0);
		state |= (Up.Right.PlayerCount > 0);
		state |= (Down.Left.PlayerCount > 0);
		state |= (Down.Right.PlayerCount > 0);
		setState(state);
	}

	public void setActive()
	{
		active = true;
		Debug.Log("setting active");
		List<EnemySpawner> spawners = new List<EnemySpawner>();
		gameObject.GetComponents(spawners);
		for (int i = 0; i < spawners.Count; i++)
		{
			spawners[i].Spawn();
		}
	}

	public void setInactive()
	{
		active = false;
		List<EnemySpawner> spawners = new List<EnemySpawner>();
		gameObject.GetComponents(spawners);
		for (int i = 0; i < spawners.Count; i++)
		{
			spawners[i].DeSpawn();
		}
	}

	public void setState(bool state)
	{
		if (state == true)
		{
			if (active == false)
			{
				setActive();
			}
		}
		else
		{
			if (active == true)
			{
				setInactive();
			}
			
		}
	}
}
