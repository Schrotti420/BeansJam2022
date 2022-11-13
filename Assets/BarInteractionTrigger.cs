using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BarInteractionTrigger : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag.Equals("Bar"))
        {
            BarInteractionHandler.Instance.Show();
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.tag.Equals("Bar"))
        {
            BarInteractionHandler.Instance.Hide(.1f);
        }
    }
}
