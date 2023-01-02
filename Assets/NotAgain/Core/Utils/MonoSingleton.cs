using UnityEngine;

namespace NotAgain.Utils
{
	public abstract class MonoSingleton<T> : MonoBehaviour where T : MonoSingleton<T>
	{
		static T _instance = null;

		public static T Instance
		{
			get
			{
				if (_instance == null)
				{
					_instance = FindObjectOfType(typeof(T)) as T;

					if (_instance == null)
					{
						Debug.LogError($"No instance of {typeof(T)}");
						return null;
					}

					if (!_isInitialized)
					{
						_instance.Init();
					}
				}

				return _instance;
			}
		}

		static bool _isInitialized;

		void Awake()
		{
			if (_instance == null)
			{
				_instance = this as T;
			}
			else if (_instance != this)
			{
				Debug.LogError("Another instance of " + GetType() + " is already exist! Destroying self...");
				DestroyImmediate(this);
				return;
			}

			if (!_isInitialized)
			{
				DontDestroyOnLoad(gameObject);
				_instance.Init();
			}
		}
		
		protected virtual void Init()
		{
			_isInitialized = true;
		}

		void OnApplicationQuit()
		{
			_instance = null;
		}
	}
}