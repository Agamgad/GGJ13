using UnityEngine;
using System.Collections;
 
class AnimateTiledTexture : MonoBehaviour
{
    public int columns = 2;
    public int rows = 2;
	public bool invertRowProgression = true;
	public bool bounce = false;
    public float framesPerSecond = 10f;
 
    //the current frame to display
    private int currentCol = 0;
	private int currentRow = 0;
	
	private bool returning = false;
 
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
			if (returning)
				DecrementFrame();
			else
				IncrementFrame();
 
            //split into x and y indexes
            Vector2 offset = new Vector2(((float)currentCol) / columns, //x index
                                         ((float)(invertRowProgression ? rows - currentRow - 1 : currentRow)) / rows);   //y index
 
            renderer.sharedMaterial.SetTextureOffset("_MainTex", offset);
 
            yield return new WaitForSeconds(1f / framesPerSecond);
        }
 
    }
	
	private void IncrementFrame() {
		if (IsAtEnd()) {
			if (bounce) {
				returning = true;
				DecrementFrame();
			} else {
				currentCol = 0;
				currentRow = 0;
			}
		} else {
			++currentCol;
            if (currentCol >= columns) {
                currentCol = 0;
				if (currentRow < rows-1)
					++currentRow;
			}
		}
	}
	
	private void DecrementFrame() {
		if (IsAtStart()) {
			if (bounce) {
				returning = false;
				IncrementFrame();
			} else {
				currentCol = columns-1;
				currentRow = rows-1;
			}
		} else {
			--currentCol;
            if (currentCol < 0) {
                currentCol = columns-1;
				if (currentRow > 0)
					--currentRow;
			}
		}
	}
	
	private bool IsAtEnd() {
		return currentCol+1 >= columns && currentRow+1 >= rows;
	}
	
	private bool IsAtStart() {
		return currentCol <= 0 && currentRow <= 0;
	}
}
