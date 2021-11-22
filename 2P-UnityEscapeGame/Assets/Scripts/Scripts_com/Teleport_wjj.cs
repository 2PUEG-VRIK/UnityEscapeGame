using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Teleport_wjj : MonoBehaviour
{
    public GameObject target;
    bool cooldown = false;

    private void OnTriggerEnter(Collider other)
    {
        if (!(this.GetComponent<Activator>()?.isOn ?? true)) // Activator�� ������ ���� �������� �ƴϸ� true => ���
        {
            return;
        }
        if (cooldown)
        {
            return;
        }
        if (target != null)
        {
            if (other.gameObject.tag == "Player")
            {
                StartCooldown();
                Vector3 position = target.transform.position;
                position.y += target.transform.lossyScale.y / 2.0f;
                other.transform.position = position;
            }

            if (other.gameObject.tag != "Player")
            {
                StartCooldown();
                Vector3 position = target.transform.position;
                position.y += target.transform.lossyScale.y / 2.0f;
                other.transform.position = position;
            }
        }
    }

    private void StartCooldown()
    {
        if (target.GetComponent<Teleport_wjj>() != null)
        {
            target.GetComponent<Teleport_wjj>().cooldown = true;
            this.StartCoroutine("EndCooldown");
        }
    }

    public IEnumerator EndCooldown()
    {
        yield return new WaitForSeconds(1.0f);
        target.GetComponent<Teleport_wjj>().cooldown = false;
        StopCoroutine("toggleCooldown");
    }
}
