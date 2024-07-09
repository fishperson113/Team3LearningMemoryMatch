using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.UI;

public class CardRotator
{
    public static IEnumerator RotateCard(Button btn, Sprite newImage)
    {
        // Rotate the card to 90 degrees over 0.25 seconds
        for (float i = 0; i <= 0.25f; i += Time.deltaTime)
        {
            btn.transform.rotation = Quaternion.Euler(0, 180 * i / 0.25f, 0);
            yield return null;
        }
        btn.image.sprite = newImage;

        // Complete the rotation to 180 degrees over 0.25 seconds
        for (float i = 0; i <= 0.25f; i += Time.deltaTime)
        {
            btn.transform.rotation = Quaternion.Euler(0, 180 + 180 * i / 0.25f, 0);
            yield return null;
        }
    }
}
