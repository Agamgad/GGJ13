using UnityEngine;
using System.Collections;
 
class AnimateTiledTexture : MonoBehaviour
{
    public int columns = 2;
    public int rows = 2;
	public bool invertRowProgression = true;
    public float framesPerSecond = 10f;
 
    //the current frame to display
    private int currentCol = 0;
	private int currentRow = 0;
 
    void Start()
    {
        StartCoroutine(updateTiling());
 
        //set the tile size of the texture (in UV units), based on the rows and columns
        Vector2 size = new Vector2(1f / columns, 1f / rows);
        renderer.sharedMaterial.SetTextureScale("_MainTex", size);
    }
 
    private IEnumerator updateTiling()
    {
        while (true)
        {
            //move to the next index
            ++currentCol;
            if (currentCol >= columns) {
                currentCol = 0;
				++currentRow;
			}
			if (currentRow >= rows) {
				currentRow = 0;
				currentCol = 0;
			}
 
            //split into x and y indexes
            Vector2 offset = new Vector2(((float)currentCol) / columns, //x index
                                         ((float)(invertRowProgression ? rows - currentRow - 1 : currentRow)) / rows);   //y index
 
            renderer.sharedMaterial.SetTextureOffset("_MainTex", offset);
 
            yield return new WaitForSeconds(1f / framesPerSecond);
        }
 
    }
}
