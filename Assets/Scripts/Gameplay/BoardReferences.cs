using Gameplay;
using UnityEngine.UI;
using UnityEngine;

public class BoardReferences : MonoBehaviour
{
    public Cell[] CellViewsOnBoard;

    public SpriteRenderer[] RowImages;
    public SpriteRenderer[] ColumnImages;
    public SpriteRenderer[] DiagonalImages;

    public void DisableAllImages()
    {
        foreach(var row in RowImages)
        {
            row.enabled = false;
        }
        foreach(var column in ColumnImages)
        {
            column.enabled = false;
        }
        foreach(var diagonal in DiagonalImages)
        {
            diagonal.enabled = false;
        }
    }
}
