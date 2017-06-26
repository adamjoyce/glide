using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ImpactZone : MonoBehaviour
{
    public PlayerCharacter player;

    /* Use for initialisation. */
    private void Start()
    {
        // Attempt to get a reference to the player if not is set.
        if (!player)
        {
            player = GameObject.Find("Player").GetComponent<PlayerCharacter>();
        }
    }

    /* What happens when something collides with this object. */
    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Meteor")
        {
            player.TakeDamage(50.0f);
            other.GetComponent<Meteor>().TakeDamage(100.0f);
            Debug.Log(player.GetHealth());
        }
    }
}