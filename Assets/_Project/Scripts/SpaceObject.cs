using UnityEngine;

public abstract class SpaceObject : MonoBehaviour
{
    [SerializeField] private float Health;

    public void TakeDamage(float value)
    {
        if (value < Health)
        {
            Health -= value;
        }
        else
        {
            OnDie();   
        }
    }

    protected virtual void OnDie()
    {
        
    }
}
