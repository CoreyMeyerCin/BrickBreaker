using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EventManager : MonoBehaviour
{
	public static EventManager Instance { get; private set; }

	public delegate void BlockDestroyedHandler();
    public event BlockDestroyedHandler OnBlockDestroyed;

    public void TriggerBlockDestroyed()
    {
        OnBlockDestroyed?.Invoke();
    }
	void Awake()
	{
		if (Instance == null)
		{
			Instance = this;
			DontDestroyOnLoad(gameObject);
		}
		else
		{
			Destroy(gameObject);
		}
	}


}
