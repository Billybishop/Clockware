namespace Clockware {
	using System;
    using System.Collections.Generic;
    using System.Drawing;
    using System.Linq;

    using Aimtec;
    using Aimtec.SDK.Damage;
    using Aimtec.SDK.Extensions;
    using Aimtec.SDK.Menu;
    using Aimtec.SDK.Menu.Components;
    using Aimtec.SDK.Orbwalking;
    using Aimtec.SDK.TargetSelector;
    using Aimtec.SDK.Util.Cache;
    using Aimtec.SDK.Prediction.Skillshots;
    using Aimtec.SDK.Util;
	using Spell = Aimtec.SDK.Spell;
	
	internal class Orianna {
		public static Menu clockwareMenu = new Menu("clockware", "Clockware", true);
		private static float maxDistance = 2400;
		private static Spell eSpell;
		
		public Orianna() {
			PopulateMenu();
			Game.OnUpdate += Game_OnUpdate;
			SetSpells();
			Console.WriteLine("ClockWare v0.0.0.2 Loaded");
		}
		
		public void PopulateMenu() {
			var menu = new Menu("eAssist", "E-Assist"); {
				menu.Add(new MenuKeyBind("key", "Assist Key:", KeyCode.E, KeybindType.Press));
			}
			
			clockwareMenu.Add(menu);
			clockwareMenu.Attach();
		}
		
		private void SetSpells() {
			eSpell = new Spell(SpellSlot.E, 2400);
		}
		
		private Obj_AI_Hero GetNearestAlly(Vector3 pos) {
			List<Obj_AI_Hero> allies = GameObjects.AllyHeroes.ToList();
			Obj_AI_Hero target = null;
			float min = 10000000;
			
			foreach (var ally in allies) {
				bool isNear = ((ally.Distance(ObjectManager.GetLocalPlayer()) <= maxDistance));
				
				if (ally.IsValid && !ally.IsDead && isNear) {
					float dist = ally.Distance(pos);
					if (dist < min) {
						target = ally;
						min = dist;
					}
				}
			}
			
			return target;
		}
		
		private void Game_OnUpdate() {
			if (clockwareMenu["eAssist"]["key"].Enabled) eSpell.Cast(GetNearestAlly(Game.CursorPos));
		}
	}
}