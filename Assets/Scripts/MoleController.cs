using System.Collections;
using UnityEngine;

public class MoleController : MonoBehaviour
{
    [Header("Hareket Ayarları")]
    public float hiddenY = -0.2f; 
    public float visibleY = 0.2f; 
    public float speed = 4f; 
    public float waitDuration = 2.0f; 

    public bool isActive = false; 

    private Vector3 targetPosition;
    private Transform moleTransform;
    private Coroutine movementCoroutine;

    // Görsel Efektler için
    private Renderer moleRenderer;
    private Color originalColor;
    
    void Awake()
    {
        moleTransform = this.transform;
        
        // Renderer'ı al ve orijinal rengi kaydet
        moleRenderer = GetComponent<Renderer>();
        if (moleRenderer != null)
        {
            originalColor = moleRenderer.material.color;
        }

        targetPosition = new Vector3(moleTransform.localPosition.x, hiddenY, moleTransform.localPosition.z);
        moleTransform.localPosition = targetPosition;
    }

    void Update()
    {
        moleTransform.localPosition = Vector3.MoveTowards(
            moleTransform.localPosition, 
            targetPosition, 
            speed * Time.deltaTime
        );
    }

    public void Rise()
    {
        if (isActive) return; 

        if (movementCoroutine != null) StopCoroutine(movementCoroutine);
        movementCoroutine = StartCoroutine(RiseRoutine());
    }

    private IEnumerator RiseRoutine()
    {
        isActive = true;
        
        // Rengi orijinale döndür
        if(moleRenderer != null) moleRenderer.material.color = originalColor;

        targetPosition = new Vector3(moleTransform.localPosition.x, visibleY, moleTransform.localPosition.z);
        
        yield return new WaitForSeconds(waitDuration);

        if (isActive)
        {
            Hide();
        }
    }

    public void OnHit()
    {
        if (!isActive) return;

        StopAllCoroutines(); // Mevcut hareketleri ve beklemeleri durdur
        isActive = false;

        // Puan Ekle
        GameBoard gb = FindObjectOfType<GameBoard>();
        if (gb != null) gb.AddScore(10);

        // Animasyonu Başlat
        StartCoroutine(HitAnimation());
    }

    private IEnumerator HitAnimation()
    {
        // ÖNCEKİ RENK EFEKTİNİ GERİ GETİRİYORUZ
        if (moleRenderer != null) moleRenderer.material.color = Color.red;

        float timer = 0f;
        float duration = 0.5f;
        Vector3 initialScale = moleTransform.localScale;
        Quaternion initialRot = moleTransform.localRotation;

        while (timer < duration)
        {
            timer += Time.deltaTime;
            float t = timer / duration;

            // 1. DÖNME (Spin)
            moleTransform.Rotate(0, 720 * Time.deltaTime, 0); // Hızlıca dön

            // 2. KÜÇÜLME (Shrink)
            moleTransform.localScale = Vector3.Lerp(initialScale, Vector3.zero, t);

            yield return null;
        }

        // Animasyon bitince eski haline getir ve gizle
        moleTransform.localScale = initialScale;
        moleTransform.localRotation = initialRot;
        
        // Rengi düzeltmeyi unutma
        if (moleRenderer != null) moleRenderer.material.color = originalColor;

        targetPosition = new Vector3(moleTransform.localPosition.x, hiddenY, moleTransform.localPosition.z);
        moleTransform.localPosition = targetPosition;
    }

    private void Hide()
    {
        isActive = false;
        targetPosition = new Vector3(moleTransform.localPosition.x, hiddenY, moleTransform.localPosition.z);
    }
}
