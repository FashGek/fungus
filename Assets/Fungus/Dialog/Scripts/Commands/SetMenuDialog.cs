using UnityEngine;
using System;
using System.Collections;

namespace Fungus
{
	[CommandInfo("Dialog", 
	             "Set Menu Dialog", 
	             "Sets a custom menu dialog to use when displaying multiple choice menus")]
	[AddComponentMenu("")]
	public class SetMenuDialog : Command 
	{
		public MenuDialog menuDialog;

		public override void OnEnter()
		{
			if (menuDialog != null)
			{
				MenuDialog.activeMenuDialog = menuDialog;
			}

			Continue();
		}

		public override string GetSummary()
		{
			if (menuDialog == null)
			{
				return "Error: No menu dialog selected";
			}

			return menuDialog.name;
		}

		public override Color GetButtonColor()
		{
			return new Color32(184, 210, 235, 255);
		}
	}

}