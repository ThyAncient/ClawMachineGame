using UnityEngine;

public class ClaimPrizeScript : MonoBehaviour
{
    public GameObject prizeAnimationObject;
    public void exitPrizeAnimation() 
    {
        prizeAnimationObject.SetActive(false);
    }
}
