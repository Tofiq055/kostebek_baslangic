using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using TMPro;

public class GameBoard : MonoBehaviour
{
    [Header("Oyun Ayarları")]
    // Köstebeklerin ne sıklıkla çıkacağını belirler (Saniye cinsinden).
    public float spawnInterval = 2.0f; 

    [Header("Skor")]
    public TMP_Text scoreText; // Ekranda skoru yazacak yazı
    private int score = 0;

    // Sahnedeki tüm mole scriptlerini burada tutacağız.
    // Inspector'da elle atamazsanız Start fonksiyonu otomatik bulur.
    public List<MoleController> moles; 

    public void AddScore(int amount)
    {
        score += amount;
        if(scoreText != null)
        {
            scoreText.text = "Skor: " + score;
        }
    }

    private float timer = 0f;

    void Start()
    {
        // Önce listedeki boş (silinmiş) referansları temizle
        if (moles != null)
        {
            moles.RemoveAll(item => item == null);
        }

        // Eğer liste boşsa veya temizlendikten sonra boşaldıysa, yeniden bul
        if (moles == null || moles.Count == 0)
        {
            moles = new List<MoleController>();
            // Alt objelerdeki (Slotların içindeki) tüm MoleController'ları bul
            GetComponentsInChildren<MoleController>(true, moles);
        }
    }

    void Update()
    {
        // Moles listesi boşsa hata vermemesi için kontrol
        if (moles == null || moles.Count == 0) return;

        timer += Time.deltaTime;

        if (timer >= spawnInterval)
        {
            SpawnRandomMole();
            timer = 0f;
        }
    }

    void SpawnRandomMole()
    {
        // Rastgele bir köstebek seç
        int randomIndex = Random.Range(0, moles.Count);
        MoleController selectedMole = moles[randomIndex];

        // Seçilen köstebek varsa ve null değilse yukarı çıkar
        if (selectedMole != null)
        {
            selectedMole.Rise();
        }
    }
}
