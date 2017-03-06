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
			return 5;
		case TowerType.DYNAMITE:
			return 10;
		case TowerType.TOWER3:
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
		case TowerType.TOWER3:
			return 5;
		case TowerType.TOWER4:
			return 5;
		default:
			return 0;
		}
  }

	// If we want these to be different after multiple promotions, we could pass
	// the 'rank' of the tower in too
  public static float PromoteFirerateChange(this TowerType towerType)
  {
		return -0.25f;
  }

	public static float PromoteRangeChange(this TowerType towerType)
  {
		return 1;
	}

}
