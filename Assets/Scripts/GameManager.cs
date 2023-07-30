using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    GameObject _salesDesk;
    public GameObject updatePanel;
    public GameObject joysitck;
    public bool updateBtnCliced;


    public void UpdteSalesDeskBtn()
    {
        EventManager.UpdteSalesDeskBtn();
    }
    public void UpdateBtn()
    {
        updateBtnCliced= true;
        updatePanel.SetActive(true);        
        Time.timeScale = 0f;
        joysitck.SetActive(false);
    }
    public void Cancel()
    {
        updateBtnCliced= false;
        updatePanel.SetActive(false);
        Time.timeScale = 1f;
        joysitck.SetActive(true);
        _salesDesk.GetComponent<SalesDeskManager>().NotSelected();
        _salesDesk=null;
    }

    private void Update()
    {
        ClickedObjectControl();
    }

    void ClickedObjectControl()
    {
        if (Input.GetMouseButtonDown(0) && updateBtnCliced)
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;
            if (Physics.Raycast(ray, out hit))
            {
                if (hit.collider.gameObject.GetComponent<SalesDeskManager>())
                {
                    _salesDesk = hit.collider.gameObject;
                    _salesDesk.GetComponent<SalesDeskManager>().Selected();
                }
            }
        }
    }
}
