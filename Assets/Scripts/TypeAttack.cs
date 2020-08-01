using UnityEngine;
using System.Collections;
[System.Serializable]
public enum TYPE_ATTACK{
	KICK,PUNCH,STICK,SWORD,DAGGER,BATON,GLOVES,NUNCHAKU,SPIKES,PISTOL,BULLET,MAC10,AXE,BUTT
}
public class TypeAttack : MonoBehaviour {
	public TYPE_ATTACK typeAttack;
}
//keytool -list -v -keystore "F:\StickmanEpic\Stickman Epic\user.keystore" -alias tungnhstick -storepass shjniji12 -keypass shjniji12
//keytool -list -v -keystore E:\KeyStoreUnity\SVDefense.keystore -alias svdefensealias -storepass 12345678@ -keypass 12345678@
//1E:8A:70:42:2C:EF:A2:E6:11:77:B5:26:AA:B5:13:0C:E1:13:F1:9B