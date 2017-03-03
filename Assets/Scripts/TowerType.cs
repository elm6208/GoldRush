using System;

public enum TowerType
{
	NONE,
	BASIC,
	DYNAMITE,
	SLOW, // TODO: change to real name
	TOWER4, // TODO: change to real name
}

public static class TowerTypeExtensions
{        
	public static int Cost(this TowerType towerType)
	{
		switch (towerType) {
		case TowerType.BASIC:
			return 5;
		case TowerType.DYNAMITE:
			return 10;
		case TowerType.SLOW:
			return 12;
		case TowerType.TOWER4:
			return 15;
		default:
			return 0;
		}
	}

	public static int Range(this TowerType towerType)
	{
		switch (towerType) {
		case TowerType.BASIC:
			return 5;
		case TowerType.DYNAMITE:
			return 5;
		case TowerType.SLOW:
			return 5;
		case TowerType.TOWER4:
			return 5;
		default:
			return 0;
		}
	}

}