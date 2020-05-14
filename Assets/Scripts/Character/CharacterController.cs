using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterController : MonoBehaviour
{

    [HideInInspector] public Vector3 characterForce;

    private void OnTriggerExit(Collider other)
    {
        if (other.GetComponent<ParticleTile>() != null)
        {
            ParticleTile tile = other.GetComponent<ParticleTile>();

            //tile.generator.ReOrganiseTiles();
            //Debug.Log("Has Reorganised");
        }

        //Debug.Log("Has Collided");
    }
}
