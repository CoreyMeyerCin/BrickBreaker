using UnityEngine;

public abstract class ManagerBase : MonoBehaviour
{
}

public abstract class Manager<T> : ManagerBase where T : Manager<T>
{
	private static T instance;

	public static T Instance
	{
		get
		{
			if (instance == null)
			{
				instance = FindObjectOfType<T>();
				if (instance == null)
				{
					Debug.LogError("An instance of " + typeof(T) +
					" is needed in the scene, but there is none.");
				}
			}
			return instance;
		}
	}

	protected virtual void Awake()
	{
		if (instance != null && instance != this)
		{
			Destroy(gameObject);
		}
		else
		{
			instance = this as T;
		}
	}
}