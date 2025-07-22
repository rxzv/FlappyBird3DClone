using UnityEngine;

public class Data : MonoBehaviour
{
    public int BestScore = 0;    
    
    public static Data Instance { get; private set; }  

    void Awake()
    {
        if (Instance != null && Instance != this) {  
            Destroy(this);  
        }  
        else {  
            Instance = this;  
            DontDestroyOnLoad(this);
        }
    }
}
