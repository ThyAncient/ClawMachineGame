using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using static PrizeDetectorScript;

public class PrizesDescription
{
    public static string kelvintitetext = "You Got Horny kelvin! he is flaunting his peepee at you";
    public static string bossykelvintext = "You got BossyKelvin! he wants some lablab now!!";
    public static string kelvinuncommonplushietext = "You got Kelvin Plushie! He became a plushie??";
    public static string SittingKelvin = "You got SittingKelvin! hes just sitting.";
    public static string happykelvin = "You got HappyKelvin! Hes happy because of you!!";
    public static string valokelvin = "You got ValoKelvin! hes been a valo player for 5 years!!";
    public static string pajamakelvin = "You got PajamaKelvin! He has pajamas!! Wait.. why does he have a sword?!?!";
    public static string chunlikelvin = "You got ChunLiKelvin! Chun-WHAT";
}

public class PrizeDetectorScript : MonoBehaviour
{
    private List<SpriteRenderer> prizesInside = new List<SpriteRenderer>();

    public Image baseprize;
    public Image baseBookPrize1;
    public Image baseBookPrize2;
    public Image baseBookPrize3;
    public Image baseBookPrize4;
    public Image baseBookPrize5;
    public Image baseBookPrize6;
    public Image baseBookPrize7;
    public Image baseBookPrize8;
    public TextMeshProUGUI baseprizetext;
    public bool hasGottenPrize = false;

    public SpriteRenderer currentPrize;
    private Sprite lastPrizeSprite;
    public ParticleSystem prizeParticle;

    public PrizeType prizeType;
    public enum PrizeType
    {
        TiTeKelvin,
        BossyKelvin,
        KelvinUncommonPlushie,
        SittingKelvin,
        HappyKelvin,
        ValoKelvin,
        PajamaKelvin,
        ChunLiKelvin
    }


    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (!collision.CompareTag("Prize")) return;

        SpriteRenderer sr = collision.GetComponent<SpriteRenderer>();
        if (sr == null) return;

        if (!prizesInside.Contains(sr))
            prizesInside.Add(sr);
        
        UpdateCurrentPrize();
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (!collision.CompareTag("Prize")) return;

        SpriteRenderer sr = collision.GetComponent<SpriteRenderer>();
        if (sr == null) return;

        prizesInside.Remove(sr);

        UpdateCurrentPrize();
    }
    void UpdateCurrentPrize()
    {
        if (prizesInside.Count > 0)
        {
            currentPrize = prizesInside[0];
            hasGottenPrize = true;

            lastPrizeSprite = currentPrize.sprite;
            baseprize.sprite = lastPrizeSprite;

            CheckPrize(currentPrize);
            CheckPrizeID(currentPrize);
        }
        else
        {
            currentPrize = null;
            hasGottenPrize = false;
        }
    }
    public void ClaimPrize()
    {
        if (currentPrize == null) return;

        // SAVE sprite BEFORE destroy
        lastPrizeSprite = currentPrize.sprite;
        baseprize.sprite = lastPrizeSprite;

        // particles
        ParticleSystem ps = Instantiate(prizeParticle, currentPrize.transform.position, Quaternion.identity);
        Destroy(ps.gameObject, 2f);

        prizesInside.Remove(currentPrize);
        Destroy(currentPrize.gameObject);

        prizesInside.Remove(currentPrize); // IMPORTANT if using list system

        currentPrize = null;

        UpdateCurrentPrize(); // refresh remaining prizes
    }

    public void CheckPrize(SpriteRenderer prize)
    {
        Prize prizeData = prize.GetComponent<Prize>();
        if (prizeData == null) return;

        if (prizeData.prizeType == PrizeType.TiTeKelvin)
        {
            
            baseprizetext.text = PrizesDescription.kelvintitetext;
        }
        else if (prizeData.prizeType == PrizeType.BossyKelvin)
        {
            baseprizetext.text = PrizesDescription.bossykelvintext;
        }
        else if (prizeData.prizeType == PrizeType.KelvinUncommonPlushie)
        {
            baseprizetext.text = PrizesDescription.kelvinuncommonplushietext;
        }
        else if (prizeData.prizeType == PrizeType.SittingKelvin)
        {
            baseprizetext.text = PrizesDescription.SittingKelvin;
        }
        else if (prizeData.prizeType == PrizeType.HappyKelvin)
        {
            baseprizetext.text = PrizesDescription.happykelvin;
        }
        else if (prizeData.prizeType == PrizeType.ValoKelvin)
        {
            baseprizetext.text = PrizesDescription.valokelvin;
        }
        else if (prizeData.prizeType == PrizeType.PajamaKelvin)
        {
            baseprizetext.text = PrizesDescription.pajamakelvin;
        }
        else if (prizeData.prizeType == PrizeType.ChunLiKelvin)
        {
            baseprizetext.text = PrizesDescription.chunlikelvin;
        }
    }

    public void CheckPrizeID(SpriteRenderer prize)
    {
        Prize prizeData = prize.GetComponent<Prize>();
        if (prizeData == null) return;

        if (prizeData == null)
        {
            Debug.LogError("NO Prize script attached to: " + prize.name);
            return;
        }

        if (prizeData.prizeType == PrizeType.TiTeKelvin)
        {
            baseBookPrize1.color = Color.white;
            Debug.Log("ITWORKSSSSSSSSSSSSSSSSSS");
        }
        else if (prizeData.prizeType == PrizeType.BossyKelvin)
        {
            baseBookPrize2.color = Color.white;
        }
        else if (prizeData.prizeType == PrizeType.KelvinUncommonPlushie)
        {
            baseBookPrize3.color = Color.white;
        }
        else if (prizeData.prizeType == PrizeType.SittingKelvin)
        {
            baseBookPrize4.color = Color.white;
        }
        else if (prizeData.prizeType == PrizeType.HappyKelvin)
        {
            baseBookPrize5.color = Color.white;
        }
        else if (prizeData.prizeType == PrizeType.ValoKelvin)
        {
            baseBookPrize6.color = Color.white;
        }
        else if (prizeData.prizeType == PrizeType.PajamaKelvin)
        {
            baseBookPrize7.color = Color.white;
        }
        else if (prizeData.prizeType == PrizeType.ChunLiKelvin)
        {
            baseBookPrize8.color = Color.white;
        }
    }
}