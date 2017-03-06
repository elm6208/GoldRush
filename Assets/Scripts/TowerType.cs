using System;

public enum TowerType
{
	NONE,
	BASIC,
	DYNAMITE,
	SLOW,
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

	// If we want these to be different after multiple promotions, we could pass
	// the 'rank' of the tower in too
  public static float PromoteFirerateChange(this TowerType towerType)
  {
		return -0.05f;
  }

	public static float PromoteRangeChange(this TowerType towerType)
  {
		return 0.5f;
	}

}
