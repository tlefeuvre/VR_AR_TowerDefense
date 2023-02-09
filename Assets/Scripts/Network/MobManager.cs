using System.Collections;
using System.Collections.Generic;
using Unity.Netcode;
using UnityEngine;
using UnityEngine.AI;

public class MobManager : NetworkBehaviour
{
    bool isAttacking = false;
    float speed;

    private void Start()
    {
        speed = GetComponent<NavMeshAgent>().speed;
    }

    public void attackHarrow(Harrow harrow)
    {
        if (!OnClientModif())
            return;
        if (!isAttacking)
        {
            isAttacking = true;
            GetComponent<NavMeshAgent>().speed = 0;
            GetComponent<Animator>().SetBool("IsAttacking", true);
            StartCoroutine(attack(harrow));
        }
    }

    public void stopAttack(Harrow harrow)
    {
        if (!OnClientModif())
            return;

        if (!harrow)
        {
            isAttacking = false;
            GetComponent<NavMeshAgent>().speed = speed;
            GetComponent<Animator>().SetBool("IsAttacking", false);
        }
    }

    IEnumerator attack(Harrow harrow)
    {
        while (isAttacking && harrow)
        {
            if (!harrow.takeDamage(1))
            {
                Debug.LogWarning(harrow.GetComponent<Harrow>().Health);
                stopAttack(harrow);
                if (harrow)
                    Destroy(harrow.gameObject);
            }
            yield return new WaitForSeconds(1);
        }
        stopAttack(harrow);
    }

    private bool OnClientModif()
    {
        if (IsHost && IsOwner)
            return true;
        else
            return false;
    }
}
