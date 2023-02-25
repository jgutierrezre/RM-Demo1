using System.Collections;
using System.Collections.Generic;
using Microsoft.MixedReality.Toolkit.UI;
using UnityEngine;

public class ResizeObject : MonoBehaviour
{
	public Transform modelSize = null;

	public void OnSliderUpdated(SliderEventData eventData)
	{
		if (modelSize != null)
		{
			modelSize.localScale = new Vector3(eventData.NewValue, eventData.NewValue, eventData.NewValue);
		}
	}
}
