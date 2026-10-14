using UnityEngine;

public class BookButtonScript : MonoBehaviour
{
    public GameObject PrizeBook;
    public AudioSource bookbuttonSFX;
    public void OpenPrizeBook() 
    {
        PrizeBook.SetActive(true);
        bookbuttonSFX.Play();
    }

    public void ClosePrizeBook()
    {
        PrizeBook.SetActive(false);
        bookbuttonSFX.Play();
    }
}
