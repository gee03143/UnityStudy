using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DontDestroyOnLoad : MonoBehaviour
{
    public static DontDestroyOnLoad obj;

    // Start is called before the first frame update
    void Start()
    {
        DontDestroyOnLoad(this);
        if (obj == null)
        {
            obj = GetComponent<DontDestroyOnLoad>();
        }
        else
        {
            Destroy(gameObject);
        }
    }
}
