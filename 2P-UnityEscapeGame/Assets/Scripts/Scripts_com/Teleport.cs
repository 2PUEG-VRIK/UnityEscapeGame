using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Teleport : MonoBehaviour
{
    public enum TargetTypes { Player, Things }; // you can replace this with your own labels for the types of collectibles in your game!

    public GameObject target;
    public TargetTypes TargetType; // this gameObject's type

    bool cooldown = false;

    private void OnTriggerEnter(Collider other)
    {
        if (!(this.GetComponent<Activator>()?.isOn ?? true)) // Activator가 있으면 값을 가져오고 아니면 true => 통과
        {
            return;
        }
        if (cooldown)
        {
            return;
        }
        if (target != null)
        {
            if (TargetType==TargetTypes.Player &&  other.gameObject.tag == "Player")
            {
                StartCooldown();
                Vector3 position = target.transform.position;
                position.y += target.transform.lossyScale.y / 2.0f;
                other.transform.position = position;
            }

            if (TargetType == TargetTypes.Things && other.gameObject.tag == "Boxsj")
            {
                StartCooldown();
                Vector3 next_pos = target.transform.position;
                next_pos.y = other.gameObject.transform.position.y; 
                other.transform.position = next_pos;
            }
        }
    }

    private void StartCooldown()
    {
        if (target.GetComponent<Teleport>() != null)
        {
            target.GetComponent<Teleport>().cooldown = true;
            this.StartCoroutine("EndCooldown");
        }
    }

    public IEnumerator EndCooldown()
    {
        yield return new WaitForSeconds(1.0f);
        target.GetComponent<Teleport>().cooldown = false;
        StopCoroutine("EndCooldown");
    }
}
