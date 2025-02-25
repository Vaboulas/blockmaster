using UnityEngine;

public class Fall : MonoBehaviour
{
    private bool isfall = false;

    public bool HasFallen()
    {
        return isfall;
    }

    public void OnTriggerEnter(Collider other)
    {
        isfall = true;

    }

}