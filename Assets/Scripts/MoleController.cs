using System.Collections;
using UnityEngine;

public class MoleController : MonoBehaviour
{
    // Inspector Ayarları için Başlıklar
    [Header("Hareket Ayarları")]
    // Köstebeğin gizli (aşağıda) olduğu Y pozisyonu. Kendi sahnenize göre ayarlayın.
    public float hiddenY = -0.2f; 
    // Köstebeğin görünür (yukarıda) olduğu Y pozisyonu.
    public float visibleY = 0.2f; 
    // Köstebeğin yukarı çıkış ve iniş hızı.
    public float speed = 4f; 
    // Köstebeğin yukarıda kalma süresi (saniye).
    public float waitDuration = 1.0f; 

    // Köstebeğin şu an yukarıda ve vurulabilir olup olmadığını diğer scriptler buradan kontrol eder.
    public bool isActive = false; 

    private Vector3 targetPosition;
    private Transform moleTransform;
    private Coroutine movementCoroutine;

    void Awake()
    {
        moleTransform = this.transform;
        // Başlangıçta köstebek gizli konumda olmalı
        targetPosition = new Vector3(moleTransform.localPosition.x, hiddenY, moleTransform.localPosition.z);
        moleTransform.localPosition = targetPosition;
    }

    void Update()
    {
        // Hedef pozisyona doğru yumuşak hareket
        moleTransform.localPosition = Vector3.MoveTowards(
            moleTransform.localPosition, 
            targetPosition, 
            speed * Time.deltaTime
        );
    }

    // GameBoard bu fonksiyonu çağırarak köstebeği yukarı çıkarır
    public void Rise()
    {
        if (isActive) return; // Zaten aktifse tekrar tetikleme

        if (movementCoroutine != null) StopCoroutine(movementCoroutine);
        movementCoroutine = StartCoroutine(RiseRoutine());
    }

    private IEnumerator RiseRoutine()
    {
        isActive = true;

        // 1. Yukarı çık
        targetPosition = new Vector3(moleTransform.localPosition.x, visibleY, moleTransform.localPosition.z);
        
        // Yukarı pozisyona ulaşana kadar bekle (yaklaşık süre) veya sabit süre bekle
        yield return new WaitForSeconds(waitDuration);

        // 2. Süre dolunca aşağı in (Eğer vurulmadıysa)
        if (isActive)
        {
            Hide();
        }
    }

    // HandInputManager tarafından çağrılır
    public void OnHit()
    {
        if (!isActive) return;

        // Vurulma işlemlerini burada yapabiliriz (ses, efekt vb.)
        Hide();
    }

    private void Hide()
    {
        isActive = false;
        // Aşağı in
        targetPosition = new Vector3(moleTransform.localPosition.x, hiddenY, moleTransform.localPosition.z);
        
        if (movementCoroutine != null) StopCoroutine(movementCoroutine);
    }
}
