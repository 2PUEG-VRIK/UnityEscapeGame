using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class mouse_box : MonoBehaviour
{
    private Vector3 m_Offset;
    private float m_ZCoord;

    void OnMouseDown()
    {
        m_ZCoord = Camera.main.WorldToScreenPoint(gameObject.transform.position).z;
        m_Offset = gameObject.transform.position - GetMouseWorldPosition();

    }
    Vector3 GetMouseWorldPosition()
    {
        Vector3 mousePoint = Input.mousePosition;
        mousePoint.z = m_ZCoord;

        return Camera.main.ScreenToWorldPoint(mousePoint);
    }

    private void OnMouseUp()
    {
        gameObject.SetActive(false);
    }
    private void OnMouseEnter()
    {
    }

    void OnMouseDrag()
    {
        transform.position = GetMouseWorldPosition() + m_Offset;
    }
}
