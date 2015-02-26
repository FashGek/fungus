﻿using UnityEngine;
using UnityEngine.UI;
using System;
using System.Collections;
using System.Collections.Generic;

namespace Fungus
{

	public class SayDialog : Dialog 
	{
		// Currently active Say Dialog used to display Say text
		public static SayDialog activeSayDialog;

		public Image continueImage;

		public static SayDialog GetSayDialog()
		{
			if (activeSayDialog == null)
			{
				// Use first Say Dialog found in the scene (if any)
				SayDialog sd = GameObject.FindObjectOfType<SayDialog>();
				if (sd != null)
				{
					activeSayDialog = sd;
				}
				
				if (activeSayDialog == null)
				{
					// Auto spawn a say dialog object from the prefab
					GameObject go = Resources.Load<GameObject>("FungusSayDialog");
					if (go != null)
					{
						GameObject spawnedGO = Instantiate(go) as GameObject;
						spawnedGO.name = "SayDialog";
						spawnedGO.SetActive(false);
						activeSayDialog = spawnedGO.GetComponent<SayDialog>();
					}
				}
			}
			
			return activeSayDialog;
		}

		public virtual void Say(string text, bool waitForInput, Action onComplete)
		{
			Clear();

			Action onWritingComplete = delegate {
				if (waitForInput)
				{
					ShowContinueImage(true);
					StartCoroutine(WaitForInput(delegate {
						Clear();
						StopVoiceOver();
						if (onComplete != null)
						{
							onComplete();
						}
					}));
				}
				else
				{
					if (onComplete != null)
					{
						onComplete();
					}
				}
			};

			Action onExitTag = delegate {
				Clear();					
				if (onComplete != null)
				{
					onComplete();
				}
			};

			StartCoroutine(WriteText(text, onWritingComplete, onExitTag));
		}

		public override void Clear()
		{
			base.Clear();
			ShowContinueImage(false);
		}

		protected override void OnWaitForInputTag(bool waiting)
		{
			ShowContinueImage(waiting);
		}

		protected virtual void ShowContinueImage(bool visible)
		{
			if (continueImage != null)
			{
				continueImage.enabled = visible;
			}
		}
	}

}
