namespace Clockware {
	using Aimtec;
	using Aimtec.SDK.Events;
	
	class Program {
		static void Main(string[] args) {
			GameEvents.GameStart += GameEvents_GameStart;
		}
		
		private static void GameEvents_GameStart() {
			if (ObjectManager.GetLocalPlayer().ChampionName != "Orianna") return;
			
			var Orianna = new Orianna();
		}
	}
}