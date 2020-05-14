using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager instance = null;

    public GameSettings gameSettings;

    public CharacterController characterInstance; // loaded through editor

    private void Awake()
    {
        if(instance == null)
        {
            instance = this.gameObject.GetComponent<GameManager>();
        } else
        {
            Destroy(this);
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
