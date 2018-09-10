using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace UdonLib.Commons
{
	public class NumberImageView : MonoBehaviour 
	{

		[SerializeField]
		private Sprite[] _numberSprites = new Sprite[10];

		[SerializeField]
		private Image[] _numberImages;

		[SerializeField]
		private bool _unuseImageDestroy = false;

		public void SetNumber(int number)
		{
			for(int i = 0; i < _numberImages.Length; i++)
			{
				if (number == 0 && i == 0) 
				{
					_numberImages [0].gameObject.SetActive(true);
					_numberImages [0].sprite = _numberSprites [0];
					continue;
				}

				int pow = (int)Mathf.Pow (10, i);
				if (pow > number) 
				{
					if (_unuseImageDestroy) {
						Destroy (_numberImages [i].gameObject);
					} else {
						_numberImages [i].gameObject.SetActive(false);
					}

					continue;
				}

				if(_numberImages [i].gameObject.activeSelf == false)
				{
					_numberImages [i].gameObject.SetActive(true);
				}

				int digit = number % (pow * 10);
				if (i > 0) 
				{
					digit /= pow;
				}
				var sprite = _numberSprites [digit];
				var image = _numberImages [i];
				if(sprite == null || image == null){
					Debug.LogError ("NumberImageUI:Error missing digit image");
					return;
				}
					
				image.sprite = sprite;
			}
		}

	}
}