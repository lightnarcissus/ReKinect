using UnityEngine; 
public class Gain : MonoBehaviour { 
	public float level = 1.0f; 

	void OnAudioFilterRead(float[] data, int channels) 
	{ 
		for(int i= 0; i < data.Length; ++i) 
		{
      data[i] *= level;
    } 
	} 
} 
