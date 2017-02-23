using System;

public enum TowerType
{
	NONE,
	BASIC,
	DYNAMITE,
	TOWER3, // TODO: change to real name
	TOWER4, // TODO: change to real name
}

public static class TowerTypeExtensions
{        
	public static int Cost(this TowerType towerType)
	{
		switch (towerType) {
		case TowerType.BASIC:
			return 1;
		case TowerType.DYNAMITE:
			return 2;
		case TowerType.TOWER3:
			return 12;
		case TowerType.TOWER4:
			return 15;
		default:
			return 0;
		}
	}
}