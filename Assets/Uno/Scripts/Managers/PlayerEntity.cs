using System.Collections;

using System.Collections.Generic;

using TMPro;

using UnityEngine;

public class PlayerEntity : MonoBehaviour

{
    Camera mainCamera;

    public GameObject lastHoveredObj = null;

    public Card lastHoveredCard = null;

    Ray ray;

    RaycastHit hit;

    bool isHost = false;

    // Start is called before the first frame update
    void Start()
    {
        mainCamera = transform.Find("Main Camera").GetComponent<Camera>();
        if(!isHost) mainCamera.enabled = false;
    }

    // Update is called once per frame
    void Update()
    {
        ray = mainCamera.ScreenPointToRay(Input.mousePosition);

        if (Physics.Raycast(ray, out hit))

        {
            GameObject hitObj = hit.collider.gameObject;

            if (hitObj.CompareTag("Card"))
            {
                if (lastHoveredObj != hitObj)
                {
                    ResetLastHovered(hitObj);
                }
            }

            else
            {
                ResetLastHovered();
            }
        }
        else
        {
            ResetLastHovered();
        }

        if (Input.GetKeyUp(KeyCode.Space))
        {
            UIManager.Instance.SwitchPanel<MainMenu>();
        }
    }

    void ResetLastHovered(GameObject obj=null)
    {
        if (obj == null)

        {
            if(lastHoveredObj != null&&lastHoveredCard!=null)

            {
                lastHoveredCard.ColorReset();

                lastHoveredObj=null;

                lastHoveredCard=null;
            }
        }

        else

        {
            if (lastHoveredObj != null && lastHoveredCard != null)

            {
                lastHoveredCard.ColorReset();
            }

            lastHoveredObj = obj;

            lastHoveredCard = lastHoveredObj.GetComponent<Card>();

        }
    }
}
