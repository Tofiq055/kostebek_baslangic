using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameBoard : MonoBehaviour
{
    [Header("Oyun Ayarları")]
    // Köstebeklerin ne sıklıkla çıkacağını belirler (Saniye cinsinden).
    public float spawnInterval = 2.0f; 

    // Sahnedeki tüm mole scriptlerini burada tutacağız.
    // Inspector'da elle atamazsanız Start fonksiyonu otomatik bulur.
    public List<MoleController> moles; 

    private float timer = 0f;

    void Start()
    {
        // Eğer liste boşsa, çocuk objelerdeki MoleController'ları bulup ekle
        if (moles == null || moles.Count == 0)
        {
            moles = new List<MoleController>();
            // True parametresi, pasif olan objeleri de dahil eder
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
