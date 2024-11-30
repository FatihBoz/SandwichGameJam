using System.Collections;
using UnityEngine;

public class TextManager : MonoBehaviour
{
    public GameObject floatingTextPrefab; // Floating Text için prefab
    public Transform textSpawnPoint; // Yazýnýn çýkacaðý nokta
    public float textLifetime = 2f; // Yazýnýn ne kadar süre ekranda kalacaðý
    public Vector3 offset = new Vector3(0, 2, 0); // Yazýnýn karaktere göre konumu
    public Vector3 randomOffsetRange = new Vector3(0.5f, 0.5f, 0); // Yazýnýn rastgele hareketi

    public void ShowFloatingText(string message, Color textColor)
    {
        if (floatingTextPrefab == null) return;

        // Prefab'ý oluþtur
        GameObject floatingText = Instantiate(floatingTextPrefab, textSpawnPoint.position + offset, Quaternion.identity, transform);

        // Yazý ayarlarý
        TextMesh textMesh = floatingText.GetComponent<TextMesh>();
        if (textMesh != null)
        {
            textMesh.text = message;
            textMesh.color = textColor;
        }

        // Rastgele hareket için pozisyon ayarý
        Vector3 randomOffset = new Vector3(
            Random.Range(-randomOffsetRange.x, randomOffsetRange.x),
            Random.Range(-randomOffsetRange.y, randomOffsetRange.y),
            Random.Range(-randomOffsetRange.z, randomOffsetRange.z)
        );

        floatingText.transform.localPosition += randomOffset;

        // Yazýyý yok etme
        Destroy(floatingText, textLifetime);
    }

    public void ShowDamageText(int damage)
    {
        string message = $"-{damage} Taken";
        Color damageColor = Color.red; // Kýrmýzý renk

        ShowFloatingText(message, damageColor);
    }

    // Stunned Yazýsý Gösterici
    public void ShowStunnedText()
    {
        string message = "Stunned!";
        Color stunnedColor = Color.blue; // Mavi renk

        ShowFloatingText(message, stunnedColor);
    }
}
