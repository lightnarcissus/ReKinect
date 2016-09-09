using UnityEngine;
using System;  // Needed for Math

public class Sinus : MonoBehaviour
{
  // un-optimized version
  public double frequency = 440;
  public double gain = 0.05;
	public double upperThreshold = 440;

	private double startFrequency=240;
  private double increment;
  private double phase;
  private double sampling_frequency = 48000;

	void Start()
	{
		startFrequency = frequency;
	}
    public float timeToSlowDown = 5f;
  void OnAudioFilterRead(float[] data, int channels)
  {
    // update increment if case of frequency as change
    increment = frequency * 2 * Math.PI / sampling_frequency;
    for (var i = 0; i < data.Length; i = i + channels)
    {
      phase = phase + increment;
      data[i] = (float)(gain*Math.Sin(phase));
      if (channels == 2) data[i + 1] = data[i];
      if (phase > 2 * Math.PI) phase = 0;
    }
  }

    public void DecrementFrequency()
    {
		if(frequency>=startFrequency)
        frequency -= ((1f / 60f) * timeToSlowDown); ;
    }

    public void IncrementFrequency()
    {
		if(frequency<=upperThreshold)
        frequency += 10f;
    }
}