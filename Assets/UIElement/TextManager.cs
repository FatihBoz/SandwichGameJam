using System.Collections;
using TMPro;
using UnityEngine;

public class TextManager : MonoBehaviour
{
    public GameObject floatingTextPrefab; // Floating Text i�in prefab
    public Transform textSpawnPoint; // Yaz�n�n ��kaca�� nokta
    public float textLifetime = 2f; // Yaz�n�n ne kadar s�re ekranda kalaca��
    public float floatSpeed = 1f; // Yukar� do�ru hareket h�z�


    public void ShowFloatingText(string message, Color textColor)
    {
        if (floatingTextPrefab == null) return;

        // Prefab'� olu�tur
        GameObject floatingText = Instantiate(floatingTextPrefab, textSpawnPoint.position, Quaternion.identity, transform);

        // Yaz� ayarlar�
        TextMeshPro textMesh = floatingText.GetComponent<TextMeshPro>();
        if (textMesh != null)
        {
            Debug.Log("Text mesh null de�il yaz� de�i�me");
            textMesh.text = message;
            textMesh.color = textColor;
        }

        StartCoroutine(FloatTextUpwards(floatingText));
    }

    private IEnumerator FloatTextUpwards(GameObject floatingText)
    {
        float elapsedTime = 0;

        while (elapsedTime < textLifetime)
        {
            floatingText.transform.position += Vector3.up * floatSpeed * Time.deltaTime;

            elapsedTime += Time.deltaTime;
            yield return null; // Bir sonraki frame'e ge�

        }

        Destroy(floatingText);

    }

    public void ShowDamageText(float damage)
    {
        string message = $"-{damage} Taken";
        Color damageColor = Color.red; // K�rm�z� renk

        ShowFloatingText(message, damageColor);
    }

    // Stunned Yaz�s� G�sterici
    public void ShowStunnedText()
    {
        string message = "Stunned!";
        Color stunnedColor = Color.blue; // Mavi renk

        ShowFloatingText(message, stunnedColor);
    }
}
