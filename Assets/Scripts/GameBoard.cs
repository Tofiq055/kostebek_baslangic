using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameBoard : MonoBehaviour
{
    [Header("Köstebebler")]
    public List<MoleController> moles; // Sahnedeki 9 köstebek

    [Header("Oyun Döngüsü")]
    public float spawnInterval = 2.0f; // Kaç saniyede bir köstebek çıksın?

    private float timer = 0f;

    void Start()
    {
        // Eğer listeyi elle doldurmayı unutursan, otomatik bul
        if (moles == null || moles.Count == 0)
        {
            moles = new List<MoleController>();
            // Alt objelerdeki (Slotların içindeki) tüm MoleController'ları bul
            GetComponentsInChildren<MoleController>(true, moles);
        }
    }

    void Update()
    {
        timer += Time.deltaTime;

        if (timer >= spawnInterval)
        {
            SpawnRandomMole();
            timer = 0f;
        }
    }

    void SpawnRandomMole()
    {
        // Rastgele bir index seç (0 ile 8 arası)
        int randomIndex = Random.Range(0, moles.Count);
        
        // O köstebeğe "Çık!" emri ver
        if (moles[randomIndex] != null)
        {
            moles[randomIndex].Rise();
        }
    }
}
