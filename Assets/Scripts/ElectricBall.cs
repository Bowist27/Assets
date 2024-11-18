using UnityEngine;
using TMPro;

public class ElectricBalls : MonoBehaviour
{
    public string objectToDestroy;

    public string ElectricBallTag;

    private GroupBalls groupBalls ;

    // Metodo para asignar referencia a GroupBalls
    public void SetGroupBalls(GroupBalls group_balls)
    {
        groupBalls = group_balls;
    }

      void OnBecameInvisible()
    {
        Destroy(gameObject); 
    }

    void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag(objectToDestroy))
        {
            // Destruye el TieFighter
            Destroy(gameObject);
        }

        if (collision.gameObject.CompareTag(ElectricBallTag))
        {
            Destroy(collision.gameObject);
            Time.timeScale = 0f;
        }
    }
    
    void OnDestroy()
    {
        groupBalls?.TieFighterDestroyed();
    }

}
