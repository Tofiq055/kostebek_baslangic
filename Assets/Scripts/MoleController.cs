using System.Collections;
using UnityEngine;

public class MoleController : MonoBehaviour
{
    [Header("Hareket Ayarları")]
    public float hiddenY = -0.2f; 
    public float visibleY = 0.2f; 
    public float speed = 4f; 
    public float waitDuration = 1.0f; 

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

        // Vuruş efekti: Kırmızı ol ve beklemeden in
        if (movementCoroutine != null) StopCoroutine(movementCoroutine);
        StartCoroutine(HitRoutine());
    }

    private IEnumerator HitRoutine()
    {
        isActive = false; 

        // Görsel efekt: Kırmızı yan!
        if (moleRenderer != null) moleRenderer.material.color = Color.red;

        // Çok kısa bekle
        yield return new WaitForSeconds(0.15f);

        Hide();
    }

    private void Hide()
    {
        isActive = false;
        targetPosition = new Vector3(moleTransform.localPosition.x, hiddenY, moleTransform.localPosition.z);
    }
}
