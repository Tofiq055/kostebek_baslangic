using System.Collections;
using UnityEngine;

public class MoleController : MonoBehaviour
{
    // Köstebeğin ne kadar yukarı çıkacağı (Senin mole boyutuna göre 0.08 iyi olabilir)
    // Başlangıç (Gizli) Y: -0.03
    // Hedef (Görünür) Y: 0.05 gibi düşünebiliriz.
    // Bu değerleri editörden elle gireceğiz.
    
    [Header("Ayarlar")]
    public float hiddenY = -0.03f; // Yuvadaki Y konumu
    public float visibleY = 0.05f; // Yukarıdaki Y konumu
    public float speed = 3f;       // Çıkış/İniş hızı
    public float waitDuration = 1.5f; // Yukarıda bekleme süresi

    private Vector3 targetPosition;
    private bool isActive = false; // Şu an yukarıda mı?

    // Köstebeğin kendi Transform'u
    private Transform moleTransform;

    void Awake()
    {
        moleTransform = this.transform;
        // Başlangıçta hedefimiz gizli konum
        targetPosition = new Vector3(moleTransform.localPosition.x, hiddenY, moleTransform.localPosition.z);
        moleTransform.localPosition = targetPosition; // Hemen gizle
    }

    void Update()
    {
        // Hedefe doğru yumuşakça (Lerp ile) git
        // 3D Distance kontrolü yerine basit MoveTowards kullanıyoruz, işlemci dostu.
        moleTransform.localPosition = Vector3.MoveTowards(
            moleTransform.localPosition, 
            targetPosition, 
            speed * Time.deltaTime
        );
    }

    // Bu fonksiyonu GameBoard çağıracak
    public void Rise()
    {
        if (isActive) return; // Zaten yukarıdaysa işlem yapma

        StartCoroutine(RiseRoutine());
    }

    private IEnumerator RiseRoutine()
    {
        isActive = true;
        
        // 1. Hedefi YUKARI yap
        targetPosition = new Vector3(moleTransform.localPosition.x, visibleY, moleTransform.localPosition.z);

        // 2. Yukarıda bekle
        yield return new WaitForSeconds(waitDuration);

        // 3. Hedefi AŞAĞI (Gizli) yap
        targetPosition = new Vector3(moleTransform.localPosition.x, hiddenY, moleTransform.localPosition.z);
        
        // Biraz bekle ki tam insin (Animasyon süresi kadar)
        yield return new WaitForSeconds(0.5f);
        
        isActive = false; // Artık yeni emre hazır
    }
    
    // Vurulunca çağıracağız (Faz 4 için hazırlık)
    public void OnHit()
    {
        StopAllCoroutines(); // Beklemeyi iptal et
        isActive = false;
        // Hemen aşağı indir
        targetPosition = new Vector3(moleTransform.localPosition.x, hiddenY, moleTransform.localPosition.z);
    }
}
