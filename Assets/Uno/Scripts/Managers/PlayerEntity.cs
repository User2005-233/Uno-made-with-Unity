using UnityEngine;

public class PlayerEntity : MonoBehaviour

{
    int code;
    Camera mainCamera;
    public bool isSelf = false;

    // Start is called before the first frame update
    void Start()
    {
        mainCamera = transform.Find("Main Camera").GetComponent<Camera>();
    }
    
    public void MakeNotSelf()
    {
        mainCamera.enabled = false;
        isSelf = false;
    }
    
    public void MakeMeSelf()
    {
        mainCamera.enabled = true;
        isSelf = true;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
