using System.Collections;
using UnityEngine;

public class TextManager : MonoBehaviour
{
    public GameObject floatingTextPrefab; // Floating Text i�in prefab
    public Transform textSpawnPoint; // Yaz�n�n ��kaca�� nokta
    public float textLifetime = 2f; // Yaz�n�n ne kadar s�re ekranda kalaca��
    public Vector3 offset = new Vector3(0, 2, 0); // Yaz�n�n karaktere g�re konumu
    public Vector3 randomOffsetRange = new Vector3(0.5f, 0.5f, 0); // Yaz�n�n rastgele hareketi

    public void ShowFloatingText(string message, Color textColor)
    {
        if (floatingTextPrefab == null) return;

        // Prefab'� olu�tur
        GameObject floatingText = Instantiate(floatingTextPrefab, textSpawnPoint.position + offset, Quaternion.identity, transform);

        // Yaz� ayarlar�
        TextMesh textMesh = floatingText.GetComponent<TextMesh>();
        if (textMesh != null)
        {
            textMesh.text = message;
            textMesh.color = textColor;
        }

        // Rastgele hareket i�in pozisyon ayar�
        Vector3 randomOffset = new Vector3(
            Random.Range(-randomOffsetRange.x, randomOffsetRange.x),
            Random.Range(-randomOffsetRange.y, randomOffsetRange.y),
            Random.Range(-randomOffsetRange.z, randomOffsetRange.z)
        );

        floatingText.transform.localPosition += randomOffset;

        // Yaz�y� yok etme
        Destroy(floatingText, textLifetime);
    }

    public void ShowDamageText(int damage)
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
