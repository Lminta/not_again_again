using UnityEngine;
namespace NotAgain.Utils
{
    public class ServiceLocator 
    {
        struct Container<TObject> where TObject : class
        {
            public static TObject Instance;
        }
        
        public static void Set<TObject>(TObject instance) where TObject : class 
        {
            if (Container<TObject>.Instance != null)
            {
                Debug.LogError($"Service already set. '{typeof(TObject).FullName}'");
                return;
            }

            Container<TObject>.Instance = instance;
        }
        
        public static TObject Get<TObject>() where TObject : class 
        {
            if (Container<TObject>.Instance == null)
                Debug.LogError($"Service not set. '{typeof(TObject).FullName}'");
            
            return Container<TObject>.Instance;
        }

        public static void Release<TObject>() where TObject : class
        {
            Container<TObject>.Instance = null;
        }
    }
}
